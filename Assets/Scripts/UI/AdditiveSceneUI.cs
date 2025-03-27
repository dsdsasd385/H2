using UnityEngine;
using UnityEngine.UI;

public class AdditiveSceneUI : UI
{
    [SerializeField] private RawImage displayWindow;

    public RenderTexture DisplayTex => displayWindow.texture as RenderTexture;

    public void SetWindow(int width, int height)
    {
        displayWindow.rectTransform.sizeDelta = new Vector2(width, height);
        DisplayTex.width = width;
        DisplayTex.height = height;
    }
}