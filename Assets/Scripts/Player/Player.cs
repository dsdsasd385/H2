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
    public Status Status => _status;

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
        Debug.Log($"레벨업 했습니다. Level : {_level}");

        _expToNextLevel = Mathf.RoundToInt(_expToNextLevel * 1.5f);
        Debug.Log($"남은 경험치는 {Exp}이며 레벨업 경험치는 {_expToNextLevel}으로 변경되었습니다.");
        // UI연결

        // 스킬추가
    }

    public override void TakeDamage(float power, float defense, float critical)
    {
        float damage = base.CalculateDamage(power, defense, critical);

        int damageInt = (int)damage;

        _status.Hp -= damageInt;
    }


    public override void Die()
    {
        // 사망 로직
        //animator.SetTrigger("Die");
        Debug.Log("플레이어가 사망했습니다.");

        SceneManager.LoadScene("TitleScene");
    }


    // 체력변할때 이벤트
    public void OnHpChanged(int value)
    {
        // 이전 체력 저장
        _lastHp = _status.Hp;
        // 체력 수정
        var newHp = _lastHp * (1 + value / 100f);
        _status.Hp = (int)newHp;
        Debug.Log($"체력이 변경되었습니다. {_status.Hp}");
    }

    // 공격력변할때 이벤트
    public void OnPowerChanged(int value)
    {
        // 공격력 수정
        _status.Power *= (1 + value / 100f);
        Debug.Log($"공격력이 변경되었습니다. {_status.Power}");
    }
    // 방어력변할때 이벤트
    public void OnDefenseChanged(int value)
    {
        // 방어력 수정
        _status.Defense *= (1 + value / 100f);

        Debug.Log($"방어력이 변경되었습니다. {_status.Defense}");
    }

    public void OnCriticalChanged(int value)
    {
        _status.Critical *= (1 + value / 100f);
        Debug.Log($"크리티컬이 변경되었습니다. {_status.Critical}");
    }   
}
