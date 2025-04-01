using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventChaining : MonoBehaviour
{

    Status _status;
    PlayerUI _playerUI;
    private void Awake()
    {
        _playerUI = GetComponent<PlayerUI>();
        _status = GetComponent<Player>().status;

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        _status.OnHpChange += _playerUI.SetHpVar;
        _status.OnPowerChange += _playerUI.SetPowerText;
        _status.OnDefenseChange += _playerUI.SetDefenseText;
        _status.OnCriticalChange += _playerUI.SetCriticalText;
        _status.OnSpeedChange += _playerUI.SetSpeedText;
    }

    private void OnDestroy()
    {
        _status.OnHpChange -= _playerUI.SetHpVar;
        _status.OnPowerChange -= _playerUI.SetPowerText;
        _status.OnDefenseChange -= _playerUI.SetDefenseText;
        _status.OnCriticalChange -= _playerUI.SetCriticalText;
        _status.OnSpeedChange -= _playerUI.SetSpeedText;
    }
}
