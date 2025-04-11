using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    public Player Player { get; private set; }

    public PlayerAnimationHandler _playerAni { get; private set; }

    private Scene _oldScene;
    private StageRouletteType _stageRouletteTypes;

    private event Action<RouletteResult> roulette;

    private Monster _monster;
    private Vector3 _originPos;
    private float _moveSpeed;

    public static void InitializeFromChapter()
    {
        if (Chapter.playerObj.TryGetComponent(out PlayerController controller))
        {
            controller.InitializeComponents();
        }

        else
        {
            Debug.Log($" PlayerController�� Chapter.playerObj�� �����ϴ�!");
        }
    }

    public void InitializeComponents()
    {
        _playerAni = GetComponent<PlayerAnimationHandler>();
        Player = Player.currentPlayer;
        _originPos = transform.position;

        if (Player == null)
        {
            Debug.Log("Player가 null입니다.");
            return;
        }

        Player.transform = transform;

        _oldScene = SceneManager.GetActiveScene();

        SubscribeToEvents();

        Player.Init();
    }
    private void SubscribeToEvents()
    {
        Chapter.RouletteResultChangedEvent += SetStatus;
        Battle.BattleStart += MoveToBattleScene;
    }

    private void OnDisable()
    {
        Chapter.RouletteResultChangedEvent -= SetStatus;
        Battle.BattleStart -= MoveToBattleScene;
    }
    private void OnDestroy()
    {
        Chapter.RouletteResultChangedEvent -= SetStatus;
        Battle.BattleStart -= MoveToBattleScene;
    }

    private void SetStatus(RouletteResult result)
    {
        switch (result.Type)
        {
            case StageRouletteType.EXERCISE:
                Player.OnHpChanged(result.ChangeValue);
                Debug.Log($"체력 증가! {result.ChangeValue}%");
                break;
            case StageRouletteType.RESHARPENING_WEAPON:
                Player.OnPowerChanged(result.ChangeValue);
                Debug.Log($"공격력 증가!{result.ChangeValue}%");
                break;
            case StageRouletteType.CLEANING_ARMOR:
                Player.OnDefenseChanged(result.ChangeValue);
                Debug.Log($"방어력 증가! {result.ChangeValue}%");
                break;
            case StageRouletteType.PICK_COIN:
                Player.AddCoin(result.ChangeValue);
                Debug.Log($"코인 획득! {result.ChangeValue}");
                break;
            case StageRouletteType.BUG_BITE:
                Player.OnHpChanged(result.ChangeValue);
                Debug.Log($"체력이 깎였습니다. {result.ChangeValue} %");
                break;
            case StageRouletteType.BROKEN_WEAPON:
                Player.OnPowerChanged(result.ChangeValue);
                Debug.Log($"공격력이 깎였습니다!  {result.ChangeValue} %");
                break;
            case StageRouletteType.LOOSEN_ARMOR:
                Player.OnDefenseChanged(result.ChangeValue);
                Debug.Log($"방어력이 깎였습니다! {result.ChangeValue} %");
                break;

            case StageRouletteType.LOST_COIN:
                Player.LostCoin(result.ChangeValue);
                Debug.Log($"코인을 잃었습니다! {result.ChangeValue}");
                break;
        }
    }

    public void MoveToBattleScene()
    {
        if (Battle._battle && Battle.IsBattle)
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName("BattleScene"));
        }
        else
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(_oldScene.name));
        }
    }

    // 플레이어 -> 몬스터 이동
    public IEnumerator PlayerMoveToTarget(Transform transform, Vector3 targetTransform, float speed)
    {
        _moveSpeed = speed;
        Vector3 targetPos = targetTransform;

        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator PlayerAttackSequence(PlayerController player, MonsterController monster)
    {
        Debug.Log("플레이어가 공격을 시작했습니다..");

        yield return Skill.UseActiveSkills(player.Player, new List<Entity>() { monster.Monster });
        
        yield return _playerAni.PlayAttackAni();

        yield return monster.TakeDamageSequence(player.Player);

        yield return PlayerMoveToTarget(transform, _originPos, _moveSpeed);
    }

    public IEnumerator TakeDamageSequence(Entity attacker)
    {
        Debug.Log("플레이어가 데미지받았습니다.");

        yield return _playerAni.PlayDamagedAni();

        yield return new WaitForSeconds(1f);

        if ( attacker is Monster monster)
            _monster = monster;

        Player.TakeDamage(_monster.Status.Power, Player.Status.Defense, _monster.Status.Critical);


        if (_monster.Status.Hp <= 0)
        {
            _monster.Die();
        }
    }
}
