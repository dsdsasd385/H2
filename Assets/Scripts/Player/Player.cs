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

                DontDestroyOnLoad(_instance.gameObject);  // ���� ����Ǿ ����
            }

            return _instance;
        }
    }
    #endregion


    /**********************************************************************************/
    /**********************************************************************************/

    public Status status = new(50, 50f, 10f, 0.05f, 1f);
    [SerializeField] Animator animator;

    public int _lastHp;
    private PlayerUI _playerUI;
    private PlayerItem _playerItem;

    private void Awake()
    {
        // �ߺ�����
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _playerUI = GetComponent<PlayerUI>();
        _playerItem = GetComponent<PlayerItem>();
        // UI �̺�Ʈ ����
        // PlayerEventChaining���� ü�̴�


        // PlayerItem �̺�Ʈ ����
        // 
        //SetEntity();
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


    // ü�º��Ҷ� �̺�Ʈ
    public void OnHpChanged(float hp, int value)
    {
        // ���� ü�� ����
        _lastHp = status.Hp;
        // ü�� ����
        var newHp = _lastHp * (1 + value / 100f);
        status.Hp = (int)newHp;
    }

    // ���ݷº��Ҷ� �̺�Ʈ
    public void OnPowerChanged(float power, int value)
    {
        // ���ݷ� ����
        status.Power *= (1 + value / 100f);
        Debug.Log($"���ݷ��� ����Ǿ����ϴ�. {status.Power}");
    }
    // ���º��Ҷ� �̺�Ʈ
    public void OnDefenseChanged(float defense, int value)
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
