using System.Collections;
using UnityEngine;

public class Chapter_1 : Chapter
{
    protected override IEnumerator OnRoulette()
    {
        print($"PLAY ROULETTE!");

        yield return new WaitForSeconds(0.5f);
    }

    protected override IEnumerator OnEvent()
    {
        print($"STAGE EVENT!");

        yield return new WaitForSeconds(0.5f);
    }

    protected override IEnumerator OnBattle(float growthRate)
    {
        print($"MONSTER STATUS GROWTH : {growthRate}");
        
        yield return new WaitForSeconds(0.5f);
    }
}