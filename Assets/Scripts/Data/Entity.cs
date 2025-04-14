using System;
using UnityEngine;

public class Status
{
    private int   _maxHp;
    private int   _hp;        
    private float _power;  
    private float _defense;  
    private float _critical;
    private float _speed;   
    
    public event Action<int, int>   OnHpChange;
    public event Action<float> OnPowerChange;
    public event Action<float> OnDefenseChange;
    public event Action<float> OnCriticalChange;
    public event Action<float> OnSpeedChange;
    
    public int MaxHp
    {
        get => _maxHp;
        set => _maxHp = Mathf.Max(0, value);
    }

    public int Hp
    {
        get { return _hp; }  
        set
        {
            int oldHp = _hp;
            _hp = Mathf.Max(0, value);
            OnHpChange?.Invoke(_hp, MaxHp);
            Debug.Log($"이전 체력은 : {oldHp}, 새로운 체력은: {_hp}");
        }
    }


    public float Power
    {
        get { return _power; }
        set
        {
            float oldPower = _power;
            _power = Mathf.Max(0, value);
            OnPowerChange?.Invoke(_power);
            Debug.Log($"이전 공격력은 : {oldPower}, 새로운 공격력은 : {_power}");
        }
    }

    public float Defense
    {
        get { return _defense; }
        set
        {
            float oldDefense = _defense;
            _defense = Mathf.Max(0, value);
            OnDefenseChange?.Invoke(_defense);
            Debug.Log($"이전 방어력은 : {oldDefense}, 새로운 방어력은 : {_defense}");


        } 
    }

    public float Critical
    {
        get { return _critical; }
        set
        {
            _critical = Mathf.Max(0, value);
            OnCriticalChange?.Invoke(_critical);
        } 
    }



    public float Speed
    {
        get { return _speed; }
        set
        {
            _speed = Mathf.Max(0, value);
            OnSpeedChange?.Invoke(_speed);
        }
    }

    public StatusData ToStatusData()
    {
        return new StatusData
        {
            hp = Hp,
            power = Power,
            defense = Defense,
            critical = Critical,
            speed = Speed
        };
    }

    public Status(StatusData data)
    {
        MaxHp = Hp = data.hp;
        Power = data.power;
        Defense = data.defense;
        Critical = data.critical;
        Speed = data.speed;
    }

    public Status(int hp, float power, float defense, float critical, float speed)
    {
        _hp = Mathf.Max(0, hp);
        _power = power;
        _defense = defense;
        _critical = critical;
        _speed = speed;

        OnHpChange = null;
        OnCriticalChange = null;
        OnDefenseChange = null;
        OnPowerChange = null;
        OnSpeedChange = null;
    }

    public static Status operator *(Status status, float multiplier)
    {
        status.Hp *= (int)multiplier;
        status.Power *= multiplier;
        status.Defense *= multiplier;
        status.Critical *= multiplier;
        status.Speed *= multiplier;

        return status;
    }
}

public abstract class Entity
{
    public Status Status { get; protected set; }

    public bool IsFreeze { get; private set; } // 빙결 상태 여부
    public int FreezeTurn { get; private set; } // 빙결 턴
    public bool IsPoison { get; private set; } // 중독 상태 여부
    public int PoisonTurn { get; private set; } // 중독 턴

    private float _poisonDamage; // 중독데미지


    public virtual void Attack(Entity target) { }
    public virtual void Heal(int healAmount) { }

    #region Poison 독 

    public virtual void ApplyPoison(int poisonTurn, float damagePerTurn)
    {
        IsPoison = true;
        PoisonTurn = poisonTurn;
        _poisonDamage = damagePerTurn;
        Debug.Log($"중독 효과 적용: {poisonTurn} 턴 동안, 턴 당 {_poisonDamage} 피해");
    }

    public void TryApplyPoison(float poisonChance, int poisonTurn, float poisonDamagePerTurn)
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);

        if (randomValue <= poisonChance)
        {
            ApplyPoison(poisonTurn, poisonDamagePerTurn); // 중독 상태 적용
            Debug.Log("중독 상태 적용!");
        }
        else
        {
            Debug.Log("중독 상태 미적용.");
        }
    }
    #endregion

    #region Freeze 빙결

    public void TryApplyFreeze(float freezeChance)
    {
        // 0부터 1 사이의 랜덤 값 생성 (0.0f ~ 1.0f)
        float randomValue = UnityEngine.Random.Range(0f, 1f);

        // 랜덤 값이 주어진 확률(예: 10%) 이상일 경우 빙결 적용
        if (randomValue <= freezeChance)
        {
            ApplyFreeze(1); // 1턴 동안 빙결 상태 적용
            Debug.Log("빙결 상태 적용!");
        }
        else
        {
            Debug.Log("빙결 상태 미적용.");
        }
    }
    public void ApplyFreeze(int turnCount)
    {
        IsFreeze = true;
        FreezeTurn = turnCount;
        Debug.Log($"빙결 효과 적용: {FreezeTurn} 턴 동안 빙결");

    }
    #endregion

    public void EndTurnUpdate()
    {
        if (IsFreeze)
        {
            FreezeTurn--;  // 턴 차감
            Debug.Log($"빙결상태 턴차감! 남은턴은 : {PoisonTurn} 입니다.");

            if (FreezeTurn <= 0)
            {
                IsFreeze = false;  // 빙결 상태 종료
                Debug.Log($"빙결상태종료! IsFreeze : {IsFreeze}");

            }
        }

        if (IsPoison)
        {
            PoisonTurn--;  // 턴 차감
            Debug.Log($"중독상태 턴차감! 남은턴은 : {PoisonTurn} 입니다.");

            Status.Hp -= (int)_poisonDamage;
            Debug.Log($"중독되어 체력이 {(int)_poisonDamage}만큼 감소하여 {Status.Hp}가 되었습니다.");

            if (PoisonTurn <= 0)
            {
                IsPoison = false;  // 중독 상태 종료
                Debug.Log($"중독상태종료! IsPoison : {IsPoison}");

            }
        }
    }

    public abstract void TakeDamage(float power, float defense, float critical);
    
    public Transform transform { get; set; }


    public float CalculateDamage(float power, float defense, float critical)
    {
        float finalDamage = power;
        float criticaMultplier = 2.0f;
        if (UnityEngine.Random.value < critical)
        {
            finalDamage *= criticaMultplier;
            Debug.Log($"크리티컬 공격력은 : {finalDamage}");
        }

        float defenseRate = defense / (1 + defense);  

        finalDamage = finalDamage * (1 - defenseRate);
        Debug.Log($"총 데미지는 {finalDamage}");
        return finalDamage;  
    }

    public virtual void Die() { }
}
