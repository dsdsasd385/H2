using System.Collections;
using UnityEngine;

public class Chapter_1 : Chapter
{
    protected override IEnumerator OnRoulette()
    {
        RouletteScene rouletteScene = null;
        
        StagePlayUI.AddDialog("Roulette Started!\nWish Good Luck!");

        yield return AdditiveScene.LoadSceneAsync<RouletteScene>(0.8f, 0.8f, scene => rouletteScene = scene);

        yield return rouletteScene.StartRoulette();

        yield return rouletteScene.UnloadScene();
    }

    protected override IEnumerator OnEvent()
    {
        yield return new WaitForSeconds(1f);
        
        StagePlayUI.AddDialog("Stage Event!\n\nInteresting!");
    }

    protected override IEnumerator OnBattle(float growthRate)
    {
        yield return new WaitForSeconds(1f);
        
        StagePlayUI.AddDialog($"Battle with Monster({growthRate})\nCould be hard Battle!\nFIGHT!");
        
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 5; i++)
        {
            StagePlayUI.AddDialog($"PLAYER ATTACK MONSTER!");
            
            yield return new WaitForSeconds(0.5f);
        }
    }
}