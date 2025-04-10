using DG.Tweening;
using System;
using System.Collections;
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


        var playerController = Chapter.playerObj.GetComponent<PlayerController>();
        var player = playerController.Player;
               
        var monsterController = _battle.monster.GetComponent<MonsterController>();
        var monster = monsterController.Monster;

        if (player.Status.Speed > monster.Status.Speed)
        {
            yield return AttackSequence(playerController, monsterController);
        }
        else if (player.Status.Speed < monster.Status.Speed)
        {

            yield return AttackSequence(monsterController, playerController);
        }
        else
        {
            yield return AttackSequence(playerController, monsterController);
        }

        if (player.Status.Hp > 0)
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

    private static IEnumerator AttackSequence(PlayerController playerController, MonsterController monsterController)
    {
        var player = playerController.Player;
        var monster = monsterController.Monster;

        Debug.Log($"플레이어 선공입니다. 플레이어 공격력은 : {player.Status.Power} 몬스터 체력은 : {monster.Status.Hp}");

        while (player.Status.Hp > 0 && monster.Status.Hp > 0)
        {            // 공격 후 즉시 승패 체크
            if (player.Status.Hp <= 0)
            {
                break; // 상대방의 HP가 0 이하일 경우 공격을 중지
            }

            // 점프
            //yield return player.transform.DOLocalJump(_battle.playerPrefabs.transform.position, 1.5f, 2, 1f)
            //    .SetEase(Ease.InQuart)
            //    .WaitForCompletion();
            if(playerController == null)
            {
                Debug.Log("PlayerController = null");
            }   

            yield return CoroutineRunner.Instance.RunCoroutine(playerController.PlayerAttackSequence(playerController, monsterController));

            //Debug.Log($"플레이어가 공격했습니다! 플레이어의 공격력은 '{player.Status.Power}', 크리는 '{player.Status.Critical}'입니다.");
            //Debug.Log($"몬스터가 공격당한뒤 체력은 : '{monster.Status.Hp}', 방어력은 '{monster.Status.Defense}' 입니다.");
            if (monster.Status.Hp <= 0)
            {
                break; // 플레이어의 HP가 0 이하일 경우 공격을 중지
            }
            Debug.Log("체력체크함");

            yield return new WaitForSeconds(2f);

            // 공격 후 즉시 승패 체크

            
            // 점프
            //yield return monster.transform.DOLocalJump(_battle.monsterPrefabs.transform.position, 1.5f, 2, 1f)
            //    .SetEase(Ease.InQuart)
            //    .WaitForCompletion();

            CoroutineRunner.Instance.RunCoroutine(monsterController.MonsterAttackSequence(playerController, monsterController));

            Debug.Log($"몬스터가 공격했습니다! 몬스터의 공격력은 : '{monster.Status.Power}', 몬스터 크리는 '{monster.Status.Critical}'");
            Debug.Log($"플레이어가 공격당한뒤 체력은 : '{player.Status.Hp}' 플레이어 방어력은, '{player.Status.Defense}' 입니다. ");
            
            if (monster.Status.Hp <= 0)
            {
                break; // 플레이어의 HP가 0 이하일 경우 공격을 중지
            }

            yield return new WaitForSeconds(2f);

        }
    }

    private static IEnumerator AttackSequence(MonsterController monsterController, PlayerController playerController)
    {
        var player = playerController.Player;
        var monster = monsterController.Monster;

        Debug.Log($"몬스터 선공입니다. 몬스터 공격력은 : {monster.Status.Power}, 플레이어 체력은 : {player.Status.Hp}");
        while (monster.Status.Hp > 0 && player.Status.Hp > 0)
        {            // 공격 후 즉시 승패 체크
            if (monster.Status.Hp <= 0)
            {
                break; // 상대방의 HP가 0 이하일 경우 공격을 중지
            }

            // 점프
            //yield return player.transform.DOLocalJump(_battle.monsterPrefabs.transform.position, 1.5f, 2, 1f)
            //    .SetEase(Ease.InQuart)
            //    .WaitForCompletion();

            monsterController.MonsterAttackSequence(playerController, monsterController);

            Debug.Log($"몬스터가 공격했습니다! 몬스터의 공격력은 : '{monster.Status.Power}', 몬스터 크리는 '{monster.Status.Critical}'");
            Debug.Log($"플레이어가 공격당한뒤 체력은 : '{player.Status.Hp}' 플레이어 방어력은, '{player.Status.Defense}' 입니다. ");
            if (player.Status.Hp <= 0)
            {
                break; // 플레이어의 HP가 0 이하일 경우 공격을 중지
            }
            yield return new WaitForSeconds(2f);

            // 공격 후 즉시 승패 체크


            //// 점프
            //yield return monster.transform.DOLocalJump(_battle.playerPrefabs.transform.position, 1.5f, 2, 1f)
            //    .SetEase(Ease.InQuart)
            //    .WaitForCompletion();

            playerController.PlayerAttackSequence(playerController, monsterController);

            if (player.Status.Hp <= 0)
            {
                break; // 플레이어의 HP가 0 이하일 경우 공격을 중지
            }
            Debug.Log($"플레이어가 공격했습니다! 플레이어의 공격력은 '{player.Status.Power}', 크리는 '{player.Status.Critical}'입니다.");
            Debug.Log($"몬스터가 공격당한뒤 체력은 : '{monster.Status.Hp}', 방어력은 '{monster.Status.Defense}' 입니다.");

            yield return new WaitForSeconds(2f);
        }
    }
}