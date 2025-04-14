using UnityEngine;
using UnityEngine.Pool;

public class MonsterPoolManager : MonoBehaviour
{
    // 수정 여부: 새로 생성함
    // 한줄 설명: 몬스터 오브젝트를 ObjectPool로 관리하는 매니저

    public static MonsterPoolManager Instance; // 싱글톤 접근용

    [Header("풀링할 몬스터 프리팹")]
    public GameObject monsterPrefab;

    private ObjectPool<GameObject> pool;

    private void Awake()
    {
        // 싱글톤 패턴
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 풀 생성
        pool = new ObjectPool<GameObject>(
            createFunc: CreateMonster,
            actionOnGet: OnGetMonster,
            actionOnRelease: OnReleaseMonster,
            actionOnDestroy: OnDestroyMonster,
            collectionCheck: false,
            defaultCapacity: 3,
            maxSize: 10
        );
    }

    // 몬스터 생성
    private GameObject CreateMonster()
    {
        GameObject monsterPrefab = Resources.Load<GameObject>("Monster");
        GameObject monster = Instantiate(monsterPrefab);
        monster.SetActive(false);
        return monster;
    }

    // 꺼낼 때 처리
    private void OnGetMonster(GameObject monster)
    {
        monster.SetActive(true);
        // 필요 시 초기화 함수 호출
        var controller = monster.GetComponent<MonsterController>();
        controller?.Init(); // Init()은 직접 정의해줘야 함
    }

    // 반납할 때 처리
    private void OnReleaseMonster(GameObject monster)
    {
        monster.SetActive(false);
    }

    // 진짜 Destroy할 때 처리
    private void OnDestroyMonster(GameObject monster)
    {
        Destroy(monster);
    }

    // 외부에서 몬스터 꺼내기
    public GameObject SpawnMonster(Vector3 position, Quaternion rotation)
    {
        GameObject monster = pool.Get();
        monster.transform.SetPositionAndRotation(position, rotation);
        return monster;
    }

    // 외부에서 몬스터 회수하기
    public void DespawnMonster(GameObject monster)
    {
        pool.Release(monster);
    }
}
