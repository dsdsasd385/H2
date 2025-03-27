using UnityEngine;
using UnityEngine.UI;

public class AdditiveSceneUI : UI
{
    [SerializeField] private RectTransform windowRect;
    [SerializeField] private RawImage      displayWindow;

    public RenderTexture DisplayTex => displayWindow.texture as RenderTexture;

    public void SetWindow(int width, int height)
    {
        windowRect.sizeDelta = new Vector2(width, height);
        DisplayTex.width = width;
        DisplayTex.height = height;
    }
}