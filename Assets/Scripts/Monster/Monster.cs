using UnityEngine;
using System.Threading.Tasks;


public class Monster : Entity
{
    public Status _status = new(30, 20f, 3f, 0.05f, 0.8f);


    [SerializeField] Animator animator;
    private Player _playerTarget;

    private int _exp = 30;


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

    public override void Attack(Entity target)
    {
        _playerTarget = target as Player;

        if (target is Player)
        {
            //animator.SetTrigger("Attack");

            target.TakeDamage(_status.Power, _playerTarget._status.Defense, _status.Critical);
        }

        else
        {
            return;
        }
    }

    public override void TakeDamage(float power, float defense, float critical)
    {

        //animator.SetTrigger("Damaged");

        float damage = base.CalculateDamage(power, defense, critical);

        if (damage > _status.Hp)
        {
            Die();
        }

        int damageInt = (int)damage;

       _status.Hp -= damageInt;
    }

    protected async override void Die()
    {
        // 사망 로직
        //animator.SetTrigger("Die");
        
        await Task.Delay(1500);
  // 경험치 주기

        //if (gameObject != null)
        //    Destroy(this.gameObject);
    }
}
