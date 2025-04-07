using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public Player Player {  get; private set; }

    private Wallet _wallet;
    public Wallet Wallet => _wallet;

    public PlayerUI _playerUI { get; private set; }
    public PlayerExp _playerExp { get; private set; }

    private Scene _oldScene; 
    private StageRouletteType _stageRouletteTypes;

    private event Action<RouletteResult> roulette;
      

    public static void InitializeFromChapter()
    {
        // if(Chapter.playerObj.TryGetComponent(out PlayerController controller))
        // {
        //     controller.InitializeComponents();
        // }
        //
        // else
        // {
        //     Debug.Log($" PlayerController�� Chapter.playerObj�� �����ϴ�!");
        // }

    }

    public void InitializeComponents()
    {
        if(Player == null)
            Player = new Player();

        Player.Init();

        _playerUI = GetComponent<PlayerUI>();
        _playerExp = new PlayerExp();
        _wallet = GetComponent<Wallet>();

        var eventHandle = GetComponent<PlayerUIEventHandler>();

        if( eventHandle != null)
        {
            eventHandle.Initialize(_playerUI, _wallet, _playerExp, Player.Status);
        }
        else
        {
            Debug.LogWarning("PlayerUIEventHandler�� �������� �ʽ��ϴ�.");
        }
    }
    void Awake()
    {
        //Player = new Player();
        //_playerExp = new PlayerExp();
        //_playerUI = GetComponent<PlayerUI>();
        //Debug.Log($"Player Status = {Player.Status}");

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
