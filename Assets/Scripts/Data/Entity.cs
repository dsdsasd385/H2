using System;
using UnityEngine;
public struct Status
{
    private int _hp;        // ü��
    private float _power;    // ���ݷ�
    private float _defense;  // ����
    private float _critical; // ġ��Ÿ
    private float _speed;    // ����(speed�� �������� ����)


    public event Action<int, int> OnHpChange;
    public event Action<float, float> OnPowerChange;
    public event Action<float, float> OnDefenseChange;
    public event Action<float> OnCriticalChange;
    public event Action<float> OnSpeedChange;

    public int Hp
    {
        get { return _hp; }  // hp ���� ������ ���� �׳� ��ȯ
        set
        {
            int oldHp = _hp;
            // hp ���� 0 ���Ϸ� �������� �ʵ���
            _hp = Mathf.Max(0, value);

            if(oldHp != _hp)
            {
                OnHpChange?.Invoke(oldHp, _hp);

                Debug.Log($"ü���� ����Ǿ����ϴ�. ����ü�� : {oldHp}, ����ü�� : {_hp}");
            }
        }  
    }


    public float Power
    {
        get { return _power; }
        set
        {
            float oldPower = _power;
            _power = Mathf.Max(0, value);
            if(oldPower != _power)
            {
                OnPowerChange?.Invoke(oldPower, _power);
                Debug.Log($"���ݷ��� ����Ǿ����ϴ�. �������ݷ� : {oldPower}, ������ݷ� : {_power}");
            }
        } // ���� 0 �̻� �����ǵ���
    }

    // ���� ������Ƽ
    public float Defense
    {
        get { return _defense; }
        set
        {
            float oldDefense = _defense;
            _defense = Mathf.Max(0, value);
            
            if(oldDefense != _defense)
            {
                OnDefenseChange?.Invoke(oldDefense, _defense);
                Debug.Log($"�� ����Ǿ����ϴ�. �������� : {oldDefense}, ������� : {_defense}");

            }
        } // ���� 0 �̻� �����ǵ���
    }


    // ġ��Ÿ ������Ƽ
    public float Critical
    {
        get { return _critical; }
        set
        {
            _critical = Mathf.Max(0, value);
            OnCriticalChange?.Invoke(_critical);
        } // ���� 0 �̻� �����ǵ���
    }



    // ���� ������Ƽ
    public float Speed
    {
        get { return _speed; }
        set
        {
            _speed = Mathf.Max(0, value);
            OnSpeedChange?.Invoke(_speed);
        } // ���� 0 �̻� �����ǵ���
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

public abstract class Entity : MonoBehaviour
{
    public event Action<int, int> OnHpChange;
    public event Action<float, float> OnPowerChange;
    public event Action<float, float> OnDefenseChange;
    public event Action<float> OnCriticalChange;
    public event Action<float> OnSpeedChange;

    public Status status { get;protected set; }

    public bool myTurn;

    // ���� ����
    protected abstract void SetEntity();

    // ���� ���
    public virtual void Attack(Entity target) { }

    // ���� ����
    public virtual void TakeDamage(float power, float defense, float critical)
    {
        float damage = CalculateDamage(power, defense , critical);
    }

    // ���ط� ���
    protected float CalculateDamage(float power , float defense, float critical)
    {
        float finalDamage = power;
        float criticaMultplier = 2.0f;
        if(UnityEngine.Random.value < critical)
        {
            finalDamage *= criticaMultplier;
            Debug.Log($"ũ��Ƽ�� ������! : {finalDamage}");            
        }

        float defenseRate = defense / (1 + defense);  // ����� ���
        Debug.Log($"������� {defenseRate}");
        
        finalDamage = finalDamage * (1 - defenseRate);
        Debug.Log($"���� �������� {finalDamage}");
        return finalDamage;  // ���� ���ط� ����


    }
    // ��� ó��
    protected virtual void Die() { }
}
