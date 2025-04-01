using System.Collections;
using System.Threading;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public static class Battle
{
    private static BattleScene _battle;

    public static IEnumerator BattleCoroutine(float growthRate)
    {
        yield return AdditiveScene.LoadSceneAsync<BattleScene>(990, 930, 200, loadedScene => _battle = loadedScene);

        StagePlayUI.AddDialog("<color=red>전투</color>가 발생했습니다!");

        var player = _battle.player;
        var monster = _battle.monster;

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


        if (player.status.Speed > monster.status.Speed)
        {
            yield return AttackSequence(player, monster);
        }
        else if (player.status.Speed < monster.status.Speed)
        {

            yield return AttackSequence(monster, player);
        }
        else
        {
            yield return AttackSequence(player, monster);
        }

        if (player.status.Hp > 0)
        {
            StagePlayUI.AddDialog("플레이어 승리! 축하해요!!");
        }

        else
        {
            StagePlayUI.AddDialog("플레이어 패배! 더 강해지기!!");
        }

        yield return Delay.Wait(1f);

        yield return _battle.UnloadScene();
    }

    private static IEnumerator AttackSequence(Player player, Monster monster)
    {
        Debug.Log($"플레이어 선공입니다. 플레이어 공격력은 : {player.status.Power} 몬스터 체력은 : {monster.status.Hp}");

        while (player.status.Hp > 0 && monster.status.Hp > 0)
        {            // 공격 후 즉시 승패 체크
            if (player.status.Hp <= 0)
            {
                break; // 상대방의 HP가 0 이하일 경우 공격을 중지
            }

            player.Attack(monster);
            Debug.Log($"플레이어가 공격했습니다! 플레이어의 공격력은 '{player.status.Power}', 크리는 '{player.status.Critical}'입니다.");
            Debug.Log($"몬스터가 공격당한뒤 체력은 : '{monster.status.Hp}', 방어력은 '{monster.status.Defense}' 입니다.");

            yield return new WaitForSeconds(1f);

            // 공격 후 즉시 승패 체크
            if (monster.status.Hp <= 0)
            {
                break; // 플레이어의 HP가 0 이하일 경우 공격을 중지
            }

            monster.Attack(player);

            Debug.Log($"몬스터가 공격했습니다! 몬스터의 공격력은 : '{monster.status.Power}', 몬스터 크리는 '{monster.status.Critical}'");
            Debug.Log($"플레이어가 공격당한뒤 체력은 : '{player.status.Hp}' 플레이어 방어력은, '{player.status.Defense}' 입니다. ");


            yield return new WaitForSeconds(1f);

        }
    }

    private static IEnumerator AttackSequence(Monster monster, Player player)
    {
        Debug.Log($"몬스터 선공입니다. 몬스터 공격력은 : {monster.status.Power}, 플레이어 체력은 : {player.status.Hp}");
        while (monster.status.Hp > 0 && player.status.Hp > 0)
        {            // 공격 후 즉시 승패 체크
            if (monster.status.Hp <= 0)
            {
                break; // 상대방의 HP가 0 이하일 경우 공격을 중지
            }

            monster.Attack(player);
            Debug.Log($"몬스터가 공격했습니다! 몬스터의 공격력은 : '{monster.status.Power}', 몬스터 크리는 '{monster.status.Critical}'");
            Debug.Log($"플레이어가 공격당한뒤 체력은 : '{player.status.Hp}' 플레이어 방어력은, '{player.status.Defense}' 입니다. ");

            yield return new WaitForSeconds(1f);

            // 공격 후 즉시 승패 체크
            if (player.status.Hp <= 0)
            {
                break; // 플레이어의 HP가 0 이하일 경우 공격을 중지
            }

            player.Attack(monster);

            Debug.Log($"플레이어가 공격했습니다! 플레이어의 공격력은 '{player.status.Power}', 크리는 '{player.status.Critical}'입니다.");
            Debug.Log($"몬스터가 공격당한뒤 체력은 : '{monster.status.Hp}', 방어력은 '{monster.status.Defense}' 입니다.");

            yield return new WaitForSeconds(1f);

        }
    }
}