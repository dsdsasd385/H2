using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StatusRouletteUI : UI
{
    public static IEnumerator ShowRouletteImage(StageRouletteType type, float duration = 2f)
    {
        var ui = UI.Open<StatusRouletteUI>();

        var sprite = ResourceManager.GetSprite(type.ToString());

        ui.imgRoulette.sprite = sprite;

        yield return ui.imgRoulette.FadeIn();

        yield return new WaitForSeconds(duration);

        yield return ui.imgRoulette.FadeOut();
        
        ui.Close();
    }
    
    /******************************************************************************************************************/
    /******************************************************************************************************************/

    [SerializeField] private Image imgRoulette;
}