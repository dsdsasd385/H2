using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    #region 싱글톤 
    private static PlayerExp _instance;

    public static PlayerExp Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerExp>();

                if (_instance == null)
                {
                    GameObject player = new GameObject("PlayerExp");
                    _instance = player.AddComponent<PlayerExp>();

                }

                DontDestroyOnLoad(_instance.gameObject);  // 씬이 변경되어도 유지
            }

            return _instance;
        }
    }
    #endregion

    public event Action<int> OnChangeExp;
    public event Action<int> OnLevelUp;


    private int _exp;
    private int _expToNextLevel;
    private int _level;

    private void Awake()
    {
        // 중복방지
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }
    public int Exp
    {
        get
        {
            return _exp;
        }
        set
        {
            _exp += value;
            OnChangeExp?.Invoke(_exp);
            Debug.Log($"Exp가 변경되었습니다. {_exp}");
        }
    }

    public int Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level++;
        }
    }

    public void AddExp(int exp)
    {
        if(_expToNextLevel <= (Exp + exp))
        {
            LevelUp();
            Debug.Log($"레벨업 했습니다. Level : {_level}");
        }
        Exp += exp;

        Debug.Log($"경험치가 올랐습니다. 경험치 : {exp}");
    }

    public void LevelUp()
    {
        _level++;
        // 스킬추가
    }
}
