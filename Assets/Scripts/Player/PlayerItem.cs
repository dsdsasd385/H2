using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    //#region 싱글톤
    //private static PlayerItem _instance;

    //public static PlayerItem Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = FindObjectOfType<PlayerItem>();

    //            if(_instance == null)
    //            {
    //                GameObject playerItem = new GameObject("PlayerItem");
    //                _instance = playerItem.AddComponent<PlayerItem>();

    //                DontDestroyOnLoad(playerItem);
    //            }
    //        }
    //        return _instance;
    //    }
    //}

    //#endregion      

    public event Action<int> OnChangeCoin;

    private int _coin;
    public int Coin
    {
        get { return _coin; }  // hp 값을 가져올 때는 그냥 반환
        set
        {
            int oldCoin = _coin;
            // hp 값이 0 이하로 설정되지 않도록
            _coin = Mathf.Max(0, value);
            OnChangeCoin?.Invoke(_coin);
            Debug.Log($"코인이 변경되었습니다. 이전코인 : {oldCoin}, 현재코인 : {_coin}");

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
        Debug.Log($"현재코인은{_coin}");
        Coin += addCoin;
    }

    public void RemoveGold(int removeCoin)
    {
        Coin -= removeCoin;
    }
}
