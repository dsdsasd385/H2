using NaughtyAttributes;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Player : Entity
{
    #region 싱글톤 
    private static Player _instance;

    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Player>();

                if (_instance == null)
                {
                    GameObject player = new GameObject("Player");
                    _instance = player.AddComponent<Player>();

                }

                else
                {
                    // 이럴일은 없겠지만 혹시나해서
                    Destroy(_instance.gameObject);
                    GameObject player = new GameObject("Player");
                    _instance = player.AddComponent<Player>();
                }

                DontDestroyOnLoad(_instance.gameObject);  // 씬이 변경되어도 유지
            }

            return _instance;
        }
    }
    #endregion


    /**********************************************************************************/
    /**********************************************************************************/

    public Status status;
    [SerializeField] Animator animator;


    private void Awake()
    {
        // UI 이벤트 연결
        // if(OnHpChanged == null)
        // status.OnHpChange += PlayerUI.SetHpVar;
        // status.OnPowerChange += PlayerUI.SetPower;
        // status.OnDefenseChange += PlayerUI.SetDefense;
        // status.OnCriticalChange += PlayerUI.SetCritical;
        // status.OnSpeedChange += PlayerUI.SetSpeed;

        // PlayerItem 이벤트 연결
        // 
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
        // 사망 로직
        //animator.SetTrigger("Die");
        await Task.Delay(1500);
        Destroy(gameObject);
    }


    // 체력변할때 이벤트
    public void OnHpChanged(float hp, float value)
    {
        // 체력 수정
        var newHp = status.Hp * (value / 100);
        status.Hp = (int)newHp;

    }

    // 공격력변할때 이벤트
    public void OnPowerChanged(float power, float value)
    {
        // 공격력 수정
        status.Power *= (value /100);
        Debug.Log($"공격력이 변경되었습니다. {status.Power}");
    }
    // 방어력변할때 이벤트
    public void OnDefenseChanged(float defense, float value)
    {
        // 방어력 수정
        status.Defense *= (value / 100);

        Debug.Log($"방어력이 변경되었습니다. {status.Defense}");
    }

    public void OnCriticalChanged(float value)
    {
        status.Critical *= value;
        Debug.Log($"크리티컬이 변경되었습니다. {status.Critical}");
    }

    
}
