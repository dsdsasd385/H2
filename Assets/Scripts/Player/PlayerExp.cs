using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : Player
{
    private int _exp;
    private int _expToNextLevel;
    private int _level;
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
    public PlayerExp _playerExp { get; private set; }

    public event Action<int> OnChangeExp;
    public event Action<int> OnLevelUp;





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
        // UI����

        // ��ų�߰�
    }
}
