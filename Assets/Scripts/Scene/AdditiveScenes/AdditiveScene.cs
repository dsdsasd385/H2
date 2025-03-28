using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class AdditiveScene : MonoBehaviour
{
    protected static AdditiveSceneUI SceneUI { get; private set; }
    
    private static AdditiveScene CurrentAdditiveScene { get; set; }

    private static Scene         _currentScene;
    private static AudioListener _audioListener;
    
    public static IEnumerator LoadSceneAsync<T>(float width, float height, float yPos, Action<T> loadedScene) where T : AdditiveScene
    {
        var sceneName = typeof(T).Name;

        if (Camera.main.TryGetComponent(out AudioListener listener))
        {
            _audioListener = listener;
            _audioListener.enabled = false;
        }
        
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        _currentScene = SceneManager.GetSceneByName(sceneName);

        SceneManager.SetActiveScene(_currentScene);

        yield return CurrentAdditiveScene.Initialize(width, height, yPos);
        
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

    private IEnumerator Initialize(float width, float height, float yPos)
    {
        SceneUI = UI.Open<AdditiveSceneUI>();
        SceneUI.SetWindow(width, height, yPos);
        mainCam.targetTexture = SceneUI.DisplayTex;
        
        yield return SceneUI.CanvasGroup.DOFade(1f, 0.5f)
            .From(0f)
            .WaitForCompletion();
        
        yield return OnSceneLoaded();
    }

    public IEnumerator UnloadScene()
    {
        yield return OnUnloadScene();
        
        yield return SceneUI.CanvasGroup.DOFade(0f, 0.5f)
            .WaitForCompletion();
        
        yield return SceneManager.UnloadSceneAsync(_currentScene);

        if (_audioListener != null)
            _audioListener.enabled = true;
    }
}