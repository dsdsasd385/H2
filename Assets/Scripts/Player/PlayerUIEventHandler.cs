using UnityEngine;

public class PlayerUIEventHandler : MonoBehaviour
{
    private Status _status;
    private PlayerUI _playerUI;
    private Wallet _wallet;
    private PlayerExp _playerExp;

    
    public void Initialize(PlayerUI playerUI,Wallet wallet,PlayerExp playerExp,Status status)
    {
        _playerUI = playerUI;
        _wallet = wallet;
        _playerExp = playerExp;
        _status = status;
        
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        _status.OnHpChange += _playerUI.SetHpVar;
        _status.OnPowerChange += _playerUI.SetPowerText;
        _status.OnDefenseChange += _playerUI.SetDefenseText;
        _status.OnCriticalChange += _playerUI.SetCriticalText;
        _status.OnSpeedChange += _playerUI.SetSpeedText;
        _wallet.OnCoinChanged += _playerUI.SetCoinText;
    }

    private void OnDestroy()
    { 

        _status.OnHpChange -= _playerUI.SetHpVar;
        _status.OnPowerChange -= _playerUI.SetPowerText;
        _status.OnDefenseChange -= _playerUI.SetDefenseText;
        _status.OnCriticalChange -= _playerUI.SetCriticalText;
        _status.OnSpeedChange -= _playerUI.SetSpeedText;
        _wallet.OnCoinChanged -= _playerUI.SetCoinText;
        
    }
}
