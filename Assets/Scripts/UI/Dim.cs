using System;
using DG.Tweening;
using UnityEngine;

public class Dim : UI
{
    private static Dim _instance;
    private static Dim Instance
    {
        get
        {
            if (_instance == null || _instance.gameObject.activeSelf == false)
                _instance = UI.Open<Dim>();
            return _instance;
        }
    }

    public static void FadeIn(float duration, float from, Action onFinished = null)
    {
        Instance.CanvasGroup.DOFade(1f, duration)
            .From(from)
            .OnComplete(() =>
            {
                onFinished?.Invoke();
            });
    }
    
    public static void FadeOut(float duration, float from, Action onFinished = null)
    {
        Instance.CanvasGroup.DOFade(0f, duration)
            .From(from)
            .OnComplete(() =>
            {
                Instance.Close(true);
                onFinished?.Invoke();
            });
    }
}