using System;
using System.Threading.Tasks;
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
    public ref Status Status => ref _status;

    private int _lastHp;
    private int _exp;
    private int _expToNextLevel;
    private int _level;
    private Monster _monsterTarget;

    public event Action<int> OnChangeExp;
    public event Action<int> OnLevelUp;
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
            _level = value;
            OnLevelUp?.Invoke(_level);
        }
    }


    public void Init()
    {
        _status = new Status(50, 3000f, 5f, 0.05f, 1f);
        _level = 1;
        _exp = 0;
        _expToNextLevel = 25;
    }   

    public void AddExp(int exp)
    {
        Exp += exp;

        while (Exp >= _expToNextLevel)
        {
            LevelUp();
        }     
    }

    public void LevelUp()
    {
        Exp -= _expToNextLevel;
        Level++;
        Debug.Log($"������ �߽��ϴ�. Level : {_level}");

        _expToNextLevel = Mathf.RoundToInt(_expToNextLevel * 1.5f);
        Debug.Log($"���� ����ġ�� {Exp}�̸� ������ ����ġ�� {_expToNextLevel}���� ����Ǿ����ϴ�.");
        // UI����

        // ��ų�߰�
    }
    public override void Attack(Entity target)
    {
        _monsterTarget = target as Monster;

        if (_monsterTarget != null)
        {
            //animator.SetTrigger("Attack");
            target.TakeDamage(_status.Power, _monsterTarget.Status.Defense, _status.Critical, this);
        }

        else
        {
            return;
        }
    }
    public override void TakeDamage(float power, float defense, float critical, Entity attacker)
    {
        //animator.SetTrigger("Damaged");
        if (_monsterTarget == null && attacker is Monster monster)
            _monsterTarget = monster;

        float damage = base.CalculateDamage(power, defense, critical);

        if (damage > _status.Hp)
        {
            Die();
            return;
        }
        int damageInt = (int)damage;
        _status.Hp -= damageInt;
    }

    protected async override void Die()
    {
        // ��� ����
        //animator.SetTrigger("Die");
        Debug.Log("�÷��̾ ����߽��ϴ�.");
        await Task.Delay(2500);

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
        Debug.Log($"ü���� ����Ǿ����ϴ�. {_status.Hp}");
    }

    // ���ݷº��Ҷ� �̺�Ʈ
    public void OnPowerChanged(int value)
    {
        // ���ݷ� ����
        _status.Power *= (1 + value / 100f);
        Debug.Log($"���ݷ��� ����Ǿ����ϴ�. {_status.Power}");
    }
    // ���º��Ҷ� �̺�Ʈ
    public void OnDefenseChanged(int value)
    {
        // ���� ����
        _status.Defense *= (1 + value / 100f);

        Debug.Log($"������ ����Ǿ����ϴ�. {_status.Defense}");
    }

    public void OnCriticalChanged(int value)
    {
        _status.Critical *= (1 + value / 100f);
        Debug.Log($"ũ��Ƽ���� ����Ǿ����ϴ�. {_status.Critical}");
    }   
}
