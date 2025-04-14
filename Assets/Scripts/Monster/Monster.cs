using UnityEngine;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;


public class Monster : Entity
{
    private Status _status = new(30, 20f, 3f, 0.05f, 0.8f);    
    public Status Status => _status;

    [SerializeField] Animator animator;

    readonly int _exp = 30;

    public Action<int> OnDieGiveExp;

    public void UpgradeStatus(float multiplier)
    {        
        _status = _status * multiplier;
    }
        

    public override void TakeDamage(float power, float defense, float critical)
    {        
        float damage = base.CalculateDamage(power, defense, critical);

        int damageInt = (int)damage;

       _status.Hp -= damageInt;

        var text = WorldCanvas.Get<DamageText>(transform.position + Vector3.up, 100);
        text.transform.position = transform.position + Vector3.up;  // 생성 후 재할당
        text.ShowDamage(damageInt);
        Debug.Log($"Transform : {transform.position}");
    }

    public override void Die()
    {
        OnDieGiveExp?.Invoke(_exp);
    }
}
