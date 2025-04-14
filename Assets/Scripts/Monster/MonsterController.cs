using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Monster _monster;
    public Monster Monster => _monster;
    public Status Status => _monster.Status;

    private MonsterAnimationHandler _monsterAni;

    private Player _playerTarget;

    private float _moveSpeed;
    private Vector3 _originPos;
    // Start is called before the first frame update
    //void Awake()
    //{
    //    _monster = new Monster();

    //    _monster.transform = transform;

    //    _monsterAni = GetComponent<MonsterAnimationHandler>();

    //    _originPos = transform.position;

    //    SubscribeToEvents();
    //}


    //private void OnDestroy()
    //{
    //    UnsubscribeFromEvents();
    //} 
    public void Init()
    {
        _monster = new Monster();

        _monster.transform = transform;

        _monsterAni = GetComponent<MonsterAnimationHandler>();

        _originPos = transform.position;

        SubscribeToEvents();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    public void SubscribeToEvents()
    {
        if (_monsterAni != null && Monster != null)
        {
            Monster.OnDieGiveExp += GievExp;
        }
    }

    public void UnsubscribeFromEvents()
    {
        if (_monsterAni != null && Monster != null)
        {
            Monster.OnDieGiveExp -= GievExp;

        }
    }

    private void GievExp(int exp)
    {
        if (_playerTarget != null)
        {
            _playerTarget.AddExp(exp);
            Debug.Log($"플레이어 경험치 획득 : {exp}");

        }
        else
        {
            Debug.LogWarning("_playerTarget 이 없습니다.");
        }
    }
    public IEnumerator MonsterMoveToTarget(Transform transform, Vector3 targetTransform, float speed)
    {
        yield return _monsterAni.PlayRunAni();

        _moveSpeed = speed;
        Vector3 targetPos = new Vector3(targetTransform.x +3, targetTransform.y, targetTransform.z);

        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator ReturnMovePlayer(Transform transform, Vector3 targetTransform, float speed)
    {
        yield return _monsterAni.PlayRunAni();

        _moveSpeed = speed;
        Vector3 targetPos = targetTransform;

        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        yield return _monsterAni.PlayIdle();
    }

    public IEnumerator MonsterAttackSequence(PlayerController player, MonsterController monster)
    {
        Debug.Log("몬스터가 공격했습니다..");

        yield return _monsterAni.PlayAttackAni();

        yield return player.TakeDamageSequence(monster.Monster);

        yield return ReturnMovePlayer(transform, _originPos, _moveSpeed);
    }
    public IEnumerator TakeDamageSequence(Entity attacker)
    {
        Debug.Log("몬스터가 데미지입었습니다..");
        yield return _monsterAni.PlayDamagedAni();

        if (_playerTarget == null && attacker is Player player)
            _playerTarget = player;

        if (_monster != null)
        {
            _monster.TakeDamage(_playerTarget.Status.Power, Status.Defense, _playerTarget.Status.Critical);

        }

        if (_monster.Status.Hp <= 0)
        {
            yield return _monsterAni.PlayDieAni();
            _monster.Die();
            MonsterPoolManager.Instance.DespawnMonster(gameObject);
        }
    }
}
