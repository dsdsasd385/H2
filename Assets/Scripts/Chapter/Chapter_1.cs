using System.Collections;
using UnityEngine;

public class Chapter_1 : Chapter
{
    protected override IEnumerator OnRoulette()
    {
        RouletteScene rouletteScene = null;

        yield return AdditiveScene.LoadSceneAsync<RouletteScene>(0.8f, 0.8f, scene => rouletteScene = scene);

        yield return rouletteScene.StartRoulette();

        yield return rouletteScene.UnloadScene();
    }

    protected override IEnumerator OnEvent()
    {
        yield return new WaitForSeconds(1f);
        
        print($"STAGE EVENT!");
    }

    protected override IEnumerator OnBattle(float growthRate)
    {
        yield return new WaitForSeconds(1f);
        
        print($"MONSTER STATUS GROWTH : {growthRate}");
    }
}