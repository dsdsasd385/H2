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
        // 사망 로직
        //animator.SetTrigger("Die");
        await Task.Delay(1500);
        //Destroy(gameObject);
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

    


 

    //private void ReturnOidScene()
    //{
    //    SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(_oldScene.name));
    //}  
}
