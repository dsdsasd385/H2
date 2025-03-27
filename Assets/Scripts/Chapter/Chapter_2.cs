using System.Collections;
using UnityEngine;

public class Chapter_2 : Chapter
{
    protected override IEnumerator OnRoulette()
    {
        yield break;
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