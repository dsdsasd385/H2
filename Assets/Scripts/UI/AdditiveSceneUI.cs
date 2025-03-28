using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AdditiveSceneUI : UI
{
    [SerializeField] private RectTransform windowRect;
    [SerializeField] private RawImage      displayWindow;

    public RenderTexture DisplayTex => displayWindow.texture as RenderTexture;

    public void SetWindow(float width, float height, float yPos)
    {
        windowRect.sizeDelta = new Vector2(width, height);
        windowRect.anchoredPosition = Vector2.up * yPos;
        DisplayTex.width = (int)width;
        DisplayTex.height = (int)height;
    }
}