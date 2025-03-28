using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public struct RouletteResult
{
    public string            Dialog;
    public int               ChangeValue;
    public StageRouletteType Type;
}

public static class Roulette
{
    private static List<StageRouletteType> _rouletteTypes;
    private static List<StageRouletteType> RouletteTypes
    {
        get
        {
            if (_rouletteTypes == null)
            {
                _rouletteTypes = new();

                foreach (StageRouletteType type in Enum.GetValues(typeof(StageRouletteType)))
                    _rouletteTypes.Add(type);
            }

            return _rouletteTypes;
        }
    }
    
    public static IEnumerator OnRoulette(Action<RouletteResult> result)
    {
        yield return Delay.WaitRandom(1f, 1.5f);
        
        var randomType = RouletteTypes.PickRandom();
        
        result?.Invoke(GetResult(randomType));
    }
    
    private static RouletteResult GetResult(StageRouletteType type)
    {
        string dialogFormat = null;
        string dialog = null;
        int    value = 0;
        
        switch (type)
        {
            case StageRouletteType.EXERCISE:
                dialogFormat = Dialog.Get(1001);
                value = Random.Range(5, 16);
                dialog = string.Format(dialogFormat, value);
                break;
            case StageRouletteType.RESHARPENING_WEAPON:
                dialogFormat = Dialog.Get(1002);
                value = Random.Range(5, 16);
                dialog = string.Format(dialogFormat, value);
                break;
            case StageRouletteType.CLEANING_ARMOR:
                dialogFormat = Dialog.Get(1003);
                value = Random.Range(5, 16);
                dialog = string.Format(dialogFormat, value);
                break;
            case StageRouletteType.PICK_COIN:
                dialogFormat = Dialog.Get(1004);
                value = Random.Range(500, 1501);
                dialog = string.Format(dialogFormat, value);
                break;
            case StageRouletteType.BUG_BITE:
                dialogFormat = Dialog.Get(1005);
                value = Random.Range(-15, -6);
                dialog = string.Format(dialogFormat, value);
                break;
            case StageRouletteType.BROKEN_WEAPON:
                dialogFormat = Dialog.Get(1006);
                value = Random.Range(-15, -6);
                dialog = string.Format(dialogFormat, value);
                break;
            case StageRouletteType.LOOSEN_ARMOR:
                dialogFormat = Dialog.Get(1007);
                value = Random.Range(-15, -6);
                dialog = string.Format(dialogFormat, value);
                break;
            case StageRouletteType.LOST_COIN:
                dialogFormat = Dialog.Get(1008);
                value = Random.Range(-1500, -501);
                dialog = string.Format(dialogFormat, value);
                break;
        }

        return new RouletteResult
        {
            Dialog = dialog,
            ChangeValue = value,
            Type = type
        };
    }
}