using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OutGameUI : UI
{
    [SerializeField] private Button        btnDungeon;
    [SerializeField] private RectTransform chapterInfo;

    private Tween _floatTween;

    private void Awake()
    {
        btnDungeon.onClick.AddListener(OnStartGame);
    }

    private void OnStartGame()
    {
        Dim.FadeIn(2f, 0f, () => GameScene.LoadScene("InGameScene"));
    }

    private void OnEnable()
    {
        Dim.FadeOut(1f, 1f);

        _floatTween = chapterInfo.DOAnchorPosY(chapterInfo.anchoredPosition.y + 25, 1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        _floatTween.Kill();
    }
}