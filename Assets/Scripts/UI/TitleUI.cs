using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleUI : UI, IPointerDownHandler
{
    [SerializeField] private Button dim;
    [SerializeField] private CanvasGroup txtClickCg;

    private Tween _fadeTween;

    private void OnEnable()
    {
        _fadeTween = txtClickCg.DOFade(1f, 1f)
            .From(0f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        _fadeTween.Kill();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameScene.LoadScene("OutGameScene");
    }
}