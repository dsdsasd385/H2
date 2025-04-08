using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Monster _monster;
    public Monster Monster => _monster;
    public Status status => _monster.Status;

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

            Monster.OnPlayAttackAnimation += _monsterAni.PlayAttackAni;
            Monster.OnPlayDamagedAnimation += _monsterAni.PlayDamagedAni;
            Monster.OnPlayDieAnimation += _monsterAni.PlayDieAni;
            Monster.OnPlayDieAction += DestroyObject;
            Monster.OnDieGiveExp += GievExp;
        }
    }

    public void UnsubscribeFromEvents()
    {
        if(_monsterAni != null && Monster != null)
        {
            Monster.OnPlayAttackAnimation -= _monsterAni.PlayAttackAni;
            Monster.OnPlayDamagedAnimation -= _monsterAni.PlayDamagedAni;
            Monster.OnPlayDieAnimation -= _monsterAni.PlayDieAni;
            Monster.OnPlayDieAction -= DestroyObject;
            Monster.OnDieGiveExp -= GievExp;

        }
    }

    private void GievExp(int exp)
    {
        _playerTarget = Monster._playerTarget;

        _playerTarget.AddExp(exp);

        Debug.Log($"플레이어에게 {exp}경험치를 주었습니다.");
    }

    private void DestroyObject()
    {
        Destroy(gameObject, 2.5f);
    }
}
