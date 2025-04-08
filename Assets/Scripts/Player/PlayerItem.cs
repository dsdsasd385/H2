using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{   
    public event Action<int> OnChangeCoin;

    private int _coin;
    public int Coin
    {
        get { return _coin; }  // hp ���� ������ ���� �׳� ��ȯ
        set
        {
            int oldCoin = _coin;
            // hp ���� 0 ���Ϸ� �������� �ʵ���
            _coin = Mathf.Max(0, value);
            OnChangeCoin?.Invoke(_coin);
            Debug.Log($"������ ����Ǿ����ϴ�. �������� : {oldCoin}, �������� : {_coin}");

        }
    }

    private void Awake()
    {
        //if(_instance != null && _instance != this)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //_instance = this;
    }


    public void SetGold(int addCoin)
    {
        Debug.Log($"����������{_coin}");
        Coin += addCoin;
    }

    public void RemoveGold(int removeCoin)
    {
        Coin -= removeCoin;
    }
}
