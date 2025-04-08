using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEditor;
using DG.Tweening;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Player Player { get; private set; }

    public PlayerAnimationHandler _playerAni { get; private set; }

    private Scene _oldScene;
    private StageRouletteType _stageRouletteTypes;

    private event Action<RouletteResult> roulette;


    public static void InitializeFromChapter()
    {
        if (Chapter.playerObj.TryGetComponent(out PlayerController controller))
        {
            controller.InitializeComponents();
        }

        else
        {
            Debug.Log($" PlayerController가 Chapter.playerObj에 없습니다!");
        }

    }

    public void InitializeComponents()
    {
        _playerAni = GetComponent<PlayerAnimationHandler>();
        Player = Player.currentPlayer;

        if (Player == null)
        {
            Debug.Log("Player가 없습니다.");
        }

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
                Debug.Log($"공격력 증가! {result.ChangeValue}%");
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
                Debug.Log($"체력 감소! {result.ChangeValue} %");
                break;
            case StageRouletteType.BROKEN_WEAPON:
                Player.OnPowerChanged(result.ChangeValue);
                Debug.Log($"공격력 감소! {result.ChangeValue} %");
                break;
            case StageRouletteType.LOOSEN_ARMOR:
                Player.OnDefenseChanged(result.ChangeValue);
                Debug.Log($"방어력 감소! {result.ChangeValue} %");
                break;

            case StageRouletteType.LOST_COIN:
                Player.LostCoin(result.ChangeValue);
                Debug.Log($"코인 감소! {result.ChangeValue}");
                break;
        }
    }

    private void MoveToBattleScene()
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

    public IEnumerator AttackCoroutine(Entity target)
    {
        _playerAni.PlayAttackAni();

        yield return new WaitForSeconds(0.5f); // => 애니메이션 시간에 맞춰 시간설정

        Player.Attack(target);
    }

}
