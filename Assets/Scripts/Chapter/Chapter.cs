using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public abstract class Chapter : MonoBehaviour
{
    public static event Action<int, StageType> StageChangedEvent;
    public static event Action<RouletteResult> RouletteResultChangedEvent;

    private static IEnumerator OnRoulette()
    {
        RouletteResult result = default;

        yield return Roulette.OnRoulette(res =>  
        {
            result = res;
            Debug.Log($"룰렛 실행 : 값은 {res.ChangeValue}, 결과 타입은{res.Type}");
            RouletteResultChangedEvent?.Invoke(result);
        });


        var logs = result.Dialog.Split('\n');

        foreach (var log in logs)
        {
            StagePlayUI.AddDialog(log);
            yield return new WaitForSeconds(0.3f);
        }

        yield return StatusRouletteUI.ShowRouletteImage(result.Type);
    }
    
    /******************************************************************************************************************/
    /******************************************************************************************************************/
    
    [ShowNativeProperty] public int        TotalStageCount => stageList.Count;
    [ShowNativeProperty] public int        RouletteCount   => stageList.Count(stage => stage == StageType.STATUS_ROULETTE);
    [ShowNativeProperty] public int        EventCount      => stageList.Count(stage => stage == StageType.STAGE_EVENT);
    [ShowNativeProperty] public int        BattleCount     => stageList.Count(stage => stage == StageType.BATTLE);
    
    [SerializeField]                       private List<StageType> stageList;
    [SerializeField] private GameObject playerPrefab;  // 인스펙터에서 할당

    [MinMaxSlider(1f, 5f), SerializeField] private Vector2         growthRateRange;

    private Player player;
    private List<IEnumerator> _stageActions = new();
    private GrowthRate        _growthRate;

    protected abstract IEnumerator OnEvent();
    protected abstract IEnumerator OnBattle(float growthRate);

    public void Initialize()
    {
        // todo
        // Loading Player
        SetPlayer();
        // Loading Map

        StagePlayUI.Initialize();
        
        _growthRate = new(growthRateRange, BattleCount);

        SetStageAction();
    }

    private void SetPlayer()
    {
        if(player == null)
        {
            GameObject playerObj = Instantiate(playerPrefab);
            player = playerObj.GetComponent<Player>();
        }
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
            StagePlayUI.AddBlank();
        }
    }
}
