using NaughtyAttributes;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Player : Entity
{
    #region �̱��� 
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
                    // �̷����� �������� Ȥ�ó��ؼ�
                    Destroy(_instance.gameObject);
                    GameObject player = new GameObject("Player");
                    _instance = player.AddComponent<Player>();
                }

                DontDestroyOnLoad(_instance.gameObject);  // ���� ����Ǿ ����
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
        // UI �̺�Ʈ ����
        // if(OnHpChanged == null)
        // status.OnHpChange += PlayerUI.SetHpVar;
        // status.OnPowerChange += PlayerUI.SetPower;
        // status.OnDefenseChange += PlayerUI.SetDefense;
        // status.OnCriticalChange += PlayerUI.SetCritical;
        // status.OnSpeedChange += PlayerUI.SetSpeed;

        // PlayerItem �̺�Ʈ ����
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
        // ��� ����
        //animator.SetTrigger("Die");
        await Task.Delay(1500);
        Destroy(gameObject);
    }


    // ü�º��Ҷ� �̺�Ʈ
    public void OnHpChanged(float hp, float value)
    {
        // ü�� ����
        var newHp = status.Hp * (value / 100);
        status.Hp = (int)newHp;

    }

    // ���ݷº��Ҷ� �̺�Ʈ
    public void OnPowerChanged(float power, float value)
    {
        // ���ݷ� ����
        status.Power *= (value /100);
        Debug.Log($"���ݷ��� ����Ǿ����ϴ�. {status.Power}");
    }
    // ���º��Ҷ� �̺�Ʈ
    public void OnDefenseChanged(float defense, float value)
    {
        // ���� ����
        status.Defense *= (value / 100);

        Debug.Log($"������ ����Ǿ����ϴ�. {status.Defense}");
    }

    public void OnCriticalChanged(float value)
    {
        status.Critical *= value;
        Debug.Log($"ũ��Ƽ���� ����Ǿ����ϴ�. {status.Critical}");
    }

    
}
