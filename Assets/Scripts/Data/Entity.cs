using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public struct Status
{
    private int _hp;        // ü��
    private float _power;    // ���ݷ�
    private float _defense;  // ����
    private float _critical; // ġ��Ÿ
    private float _speed;    // ����(speed�� �������� ����)


    public event Action<int> OnHpChanged;
    public event Action<int> OnPowerChanged;
    public event Action<int> OnDefenseChanged;
    public event Action<int> OnCriticalChanged;
    public event Action<int> OnSpeedChanged;

    public int Hp
    {
        get { return _hp; }  // hp ���� ������ ���� �׳� ��ȯ
        set
        {

            _hp = Mathf.Max(0, value);
            OnHpChanged?.Invoke(_hp);
        }  // hp ���� 0 ���Ϸ� �������� �ʵ���
    }


    public float Power
    {
        get { return _power; }
        set
        {
            _power = Mathf.Max(0, value);
            OnPowerChanged?.Invoke(_hp);
        } // ���� 0 �̻� �����ǵ���
    }

    // ���� ������Ƽ
    public float Defense
    {
        get { return _defense; }
        set
        {
            _defense = Mathf.Max(0, value);
            OnDefenseChanged?.Invoke(_hp);
        } // ���� 0 �̻� �����ǵ���
    }


    // ġ��Ÿ ������Ƽ
    public float Critical
    {
        get { return _critical; }
        set
        {
            _critical = Mathf.Max(0, value);
            OnCriticalChanged?.Invoke(_hp);
        } // ���� 0 �̻� �����ǵ���
    }



    // ���� ������Ƽ
    public float Speed
    {
        get { return _speed; }
        set
        {
            _speed = Mathf.Max(0, value);
            OnSpeedChanged?.Invoke(_hp);
        } // ���� 0 �̻� �����ǵ���
    }


    public Status(int hp, float power, float defense, float critical, float speed)
    {
        _hp = Mathf.Max(0, hp);
        _power = power;
        _defense = defense;
        _critical = critical;
        _speed = speed;

        OnHpChanged = null;
        OnCriticalChanged = null;
        OnDefenseChanged = null;
        OnPowerChanged = null;
        OnSpeedChanged = null;
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
    Status status;

    public bool myTurn;

    // ���� ����
    protected abstract void SetEntity();

    // ���� ���
    public virtual void Attack(Entity target) { }

    // ���� ����
    public virtual void TakeDamage(float power)
    {
        float damage = CalculateDamage(power);
    }

    // ���ط� ���
    protected float CalculateDamage(float power)
    {
        float defenseRate = status.Defense / (1 + status.Defense);  // ����� ���
        return power * (1 - defenseRate);  // ���� ���ط� ����
    }
    // ��� ó��
    protected virtual void Die() { }

    // ü�º��Ҷ� �̺�Ʈ
    protected virtual void OnHpChanged(float value) { }


    // ���ݷº��Ҷ� �̺�Ʈ
    protected virtual void OnPowerChanged(float value) { }

    // ���º��Ҷ� �̺�Ʈ
    protected virtual void OnDefenseChanged(float value) { }

    // ġ��Ÿ���Ҷ� �̺�Ʈ
    protected virtual void OnCriticalChanged(float value) { }

    // ���ǵ庯�Ҷ� �̺�Ʈ
    protected virtual void OnSpeedChanged(float value) { }


}
