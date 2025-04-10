using UnityEngine;
using System.Threading.Tasks;
using System;


public class Monster : Entity
{
    private Status _status = new(30, 20f, 3f, 0.05f, 0.8f);    
    public Status Status => _status;

    [SerializeField] Animator animator;

    readonly int _exp = 30;

    public Action<int> OnDieGiveExp;
    public Action OnDieEvent;

    private void Awake()
    {
        // status.OnHpChange += MonsterUI.SetHpVar;
        // status.OnOnPowerChange += MonsterUI.SetPower;
        // status.OnDefenseChange += MonsterUI.SetDefense;
        // status.OnCriticalChange += MonsterUI.SetCritical;
        // status.OnSpeedChange += MonsterUI.SetSpeed;
    }

    //public override void SetStatus() { }
    public void UpgradeStatus(float multiplier)
    {
        //float multHp = (float)status.hp * multiplier;
        //status.hp = (int)multHp;
        //status.power *= multiplier;
        //status.defense *= multiplier;
        //status.critical *= multiplier;
        //status.speed *= multiplier;

        _status = _status * multiplier;
    }
        

    public override void TakeDamage(float power, float defense, float critical)
    {        
        float damage = base.CalculateDamage(power, defense, critical);

        int damageInt = (int)damage;

       _status.Hp -= damageInt;
        Debug.Log($"몬스터의 체력은 {_status.Hp}, 받은 데미지는 {damageInt} 입니다.");
    }

    public override void Die()
    {
        // 사망 로직
        //animator.SetTrigger("Die");
        OnDieGiveExp?.Invoke(_exp);
        OnDieEvent?.Invoke();
    }
}
