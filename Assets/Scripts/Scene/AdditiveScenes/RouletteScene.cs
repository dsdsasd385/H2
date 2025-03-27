using System.Collections;
using UnityEngine;

public class RouletteScene : AdditiveScene
{
    protected override IEnumerator OnSceneLoaded()
    {
        yield break;
    }

    protected override IEnumerator OnUnloadScene()
    {
        yield break;
    }

    public IEnumerator StartRoulette()
    {
        float elapsed = 0f;
        
        while (elapsed <= 1f)
        {
            elapsed += Time.deltaTime;

            yield return null;
        }
        
        print("Roulette Finished!");
    }
}