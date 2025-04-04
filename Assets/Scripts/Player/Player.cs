using System;
using System.Threading.Tasks;
using UnityEngine;


public class Player : Entity
{
    private Status _status;
    public ref Status Status => ref _status;

    private int _lastHp;


    //public override void SetStatus() {; }

    public void Init()
    {
        _status = new Status(50, 30f, 5f, 0.05f, 1f);
    }

    public override void Attack(Entity target)
    {
        Monster monsterTarget = target as Monster;

        if (target is Monster)
        {
            //animator.SetTrigger("Attack");
            target.TakeDamage(_status.Power, monsterTarget.Status.Defense, _status.Critical);
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
        // ��� ����
        //animator.SetTrigger("Die");
        await Task.Delay(1500);
        //Destroy(gameObject);
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

    


 

    //private void ReturnOidScene()
    //{
    //    SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(_oldScene.name));
    //}  
}
