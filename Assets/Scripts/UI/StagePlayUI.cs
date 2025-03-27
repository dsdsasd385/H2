using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePlayUI : UI
{
    [SerializeField] private ScrollRect dialogView;
    [SerializeField] private Transform  dialogParent;
    [SerializeField] private DialogItem dialogPrefab;
    
    #region Singleton
    private static StagePlayUI _instance;
    private static StagePlayUI Instance
    {
        get
        {
            if (_instance == null)
                _instance = UI.Open<StagePlayUI>();
            return _instance;
        }
    }
    #endregion

    private List<DialogItem> _dialogList = new();

    public static void Initialize()
    {
        for (var i = 0; i < Instance._dialogList.Count; i++)
            Destroy(Instance._dialogList[i].gameObject);
        
        Instance._dialogList.Clear();
    }

    public static void AddDialog(string dialog)
    {
        var item = Instantiate(Instance.dialogPrefab, Instance.dialogParent);
        item.SetDialog(dialog);
        item.gameObject.SetActive(true);
        Instance._dialogList.Add(item);
        Instance.RefreshDialogView();
    }
    
    /******************************************************************************************************************/
    /******************************************************************************************************************/

    private void RefreshDialogView()
    {
        if (_refreshDialogViewCoroutine != null)
            StopCoroutine(_refreshDialogViewCoroutine);

        _refreshDialogViewCoroutine = RefreshDialogViewCoroutine();
        StartCoroutine(_refreshDialogViewCoroutine);
    }

    private IEnumerator _refreshDialogViewCoroutine;
    private IEnumerator RefreshDialogViewCoroutine()
    {
        yield return dialogView.RefreshScrollRect(0.15f, 0f);
    }
}