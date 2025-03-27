using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupButtonUI : UI
{
    public static IEnumerator WaitForClick(string buttonText, float duration = 3f)
    {
        var ui = UI.Open<PopupButtonUI>();
        
        bool isClicked = false;
        float elapsed  = 0f;

        ui.txtContent.text = buttonText;
        
        ui.button.onClick.RemoveAllListeners();

        ui.button.onClick.AddListener(() => isClicked = true);

        if (duration > 0)
        {
            while (elapsed < duration && isClicked == false)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
        else
            yield return new WaitUntil(() => isClicked);
        
        ui.Close();
    }
    
    /******************************************************************************************************************/
    /******************************************************************************************************************/

    [SerializeField] private Button   button;
    [SerializeField] private TMP_Text txtContent;
}