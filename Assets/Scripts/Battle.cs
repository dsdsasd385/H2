using DG.Tweening;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public static class Battle
{
    public static BattleScene _battle { get; private set; }
    public static event Action BattleStart;
    public static event Action BattleEnd;
    public static bool IsBattle { get; private set; }
    public static IEnumerator BattleCoroutine(float growthRate)
    {
        yield return AdditiveScene.LoadSceneAsync<BattleScene>(990, 930, 200, loadedScene => _battle = loadedScene);

        IsBattle = true;


        BattleStart?.Invoke();

        StagePlayUI.AddDialog("<color=red>전투</color>가 발생했습니다!");


        var player = Chapter.playerObj.GetComponent<PlayerController>().Player;
               
        var monster = _battle.monster.GetComponent<MonsterController>().Monster;


        //for (int i = 0; i < 5; i++)
        //{
        //    yield return player.transform.DOLocalJump(player.transform.position, 1.5f, 2, 1f)
        //        .SetEase(Ease.InQuart)
        //        .WaitForCompletion();

        //    yield return Delay.WaitRandom(0.5f, 0.8f);

        //    StagePlayUI.AddDialog($"플레이어의 공격!\n<color=red>{Random.Range(100, 1000)}</color>의 피해를 입혔습니다!");

        //    yield return monster.transform.DOLocalJump(monster.transform.position, 1.5f, 2, 1f)
        //        .SetEase(Ease.InQuart)
        //        .WaitForCompletion();

        //    yield return Delay.WaitRandom(0.5f, 0.8f);

        //    StagePlayUI.AddDialog($"몬스터의 공격!\n<color=red>{Random.Range(100, 1000)}</color>의 피해를 입혔습니다!");
        //}


        if (player._status.Speed > monster._status.Speed)
        {
            yield return AttackSequence(player, monster);
        }
        else if (player._status.Speed < monster._status.Speed)
        {

            yield return AttackSequence(monster, player);
        }
        else
        {
            yield return AttackSequence(player, monster);
        }

        if (player._status.Hp > 0)
        {
            StagePlayUI.AddDialog("플레이어 승리! 축하해요!!");
        }

        else
        {
            StagePlayUI.AddDialog("플레이어 패배! 더 강해지기!!");
        }
        IsBattle = false;
        BattleStart?.Invoke();

        yield return Delay.Wait(1f);
           
        yield return _battle.UnloadScene();
    }

    private static IEnumerator AttackSequence(Player player, Monster monster)
    {
        Debug.Log($"플레이어 선공입니다. 플레이어 공격력은 : {player._status.Power} 몬스터 체력은 : {monster._status.Hp}");

        while (player._status.Hp > 0 && monster._status.Hp > 0)
        {            // 공격 후 즉시 승패 체크
            if (player._status.Hp <= 0)
            {
                break; // 상대방의 HP가 0 이하일 경우 공격을 중지
            }

            // 점프
            //yield return player.transform.DOLocalJump(_battle.playerPrefabs.transform.position, 1.5f, 2, 1f)
            //    .SetEase(Ease.InQuart)
            //    .WaitForCompletion();

            player.Attack(monster);
            Debug.Log($"플레이어가 공격했습니다! 플레이어의 공격력은 '{player._status.Power}', 크리는 '{player._status.Critical}'입니다.");
            Debug.Log($"몬스터가 공격당한뒤 체력은 : '{monster._status.Hp}', 방어력은 '{monster._status.Defense}' 입니다.");
            if (monster._status.Hp <= 0)
            {
                break; // 플레이어의 HP가 0 이하일 경우 공격을 중지
            }
            yield return new WaitForSeconds(1f);

            // 공격 후 즉시 승패 체크
           

            // 점프
            //yield return monster.transform.DOLocalJump(_battle.monsterPrefabs.transform.position, 1.5f, 2, 1f)
            //    .SetEase(Ease.InQuart)
            //    .WaitForCompletion();

            monster.Attack(player);

            Debug.Log($"몬스터가 공격했습니다! 몬스터의 공격력은 : '{monster._status.Power}', 몬스터 크리는 '{monster._status.Critical}'");
            Debug.Log($"플레이어가 공격당한뒤 체력은 : '{player._status.Hp}' 플레이어 방어력은, '{player._status.Defense}' 입니다. ");
            
            if (monster._status.Hp <= 0)
            {
                break; // 플레이어의 HP가 0 이하일 경우 공격을 중지
            }

            yield return new WaitForSeconds(1f);

        }
    }

    private static IEnumerator AttackSequence(Monster monster, Player player)
    {
        Debug.Log($"몬스터 선공입니다. 몬스터 공격력은 : {monster._status.Power}, 플레이어 체력은 : {player._status.Hp}");
        while (monster._status.Hp > 0 && player._status.Hp > 0)
        {            // 공격 후 즉시 승패 체크
            if (monster._status.Hp <= 0)
            {
                break; // 상대방의 HP가 0 이하일 경우 공격을 중지
            }

            // 점프
            //yield return player.transform.DOLocalJump(_battle.monsterPrefabs.transform.position, 1.5f, 2, 1f)
            //    .SetEase(Ease.InQuart)
            //    .WaitForCompletion();

            monster.Attack(player);

            Debug.Log($"몬스터가 공격했습니다! 몬스터의 공격력은 : '{monster._status.Power}', 몬스터 크리는 '{monster._status.Critical}'");
            Debug.Log($"플레이어가 공격당한뒤 체력은 : '{player._status.Hp}' 플레이어 방어력은, '{player._status.Defense}' 입니다. ");
            if (player._status.Hp <= 0)
            {
                break; // 플레이어의 HP가 0 이하일 경우 공격을 중지
            }
            yield return new WaitForSeconds(1f);

            // 공격 후 즉시 승패 체크
          

            //// 점프
            //yield return monster.transform.DOLocalJump(_battle.playerPrefabs.transform.position, 1.5f, 2, 1f)
            //    .SetEase(Ease.InQuart)
            //    .WaitForCompletion();

            player.Attack(monster);

            if (player._status.Hp <= 0)
            {
                break; // 플레이어의 HP가 0 이하일 경우 공격을 중지
            }
            Debug.Log($"플레이어가 공격했습니다! 플레이어의 공격력은 '{player._status.Power}', 크리는 '{player._status.Critical}'입니다.");
            Debug.Log($"몬스터가 공격당한뒤 체력은 : '{monster._status.Hp}', 방어력은 '{monster._status.Defense}' 입니다.");

            yield return new WaitForSeconds(1f);
        }
    }
}