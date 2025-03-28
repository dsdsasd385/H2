using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePlayUI : UI
{
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

    private readonly List<GameObject> _dialogList = new();

    public static void Initialize()
    {
        for (var i = 0; i < Instance._dialogList.Count; i++)
            Destroy(Instance._dialogList[i]);
        
        Instance._dialogList.Clear();
        
        Chapter.StageChangedEvent -= OnStageChanged;
        Chapter.StageChangedEvent += OnStageChanged;
    }

    public static void AddDialog(string dialog)
    {
        var item = Instantiate(Instance.dialogPrefab, Instance.dialogParent);
        item.SetDialog(dialog);
        item.gameObject.SetActive(true);
        Instance._dialogList.Add(item.gameObject);
        Instance.RefreshDialogView();
    }

    public static void AddBlank()
    {
        var blank = Instantiate(Instance.dialogBlankPrefab, Instance.dialogParent);
        blank.gameObject.SetActive(true);
        Instance._dialogList.Add(blank);
        Instance.RefreshDialogView();
    }

    private static void OnStageChanged(int stage, StageType stageType)
    {
        print($"현재 스테이지 : {stage} - {stageType}");
    }
    
    /******************************************************************************************************************/
    /******************************************************************************************************************/
    
    [SerializeField] private ScrollRect dialogView;
    [SerializeField] private Transform  dialogParent;
    [SerializeField] private DialogItem dialogPrefab;
    [SerializeField] private GameObject dialogBlankPrefab;

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