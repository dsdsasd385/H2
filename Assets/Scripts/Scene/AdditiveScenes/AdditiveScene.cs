using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class AdditiveScene : MonoBehaviour
{
    private static AdditiveScene CurrentAdditiveScene { get; set; }

    private static Scene         _currentScene;
    private static AudioListener _audioListener;
    
    public static IEnumerator LoadSceneAsync<T>(float widthRatio, float heightRatio, Action<T> loadedScene) where T : AdditiveScene
    {
        var sceneName = typeof(T).Name;

        var width = Mathf.CeilToInt(Screen.width * widthRatio);
        var height = Mathf.CeilToInt(Screen.height * heightRatio);

        if (Camera.main.TryGetComponent(out AudioListener listener))
        {
            _audioListener = listener;
            _audioListener.enabled = false;
        }
        
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        _currentScene = SceneManager.GetSceneByName(sceneName);

        SceneManager.SetActiveScene(_currentScene);

        yield return CurrentAdditiveScene.Initialize(width, height);
        
        loadedScene?.Invoke(CurrentAdditiveScene as T);
    }
    
    /******************************************************************************************************************/
    /******************************************************************************************************************/
    [SerializeField] private Camera mainCam;

    protected abstract IEnumerator OnSceneLoaded();
    protected abstract IEnumerator OnUnloadScene();

    private void Awake()
    {
        CurrentAdditiveScene = this;
    }

    private IEnumerator Initialize(int width, int height)
    {
        var window = UI.Open<AdditiveSceneUI>();
        window.SetWindow(width, height);
        mainCam.targetTexture = window.DisplayTex;
        yield return OnSceneLoaded();
    }

    public IEnumerator UnloadScene()
    {
        yield return OnUnloadScene();
        
        yield return SceneManager.UnloadSceneAsync(_currentScene);

        if (_audioListener != null)
            _audioListener.enabled = true;
    }
}