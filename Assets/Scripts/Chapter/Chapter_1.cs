using System.Collections;

public class Chapter_1 : Chapter
{
    protected override IEnumerator OnEvent()
    {
        yield return Delay.WaitRandom(0.2f, 1f);
        
        StagePlayUI.AddDialog("스테이지 이벤트 발생!\n\n흥미롭네요!");
    }

    protected override IEnumerator OnBattle(float growthRate)
    {
        yield return Battle.BattleCoroutine(growthRate);
    }
}