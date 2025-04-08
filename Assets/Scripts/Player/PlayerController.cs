using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEditor;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Player Player {  get; private set; }

    private WalletController _wallet;
    public WalletController Wallet => _wallet;

    public PlayerUI _playerUI { get; private set; }
    public PlayerAnimationHandler _playerAni { get; private set; }

    private Scene _oldScene; 
    private StageRouletteType _stageRouletteTypes;

    private event Action<RouletteResult> roulette;
      

    public static void InitializeFromChapter()
    {
        if(Chapter.playerObj.TryGetComponent(out PlayerController controller))
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
        _playerUI = GetComponent<PlayerUI>();
        _wallet = GetComponent<WalletController>();
        _playerAni = GetComponent<PlayerAnimationHandler>();

        if (Player == null)
            Player = new Player();

        Player.Init();

        SubscribeToEvents();
    }
    void Awake()
    {
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
        UnsubscribeFromEvents();
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
        }
    }


    public void SubscribeToEvents()
    {
        if (_wallet != null && _playerUI != null && Player != null)
        {
            Player.Status.OnHpChange += _playerUI.SetHpVar;
            Player.Status.OnPowerChange += _playerUI.SetPowerText;
            Player.Status.OnDefenseChange += _playerUI.SetDefenseText;
            Player.OnChangeExp += _playerUI.SetExp;
            Player.OnLevelUp += _playerUI.SetLevel;
            _wallet.wallet.OnCoinChanged += _playerUI.SetCoinText;
            Player.OnPlayAttackAnimation += _playerAni.PlayAttackAni;
            Player.OnPlayDamagedAnimation += _playerAni.PlayDamagedAni;
            Player.OnPlayDieAnimation += _playerAni.PlayDieAni;
        }
    }

    public void UnsubscribeFromEvents()
    {
        if(_wallet != null &&  _playerUI != null && Player != null)
        {
            Player.Status.OnHpChange -= _playerUI.SetHpVar;
            Player.Status.OnPowerChange -= _playerUI.SetPowerText;
            Player.Status.OnDefenseChange -= _playerUI.SetDefenseText;
            Player.OnChangeExp -= _playerUI.SetExp;
            Player.OnLevelUp -= _playerUI.SetLevel;
            _wallet.wallet.OnCoinChanged -= _playerUI.SetCoinText;
            Player.OnPlayAttackAnimation -= _playerAni.PlayAttackAni;
            Player.OnPlayDamagedAnimation -= _playerAni.PlayDamagedAni;
            Player.OnPlayDieAnimation -= _playerAni.PlayDieAni;
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
