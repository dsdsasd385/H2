using System.Collections;

public class Chapter_2 : Chapter
{
    protected override IEnumerator OnRoulette()
    {
        yield break;
    }

    protected override IEnumerator OnEvent()
    {
        yield break;
    }

    protected override IEnumerator OnBattle(float growthRate)
    {
        print(growthRate);
        
        yield break;
    }
}