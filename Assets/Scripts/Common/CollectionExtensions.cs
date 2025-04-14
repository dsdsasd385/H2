using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    public static IEnumerator ChangeValueAnim(this TMP_Text txt, int before, int value, float duration = 1f)
    {
        float time = 0f;
        int current = before;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            int display = Mathf.RoundToInt(Mathf.Lerp(before, value, t));

            if (display != current)
            {
                current = display;
                txt.text = current.ToString();
            }

            yield return null;
        }

        txt.text = value.ToString();
    }
}