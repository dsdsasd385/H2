using System.Collections;
using UnityEngine;

public class Chapter_1 : Chapter
{
    private WaitForSeconds GetRandomDelay() => new(Random.Range(0.5f, 1.5f));
    
    protected override IEnumerator OnRoulette()
    {
        yield return GetRandomDelay();
        
        StagePlayUI.AddDialog("룰렛 이벤트 발생!\n행운을 빌어요!");
    }

    protected override IEnumerator OnEvent()
    {
        yield return GetRandomDelay();
        
        StagePlayUI.AddDialog("스테이지 이벤트 발생!\n\n흥미롭네요!");
    }

    protected override IEnumerator OnBattle(float growthRate)
    {
        yield return GetRandomDelay();
        
        StagePlayUI.AddDialog($"전투가 시작됩니다!\n난이도 비율 : {growthRate}\n화이팅!");
        
        yield return GetRandomDelay();

        for (int i = 0; i < 5; i++)
        {
            StagePlayUI.AddDialog($"플레이어가 몬스터를 공격합니다!");
            
            yield return GetRandomDelay();
            
            StagePlayUI.AddDialog($"몬스터의 반격!");
            
            yield return GetRandomDelay();
        }
        
        StagePlayUI.AddDialog($"전투가 종료되었어요!\n플레이어 승리!!");
        
        yield return GetRandomDelay();
    }
}