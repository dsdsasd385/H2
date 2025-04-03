using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class Player : Entity
{
    public static bool loadToPlayer;

    private StageRouletteType _stageRouletteTypes;

    public Status status = new(50, 50f, 10f, 0.05f, 1f);
    [SerializeField] Animator animator;

    public int _lastHp;

    private PlayerUI _playerUI;
    private event Action<RouletteResult> roulette;
    private Transform _oldTransform;
    private Scene _oldScene;


    private void Awake()
    {
        _oldScene = SceneManager.GetActiveScene();
        Debug.Log($"OidScene Name : {_oldScene.name}");

        _playerUI = GetComponent<PlayerUI>();
        // UI �̺�Ʈ ����
        // PlayerEventChaining���� ü�̴�


        // PlayerItem �̺�Ʈ ����
        // 
        //SetEntity();
    }

    private void OnEnable()
    {
        Chapter.RouletteResultChangedEvent += SetStatus;
        Battle.BattleStart += MoveToBattleScene;
        Battle.BattleEnd += MoveToBattleScene;
    }

    private void OnDisable()
    {
        Chapter.RouletteResultChangedEvent -= SetStatus;
        Battle.BattleStart -= MoveToBattleScene;
        Battle.BattleEnd -= MoveToBattleScene;
    }
    private void OnDestroy()
    {
        Chapter.RouletteResultChangedEvent -= SetStatus;
        Battle.BattleStart -= MoveToBattleScene;
        Battle.BattleEnd -= MoveToBattleScene;

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
        // ��� ����
        //animator.SetTrigger("Die");
        await Task.Delay(1500);
        Destroy(gameObject);
    }

    private void MoveToBattleScene()
    {
        if (Battle._battle && Battle.IsBattle)
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName("BattleScene"));
        }
        else
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(_oldScene.name));
        }
    }

    private void ReturnOidScene()
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(_oldScene.name));
    }

    private void SetStatus(RouletteResult result)
    {
        switch (result.Type)
        {
            case StageRouletteType.EXERCISE:
                OnHpChanged(result.ChangeValue);
                Debug.Log($"ü�� ����! {result.ChangeValue}%");
                break;
            case StageRouletteType.RESHARPENING_WEAPON:
                OnPowerChanged(result.ChangeValue);
                Debug.Log($"���ݷ� ����! {result.ChangeValue}%");
                break;
            case StageRouletteType.CLEANING_ARMOR:
                OnDefenseChanged(result.ChangeValue);
                Debug.Log($"���� ����! {result.ChangeValue}%");
                break;
            case StageRouletteType.BUG_BITE:
                OnHpChanged(result.ChangeValue);
                Debug.Log($"ü�� ����! {result.ChangeValue} %");
                break;
            case StageRouletteType.BROKEN_WEAPON:
                OnPowerChanged(result.ChangeValue);
                Debug.Log($"���ݷ� ����! {result.ChangeValue} %");
                break;
            case StageRouletteType.LOOSEN_ARMOR:
                OnDefenseChanged(result.ChangeValue);
                Debug.Log($"���� ����! {result.ChangeValue} %");
                break;
        }
    }
    // ü�º��Ҷ� �̺�Ʈ
    public void OnHpChanged(int value)
    {
        // ���� ü�� ����
        _lastHp = status.Hp;
        // ü�� ����
        var newHp = _lastHp * (1 + value / 100f);
        status.Hp = (int)newHp;
        Debug.Log($"ü���� ����Ǿ����ϴ�. {status.Hp}");
    }

    // ���ݷº��Ҷ� �̺�Ʈ
    public void OnPowerChanged(int value)
    {
        // ���ݷ� ����
        status.Power *= (1 + value / 100f);
        Debug.Log($"���ݷ��� ����Ǿ����ϴ�. {status.Power}");
    }
    // ���º��Ҷ� �̺�Ʈ
    public void OnDefenseChanged(int value)
    {
        // ���� ����
        status.Defense *= (1 + value / 100f);

        Debug.Log($"������ ����Ǿ����ϴ�. {status.Defense}");
    }

    public void OnCriticalChanged(int value)
    {
        status.Critical *= (1 + value / 100f);
        Debug.Log($"ũ��Ƽ���� ����Ǿ����ϴ�. {status.Critical}");
    }


}
