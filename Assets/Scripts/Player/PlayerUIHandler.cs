using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
    private Status _status;
    private PlayerUI _playerUI;
    private WalletController _walletController;
    private Player _playerExp;

    
    public void Initialize(PlayerUI playerUI, WalletController wallet, Player playerExp,Status status)
    {
        _playerUI = playerUI;
        _walletController = wallet;
        _playerExp = playerExp;
        _status = status;
        
        SubscribeToEvents();
    }

    public void SubscribeToEvents()
    {
        Debug.Log("playerUi¾øÀ½");
        _status.OnHpChange += _playerUI.SetHpVar;
        _status.OnPowerChange += _playerUI.SetPowerText;
        _status.OnDefenseChange += _playerUI.SetDefenseText;
        _status.OnCriticalChange += _playerUI.SetCriticalText;
        _status.OnSpeedChange += _playerUI.SetSpeedText;
        _walletController.wallet.OnCoinChanged += _playerUI.SetCoinText;
    }

    private void OnDestroy()
    { 
        if( _playerUI != null && _walletController != null)
        {
            _status.OnHpChange -= _playerUI.SetHpVar;
            _status.OnPowerChange -= _playerUI.SetPowerText;
            _status.OnDefenseChange -= _playerUI.SetDefenseText;
            _status.OnCriticalChange -= _playerUI.SetCriticalText;
            _status.OnSpeedChange -= _playerUI.SetSpeedText;
            _walletController.wallet.OnCoinChanged -= _playerUI.SetCoinText;
        }
    }
}
