using UnityEngine;
using System.Threading.Tasks;


public class Monster : Entity
{
    public Status status;

    [SerializeField] Animator animator;

    private void Awake()
    {
        // status.OnHpChange += MonsterUI.SetHpVar;
        // status.OnOnPowerChange += MonsterUI.SetPower;
        // status.OnDefenseChange += MonsterUI.SetDefense;
        // status.OnCriticalChange += MonsterUI.SetCritical;
        // status.OnSpeedChange += MonsterUI.SetSpeed;
        SetEntity();
    }
    protected override void SetEntity()
    {
        status = new(50, 50f, 10f, 0.05f, 1f);
        //status.hp = 150;
        //status.power = 50f;
        //status.defense = 10f;
        //status.critical = 0.5f;
        //status.speed = 1;
    }

    public void UpgradeStatus(float multiplier)
    {
        //float multHp = (float)status.hp * multiplier;
        //status.hp = (int)multHp;
        //status.power *= multiplier;
        //status.defense *= multiplier;
        //status.critical *= multiplier;
        //status.speed *= multiplier;

        status = status * multiplier;
    }

    public override void Attack(Entity target)
    {
        Player playerTarget = target as Player;

        if (target is Player)
        {
            //animator.SetTrigger("Attack");

            target.TakeDamage(status.Power, playerTarget.status.Defense, status.Critical);
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

        if (damage > status.Hp)
        {
            Die();
        }

        int damageInt = (int)damage;

        status.Hp -= damageInt;
    }

    protected async override void Die()
    {
        // »ç¸Á ·ÎÁ÷
        //animator.SetTrigger("Die");
        await Task.Delay(1500);

        if(gameObject != null) 
        Destroy(gameObject);
    }
}
