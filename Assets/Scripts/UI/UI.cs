using System.Collections.Generic;
using UnityEngine;

public abstract class UI : MonoBehaviour
{
    private static readonly Dictionary<string, UI> _spawnedUIList = new();

    private static string GetName<T>() where T : UI => typeof(T).Name;

    public static T Open<T>() where T : UI
    {
        var spawnedUI = Get<T>();
        if (spawnedUI != null && spawnedUI.gameObject != null)
        {
            spawnedUI.gameObject.SetActive(true);
            return spawnedUI;
        }
        
        var uiName = GetName<T>();
        T prefab = Resources.Load<T>($"UI/{uiName}");
        T ui = Instantiate(prefab);
        _spawnedUIList[uiName] = ui;
        ui.gameObject.SetActive(true);
        return ui;
    }

    public static T Get<T>() where T : UI
    {
        var uiName = GetName<T>();

        if (_spawnedUIList.TryGetValue(uiName, out UI ui))
            return ui as T;

        return null;
    }
    
    /******************************************************************************************************************/
    /******************************************************************************************************************/

    private CanvasGroup _cg;
    public CanvasGroup CanvasGroup
    {
        get
        {
            if (_cg == null)
                _cg = gameObject.AddComponent<CanvasGroup>();
            return _cg;
        }
    }
    
    
    public void Close(bool kill = false)
    {
        if (kill)
        {
            _spawnedUIList.Remove(GetType().Name);
            Destroy(gameObject);
            return;
        }
        
        gameObject.SetActive(false);
    }
}