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

    // Start is called before the first frame update
    void Awake()
    {
        _monster = new Monster();
        _monsterAni = GetComponent<MonsterAnimationHandler>();
        

        SubscribeToEvents();
    }
    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    public void SubscribeToEvents() 
    {
        if(_monsterAni != null && Monster != null)
        {
            Debug.Log($"_monsterAni가 {_monsterAni} 이며 Monster가 {Monster}입니다.");

            Monster.OnDieGiveExp += GievExp;
            Monster.OnDieEvent += DieEvent;
        }
    }

    public void UnsubscribeFromEvents()
    {
        if(_monsterAni != null && Monster != null)
        {
            Monster.OnDieGiveExp -= GievExp;
            Monster.OnDieEvent -= DieEvent;

        }
    }

    private void GievExp(int exp)
    {
        if (_playerTarget != null)
        {
            _playerTarget.AddExp(exp);
            Debug.Log($"플레이어에게 {exp}경험치를 주었습니다.");

        }
        else
        {
            Debug.LogWarning("경험치 지급 대상이 없습니다.");
        }
    }

    public IEnumerator MonsterAttackSequence(PlayerController player, MonsterController monster)
    {
        Debug.Log("몬스터가 공격합니다.");

        _monsterAni.PlayAttackAni();

        yield return new WaitForSeconds(0.5f); // => 애니메이션 시간에 맞춰 시간설정

        StartCoroutine(player.TakeDamageSequence(monster.Monster));// 필요한 데이터가 power,defense,critical
    }
    public IEnumerator TakeDamageSequence(Entity attacker)
    {
        Debug.Log("몬스터가 맞았습니다.");
        yield return _monsterAni.PlayDamagedAni();

        if (_playerTarget == null && attacker is Player player)
            _playerTarget = player;

        if(_monster != null)
        {
            _monster.TakeDamage(_playerTarget.Status.Power, Status.Defense, _playerTarget.Status.Critical);

        }

        if (_monster.Status.Hp <= 0)
        {
            _monster.Die();
        }
    }

    private void DieEvent()
    {
        _monsterAni.PlayDieAni();
        Destroy(gameObject, 2.5f);
    }

}
