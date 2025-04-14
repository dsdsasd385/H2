using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : Entity
{
    public static Player currentPlayer {  get; private set; }
    public static void CreatePlayer()
    {
        var player = new Player();
        currentPlayer = player;
        
        PlayerController.InitializeFromChapter();
    }

    private Status _status;
    public Status Status => _status;

    private int _lastHp;
    private int _exp;
    private int _expToNextLevel;
    private int _level;
    private Monster _monsterTarget;

    public event Action<int, int> OnChangeExp;
    public event Action<int>      OnLevelUp;
    public event Action<int>      OnCoinChanged;

    private int _coin;
    public int Coin
    {
        get => _coin;
        private set
        {
            int oldCoin = _coin;
            _coin = Math.Max(0, value);
            OnCoinChanged?.Invoke(_coin);
            Debug.Log($"���� ����: {oldCoin} -> {_coin}");
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

    public int Exp
    {
        get
        {
            return _exp;
        }
        set
        {
            _exp = value;

            while (_exp >= _expToNextLevel)
            {
                var remain = _exp - _expToNextLevel;
                Level++;
                _expToNextLevel = Mathf.CeilToInt(_expToNextLevel * 1.5f);
                _exp = remain;
            }
            
            OnChangeExp?.Invoke(_exp, _expToNextLevel);
            Debug.Log($"Exp획득. {_exp}");
        }
    }

    public int NeedExp => _expToNextLevel;


    public int Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
            OnLevelUp?.Invoke(_level);
        }
    }


    public void Init()
    {
        _status = new Status(SaveLoad.LoadStatus());
        _level = 1;
        _exp = 0;
        _expToNextLevel = 25;
    }   

    public void AddExp(int exp)
    {
        Exp += exp;
    }

    public override void TakeDamage(float power, float defense, float critical)
    {
        float damage = base.CalculateDamage(power, defense, critical);

        int damageInt = (int)damage;

        _status.Hp -= damageInt;
        var text = WorldCanvas.Get<DamageText>(transform.position + Vector3.up, 100);
        text.ShowDamage(damageInt);
    }


    public override void Die()
    {
        // ��� ����
        //animator.SetTrigger("Die");
        Debug.Log("플레이어가 사망했습니다..");

        SceneManager.LoadScene("TitleScene");
    }


    // ü�º��Ҷ� �̺�Ʈ
    public void OnHpChanged(int value)
    {
        // ���� ü�� ����
        _lastHp = _status.Hp;
        // ü�� ����
        var newHp = _lastHp * (1 + value / 100f);
        _status.Hp = (int)newHp;
        Debug.Log($"체력 변경. {_status.Hp}");
    }

    // ���ݷº��Ҷ� �̺�Ʈ
    public void OnPowerChanged(int value)
    {
        // ���ݷ� ����
        _status.Power *= (1 + value / 100f);
        Debug.Log($"공격력 변경. {_status.Power}");
    }
    // ���º��Ҷ� �̺�Ʈ
    public void OnDefenseChanged(int value)
    {
        // ���� ����
        _status.Defense *= (1 + value / 100f);

        Debug.Log($"방어력 변경. {_status.Defense}");
    }

    public void OnCriticalChanged(int value)
    {
        _status.Critical *= (1 + value / 100f);
        Debug.Log($"치명타 변경. {_status.Critical}");
    }   
}
