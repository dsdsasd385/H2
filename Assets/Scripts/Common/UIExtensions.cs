using System.Collections;
using DG.Tweening;
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

    public static IEnumerator FadeIn(this Image img, float duration = 0.5f, bool needInit = true)
    {
        if (needInit)
            img.DOKill();

        yield return img.DOFade(1f, duration).WaitForCompletion();
    }
    
    public static IEnumerator FadeOut(this Image img, float duration = 0.5f, bool needInit = true)
    {
        if (needInit)
            img.DOKill();

        yield return img.DOFade(0f, duration).WaitForCompletion();
    }
}