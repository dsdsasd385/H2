using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    #region �̱��� 
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

                DontDestroyOnLoad(_instance.gameObject);  // ���� ����Ǿ ����
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
        // �ߺ�����
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
            Debug.Log($"Exp�� ����Ǿ����ϴ�. {_exp}");
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
            Debug.Log($"������ �߽��ϴ�. Level : {_level}");
        }
        Exp += exp;

        Debug.Log($"����ġ�� �ö����ϴ�. ����ġ : {exp}");
    }

    public void LevelUp()
    {
        _level++;
        // ��ų�߰�
    }
}
