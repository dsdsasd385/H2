using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StatusRouletteUI : UI
{
    public static IEnumerator ShowRouletteImage(StageRouletteType type, float duration = 2f)
    {
        var ui = UI.Open<StatusRouletteUI>();
        
        // todo get sprite by type

        yield return ui.imgRoulette.FadeIn();

        yield return new WaitForSeconds(duration);

        yield return ui.imgRoulette.FadeOut();
        
        ui.Close();
    }
    
    /******************************************************************************************************************/
    /******************************************************************************************************************/

    [SerializeField] private Image imgRoulette;
}