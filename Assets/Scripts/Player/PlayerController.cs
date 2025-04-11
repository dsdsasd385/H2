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

        if (Player == null)
        {
            Debug.Log("Player�� �����ϴ�.");
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
                Debug.Log($"ü�� ����! {result.ChangeValue}%");
                break;
            case StageRouletteType.RESHARPENING_WEAPON:
                Player.OnPowerChanged(result.ChangeValue);
                Debug.Log($"���ݷ� ����! {result.ChangeValue}%");
                break;
            case StageRouletteType.CLEANING_ARMOR:
                Player.OnDefenseChanged(result.ChangeValue);
                Debug.Log($"���� ����! {result.ChangeValue}%");
                break;
            case StageRouletteType.PICK_COIN:
                Player.AddCoin(result.ChangeValue);
                Debug.Log($"���� ȹ��! {result.ChangeValue}");
                break;
            case StageRouletteType.BUG_BITE:
                Player.OnHpChanged(result.ChangeValue);
                Debug.Log($"ü�� ����! {result.ChangeValue} %");
                break;
            case StageRouletteType.BROKEN_WEAPON:
                Player.OnPowerChanged(result.ChangeValue);
                Debug.Log($"���ݷ� ����! {result.ChangeValue} %");
                break;
            case StageRouletteType.LOOSEN_ARMOR:
                Player.OnDefenseChanged(result.ChangeValue);
                Debug.Log($"���� ����! {result.ChangeValue} %");
                break;

            case StageRouletteType.LOST_COIN:
                Player.LostCoin(result.ChangeValue);
                Debug.Log($"���� ����! {result.ChangeValue}");
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

    public IEnumerator PlayerAttackSequence(PlayerController player, MonsterController monster)
    {
        Debug.Log("�÷��̾ �����մϴ�.");

        yield return Skill.UseActiveSkills(player.Player, new List<Entity>() { monster.Monster });
        
        yield return _playerAni.PlayAttackAni();

        yield return monster.TakeDamageSequence(player.Player);
    }

    public IEnumerator TakeDamageSequence(Entity attacker)
    {
        Debug.Log("�÷��̾ �¾ҽ��ϴ�.");

        _playerAni.PlayDamagedAni();

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
