using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public abstract class Chapter : MonoBehaviour
{
    public static event Action<int, StageType> StageChangedEvent;

    private static IEnumerator OnRoulette()
    {
        RouletteResult result = default;

        yield return Roulette.OnRoulette(res => result = res);
        
        StagePlayUI.AddDialog(result.Dialog);
        
        // todo UI Roulette -> Show Status Change Event

        yield return Delay.GetRandom(1f, 2f);
    }
    
    /******************************************************************************************************************/
    /******************************************************************************************************************/
    
    [ShowNativeProperty] public int        TotalStageCount => stageList.Count;
    [ShowNativeProperty] public int        RouletteCount   => stageList.Count(stage => stage == StageType.STATUS_ROULETTE);
    [ShowNativeProperty] public int        EventCount      => stageList.Count(stage => stage == StageType.STAGE_EVENT);
    [ShowNativeProperty] public int        BattleCount     => stageList.Count(stage => stage == StageType.BATTLE);
    
    [SerializeField]                       private List<StageType> stageList;
    [MinMaxSlider(1f, 5f), SerializeField] private Vector2         growthRateRange;

    private List<IEnumerator> _stageActions = new();
    private GrowthRate        _growthRate;

    protected abstract IEnumerator OnEvent();
    protected abstract IEnumerator OnBattle(float growthRate);

    public void Initialize()
    {
        // todo
        // Loading Player
        // Loading Map
        // Stage Start
        
        StagePlayUI.Initialize();
        
        _growthRate = new(growthRateRange, BattleCount);

        SetStageAction();
    }

    private void SetStageAction()
    {
        int battleStageOrder = 1;
        
        foreach (var stageType in stageList)
        {
            IEnumerator action = null;
            
            switch (stageType)
            {
                case StageType.STATUS_ROULETTE:
                    action = OnRoulette();
                    break;
                case StageType.STAGE_EVENT:
                    action = OnEvent();
                    break;
                case StageType.BATTLE:
                    action = OnBattle(_growthRate.GetRate(battleStageOrder));
                    battleStageOrder++;
                    break;
            }

            _stageActions.Add(action);
        }
    }

    public void PlayChapter()
    {
        StartCoroutine(PlayChapterCoroutine());
    }

    private IEnumerator PlayChapterCoroutine()
    {
        for (var index = 0; index < _stageActions.Count; index++)
        {
            StageChangedEvent?.Invoke(index + 1, stageList[index]);
            
            var stageAction = _stageActions[index];
            yield return PopupButtonUI.WaitForClick("스테이지 진행", -1);
            yield return stageAction;
        }
    }
}
