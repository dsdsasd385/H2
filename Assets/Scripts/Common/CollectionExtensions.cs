using System.Collections.Generic;
using UnityEngine;

public static class CollectionExtensions
{
    public static T PickRandom<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
            return default;
        
        int randomIdx = Random.Range(0, list.Count);
        var elem = list[randomIdx];
        return elem;
    }

    public static T PullRandom<T>(this List<T> list)
    {
        var elem = PickRandom(list);
        list.Remove(elem);
        return elem;
    }
}