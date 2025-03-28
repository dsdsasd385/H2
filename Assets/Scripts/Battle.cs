using System.Collections;
using DG.Tweening;
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

        for (int i = 0; i < 5; i++)
        {
            yield return player.transform.DOLocalJump(player.transform.position, 1.5f, 2, 1f)
                .SetEase(Ease.InQuart)
                .WaitForCompletion();

            yield return Delay.WaitRandom(0.5f, 0.8f);
            
            StagePlayUI.AddDialog($"플레이어의 공격!\n<color=red>{Random.Range(100, 1000)}</color>의 피해를 입혔습니다!");
            
            yield return monster.transform.DOLocalJump(monster.transform.position, 1.5f, 2, 1f)
                .SetEase(Ease.InQuart)
                .WaitForCompletion();
            
            yield return Delay.WaitRandom(0.5f, 0.8f);
            
            StagePlayUI.AddDialog($"몬스터의 공격!\n<color=red>{Random.Range(100, 1000)}</color>의 피해를 입혔습니다!");
        }
        
        StagePlayUI.AddDialog("플레이어 승리! 축하해요!!");
        
        yield return Delay.Wait(1f);

        yield return _battle.UnloadScene();
    }
}