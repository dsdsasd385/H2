using System;
using System.Collections;

public struct RouletteResult
{
    public string Dialog;
    public Action ResultAction;
}

public static class Roulette
{
    public static IEnumerator OnRoulette(StageRouletteType rouletteType, Action<RouletteResult> result)
    {
        yield break;
    }
    
    private static RouletteResult GetResult(StageRouletteType type)
    {
        return default;
    }
}