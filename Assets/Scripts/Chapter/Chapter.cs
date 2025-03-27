using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public abstract class Chapter : MonoBehaviour
{
    [ShowNativeProperty] public int        TotalStageCount => stageList.Count;
    [ShowNativeProperty] public int        RouletteCount   => stageList.Count(stage => stage == StageType.STATUS_ROULETTE);
    [ShowNativeProperty] public int        EventCount      => stageList.Count(stage => stage == StageType.STAGE_EVENT);
    [ShowNativeProperty] public int        BattleCount     => stageList.Count(stage => stage == StageType.BATTLE);
    
    [SerializeField]                       private List<StageType> stageList;
    [MinMaxSlider(1f, 5f), SerializeField] private Vector2         growthRateRange;

    private List<IEnumerator> _stageActions = new();
    private GrowthRate    _growthRate;

    protected abstract IEnumerator OnRoulette();
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
        foreach (var stageAction in _stageActions)
            yield return stageAction;
    }
}
