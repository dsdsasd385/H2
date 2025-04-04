using System;
using UnityEngine;

public class Wallet
{
    public event Action<int> OnCoinChanged;

    private int _coin;
    public int Coin
    {
        get => _coin;
        private set
        {
            int oldCoin = _coin;
            _coin = Math.Max(0, value);
            OnCoinChanged?.Invoke(_coin);
            Debug.Log($"코인 변경: {oldCoin} -> {_coin}");
        }
    }

    public void AddCoin(int amount)
    {
        Coin += amount;
    }

    public void LostCoin(int amount)
    {
        Coin += amount;
    }
}
