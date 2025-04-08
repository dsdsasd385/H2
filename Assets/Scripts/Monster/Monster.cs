using UnityEngine;
using System.Threading.Tasks;
using System;


public class Monster : Entity
{
    private Status _status = new(30, 20f, 3f, 0.05f, 0.8f);    
    public ref Status Status => ref _status;

    [SerializeField] Animator animator;
    public Player _playerTarget { get; private set; }

    private int _exp = 30;


    public Action OnPlayAttackAnimation;
    public Action OnPlayDamagedAnimation;
    public Action OnPlayDieAnimation;
    public Action OnPlayDieAction;
    public Action<int> OnDieGiveExp;

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

            target.TakeDamage(_status.Power, _playerTarget.Status.Defense, _status.Critical,this);
            OnPlayAttackAnimation?.Invoke();

        }

        else
        {
            return;
        }
    }

    public override void TakeDamage(float power, float defense, float critical, Entity attacker)
    {
        if (_playerTarget == null && attacker is Player player)
            _playerTarget = player;

        //animator.SetTrigger("Damaged");

        float damage = base.CalculateDamage(power, defense, critical);

        if (damage > _status.Hp)
        {
            Die();
        }
        OnPlayDamagedAnimation?.Invoke();


        int damageInt = (int)damage;

       _status.Hp -= damageInt;
    }

    protected async override void Die()
    {
        // 사망 로직
        //animator.SetTrigger("Die");
        OnPlayDieAnimation?.Invoke();
        OnPlayDieAction?.Invoke();
        OnDieGiveExp?.Invoke(_exp);

  // 경험치 주기

        //if (gameObject != null)
        //    Destroy(this.gameObject);
    }
}
