using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    public Player Player => _player;

    private Wallet _wallet;
    public Wallet Wallet => _wallet;

    public PlayerUI _playerUI { get; private set; }
    public PlayerExp _playerExp { get; private set; }

    private Scene _oldScene; 
    private StageRouletteType _stageRouletteTypes;

    private event Action<RouletteResult> roulette;
      
    void Awake()
    {
        _player = new Player();
        Debug.Log($"Player Status = {Player._status}");

        _oldScene = SceneManager.GetActiveScene();
    }
    private void OnEnable()
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
                _player.OnHpChanged(result.ChangeValue);
                Debug.Log($"체력 증가! {result.ChangeValue}%");
                break;
            case StageRouletteType.RESHARPENING_WEAPON:
                _player.OnPowerChanged(result.ChangeValue);
                Debug.Log($"공격력 증가! {result.ChangeValue}%");
                break;
            case StageRouletteType.CLEANING_ARMOR:
                _player.OnDefenseChanged(result.ChangeValue);
                Debug.Log($"방어력 증가! {result.ChangeValue}%");
                break;
            case StageRouletteType.BUG_BITE:
                _player.OnHpChanged(result.ChangeValue);
                Debug.Log($"체력 감소! {result.ChangeValue} %");
                break;
            case StageRouletteType.BROKEN_WEAPON:
                _player.OnPowerChanged(result.ChangeValue);
                Debug.Log($"공격력 감소! {result.ChangeValue} %");
                break;
            case StageRouletteType.LOOSEN_ARMOR:
                _player.OnDefenseChanged(result.ChangeValue);
                Debug.Log($"방어력 감소! {result.ChangeValue} %");
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



}
