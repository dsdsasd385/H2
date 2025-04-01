using NaughtyAttributes;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Player : Entity
{
    public Status status;
    [SerializeField] Animator animator;

    private void Awake()
    {
        // UI �̺�Ʈ ����
        // if(OnHpChanged == null)
        // status.OnHpChange += PlayerUI.SetHpVar;
        // status.OnPowerChange += PlayerUI.SetPower;
        // status.OnDefenseChange += PlayerUI.SetDefense;
        // status.OnCriticalChange += PlayerUI.SetCritical;
        // status.OnSpeedChange += PlayerUI.SetSpeed;
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
    

    public override void Attack(Entity target)
    {
        Monster monsterTarget = target as Monster;

        if (target is Monster)
        {
            //animator.SetTrigger("Attack");
            target.TakeDamage(status.Power, monsterTarget.status.Defense, status.Critical);
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
        // ��� ����
        //animator.SetTrigger("Die");
        await Task.Delay(1500);
        Destroy(gameObject);
    }



    // ü�º��Ҷ� �̺�Ʈ
    protected override void OnHpChanged(float value)
    {
        // ü�� ����
        var newHp = status.Hp * value;
        status.Hp = (int)newHp;
    }



    // ���ݷº��Ҷ� �̺�Ʈ
    protected override void OnPowerChanged(float value)
    {
        // ���ݷ� ����
        status.Power *= value;
    }

    protected override void OnCriticalChanged(float value)
    {
        status.Critical *= value;
    }

    protected virtual void OnSpeedChanged(float value) 
    { 
        status.Speed *= value;
    }


    // ���º��Ҷ� �̺�Ʈ
    protected override void OnDefenseChanged(float value)
    {
        // ���� ����

        status.Defense *= value;
    }

}
