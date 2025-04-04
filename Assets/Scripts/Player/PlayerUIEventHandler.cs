using UnityEngine;

public class PlayerUIEventHandler : MonoBehaviour
{
    private Status _status;
    private PlayerUI _playerUI;
    private Wallet _wallet;
    private PlayerExp _playerExp;


    private void Start()
    {
        _playerUI = GetComponent<PlayerUI>();
        _status = GetComponent<PlayerController>().Player._status;
        _wallet = GetComponent<WalletController>().wallet;
        //_playerExp = GetComponent<PlayerExp>();

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        _status.OnHpChange += _playerUI.SetHpVar;
        _status.OnPowerChange += _playerUI.SetPowerText;
        _status.OnDefenseChange += _playerUI.SetDefenseText;
        _status.OnCriticalChange += _playerUI.SetCriticalText;
        _status.OnSpeedChange += _playerUI.SetSpeedText;
        //_wallet.OnCoinChanged += _playerUI.SetCoinText;
    }

    private void OnDestroy()
    {
        _status.OnHpChange -= _playerUI.SetHpVar;
        _status.OnPowerChange -= _playerUI.SetPowerText;
        _status.OnDefenseChange -= _playerUI.SetDefenseText;
        _status.OnCriticalChange -= _playerUI.SetCriticalText;
        _status.OnSpeedChange -= _playerUI.SetSpeedText;
        //_wallet.OnCoinChanged -= _playerUI.SetCoinText;
        
    }
}
