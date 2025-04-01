using System;
using UnityEngine;
public struct Status
{
    private int _hp;        // 체력
    private float _power;    // 공격력
    private float _defense;  // 방어력
    private float _critical; // 치명타
    private float _speed;    // 선공(speed가 높은쪽이 선공)


    public event Action<int> OnHpChange;
    public event Action<float> OnPowerChange;
    public event Action<float> OnDefenseChange;
    public event Action<float> OnCriticalChange;
    public event Action<float> OnSpeedChange;

    public int Hp
    {
        get { return _hp; }  // hp 값을 가져올 때는 그냥 반환
        set
        {

            _hp = Mathf.Max(0, value);
            OnHpChange?.Invoke(_hp);
        }  // hp 값이 0 이하로 설정되지 않도록
    }


    public float Power
    {
        get { return _power; }
        set
        {
            _power = Mathf.Max(0, value);
            OnPowerChange?.Invoke(_power);
        } // 값이 0 이상만 설정되도록
    }

    // 방어력 프로퍼티
    public float Defense
    {
        get { return _defense; }
        set
        {
            _defense = Mathf.Max(0, value);
            OnDefenseChange?.Invoke(_defense);
        } // 값이 0 이상만 설정되도록
    }


    // 치명타 프로퍼티
    public float Critical
    {
        get { return _critical; }
        set
        {
            _critical = Mathf.Max(0, value);
            OnCriticalChange?.Invoke(_critical);
        } // 값이 0 이상만 설정되도록
    }



    // 선공 프로퍼티
    public float Speed
    {
        get { return _speed; }
        set
        {
            _speed = Mathf.Max(0, value);
            OnSpeedChange?.Invoke(_speed);
        } // 값이 0 이상만 설정되도록
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
    public Status status { get;protected set; }

    public bool myTurn;

    // 스텟 구현
    protected abstract void SetEntity();

    // 공격 기능
    public virtual void Attack(Entity target) { }

    // 피해 입음
    public virtual void TakeDamage(float power, float defense, float critical)
    {
        float damage = CalculateDamage(power, defense , critical);
    }

    // 피해량 계산
    protected float CalculateDamage(float power , float defense, float critical)
    {
        float finalDamage = power;
        float criticaMultplier = 2.0f;
        if(UnityEngine.Random.value < critical)
        {
            finalDamage *= criticaMultplier;
            Debug.Log($"크리티컬 데미지! : {finalDamage}");            
        }

        float defenseRate = defense / (1 + defense);  // 방어율 계산
        Debug.Log($"방어율은 {defenseRate}");
        
        finalDamage = finalDamage * (1 - defenseRate);
        Debug.Log($"최종 데미지는 {finalDamage}");
        return finalDamage;  // 계산된 피해량 리턴


    }
    // 사망 처리
    protected virtual void Die() { }

    // 체력변할때 이벤트
    protected virtual void OnHpChanged(float value) { }


    // 공격력변할때 이벤트
    protected virtual void OnPowerChanged(float value) { }

    // 방어력변할때 이벤트
    protected virtual void OnDefenseChanged(float value) { }

    // 치명타변할때 이벤트
    protected virtual void OnCriticalChanged(float value) { }

    // 스피드변할때 이벤트
    protected virtual void OnSpeedChanged(float value) { }


}
