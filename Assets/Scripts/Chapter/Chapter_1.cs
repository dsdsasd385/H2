using System.Collections;

public class Chapter_1 : Chapter
{
    protected override IEnumerator OnBattle(float growthRate)
    {
        yield return Battle.BattleCoroutine(growthRate);
    }
}