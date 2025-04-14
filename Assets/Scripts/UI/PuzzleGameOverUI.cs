using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PuzzleGameOverUI : UI
{
    [SerializeField] private TMP_Text txtMsg;
    
    public IEnumerator SetMessage(string message)
    {
        txtMsg.text = message;
        
        yield return CanvasGroup.DOFade(1f, 0.5f).From(0f).WaitForCompletion();

        yield return new WaitForSeconds(3f);
    }
}