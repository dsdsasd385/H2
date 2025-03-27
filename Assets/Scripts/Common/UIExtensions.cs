using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class UIExtensions
{
    public static IEnumerator RefreshScrollRect(this ScrollRect scroll, float duration, float endPosition)
    {
        yield return null;

        var content = scroll.content;
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        
        Canvas.ForceUpdateCanvases();

        float elapsed  = 0f;
        float start    = scroll.verticalNormalizedPosition;
        float end      = endPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            scroll.verticalNormalizedPosition = Mathf.Lerp(start, end, t);
            yield return null;
        }

        scroll.verticalNormalizedPosition = endPosition;
    }
}