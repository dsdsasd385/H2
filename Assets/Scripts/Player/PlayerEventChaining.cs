using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerEventChaining : MonoBehaviour
{

    private Status _status;
    private PlayerUI _playerUI;
    private PlayerItem _item;
    private void Awake()
    {
        _playerUI = GetComponent<PlayerUI>();
        _status = GetComponent<Player>().status;

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        _item.OnChangeCoin += _playerUI.SetCoinText;
        _status.OnHpChange += _playerUI.SetHpVar;
        _status.OnPowerChange += _playerUI.SetPowerText;
        _status.OnDefenseChange += _playerUI.SetDefenseText;
        _status.OnCriticalChange += _playerUI.SetCriticalText;
        _status.OnSpeedChange += _playerUI.SetSpeedText;
    }

    private void OnDestroy()
    {
        _item.OnChangeCoin -= _playerUI.SetCoinText;
        _status.OnHpChange -= _playerUI.SetHpVar;
        _status.OnPowerChange -= _playerUI.SetPowerText;
        _status.OnDefenseChange -= _playerUI.SetDefenseText;
        _status.OnCriticalChange -= _playerUI.SetCriticalText;
        _status.OnSpeedChange -= _playerUI.SetSpeedText;
    }
}
