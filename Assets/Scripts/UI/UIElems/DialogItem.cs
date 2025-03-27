using TMPro;
using UnityEngine;

public class DialogItem : MonoBehaviour
{
    [SerializeField] private TMP_Text txtDialog;

    public void SetDialog(string dialog)
    {
        txtDialog.text = dialog;
    }
}