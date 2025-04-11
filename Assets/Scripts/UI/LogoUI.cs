using System.Collections;
using DG.Tweening;
using UnityEngine;

public class LogoUI : UI
{
    public IEnumerator PlayFade(float duration = 3f)
    {
        yield return CanvasGroup.DOFade(1f, duration / 2f)
            .From(0f)
            .WaitForCompletion();

        yield return new WaitForSeconds(1f);
        
        yield return CanvasGroup.DOFade(0f, duration / 2f)
            .WaitForCompletion();
    }
}