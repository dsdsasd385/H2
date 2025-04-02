using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Player : Entity
{

    private StageRouletteType _stageRouletteTypes;

    public Status status = new(50, 50f, 10f, 0.05f, 1f);
    [SerializeField] Animator animator;

    public int _lastHp;
    private PlayerUI _playerUI;

    private event Action<RouletteResult> roulette;


    private void Awake()
    {

        _playerUI = GetComponent<PlayerUI>();
        // UI 이벤트 연결
        // PlayerEventChaining에서 체이닝


        // PlayerItem 이벤트 연결
        // 
        //SetEntity();
    }

    private void OnEnable()
    {
        Chapter.RouletteResultChangedEvent += SetStatus;
    }

    private void OnDisable()
    {
        Chapter.RouletteResultChangedEvent -= SetStatus;
    }
    protected override void SetEntity()
    {
        //status = new(50, 50f, 10f, 0.05f, 1f);
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
        // 사망 로직
        //animator.SetTrigger("Die");
        await Task.Delay(1500);
        Destroy(gameObject);
    }



    private void SetStatus(RouletteResult result)
    {
        switch (result.Type)
        {
            case StageRouletteType.EXERCISE:
                OnHpChanged(result.ChangeValue);
                Debug.Log($"체력 증가! {result.ChangeValue}%");
                break;
            case StageRouletteType.RESHARPENING_WEAPON:
                OnPowerChanged(result.ChangeValue);
                Debug.Log($"공격력 증가! {result.ChangeValue}%");
                break;
            case StageRouletteType.CLEANING_ARMOR:
                OnDefenseChanged(result.ChangeValue);
                Debug.Log($"방어력 증가! {result.ChangeValue}%");
                break; 
            case StageRouletteType.BUG_BITE:
                OnHpChanged(result.ChangeValue);
                Debug.Log($"체력 감소! {result.ChangeValue} %");
                break;
            case StageRouletteType.BROKEN_WEAPON:
                OnPowerChanged(result.ChangeValue);
                Debug.Log($"공격력 감소! {result.ChangeValue} %");
                break;
            case StageRouletteType.LOOSEN_ARMOR:
                OnDefenseChanged(result.ChangeValue);
                Debug.Log($"방어력 감소! {result.ChangeValue} %");
                break;
        }
    }
    // 체력변할때 이벤트
    public void OnHpChanged(int value)
    {
        // 이전 체력 저장
        _lastHp = status.Hp;
        // 체력 수정
        var newHp = _lastHp * (1 + value / 100f);
        status.Hp = (int)newHp;
        Debug.Log($"체력이 변경되었습니다. {status.Hp}");
    }

    // 공격력변할때 이벤트
    public void OnPowerChanged(int value)
    {
        // 공격력 수정
        status.Power *= (1 + value / 100f);
        Debug.Log($"공격력이 변경되었습니다. {status.Power}");
    }
    // 방어력변할때 이벤트
    public void OnDefenseChanged(int value)
    {
        // 방어력 수정
        status.Defense *= (1 + value / 100f);

        Debug.Log($"방어력이 변경되었습니다. {status.Defense}");
    }

    public void OnCriticalChanged(int value)
    {
        status.Critical *= (1 + value / 100f);
        Debug.Log($"크리티컬이 변경되었습니다. {status.Critical}");
    }


}
