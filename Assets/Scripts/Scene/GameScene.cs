using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameScene : MonoBehaviour
{
    public  static GameScene CurrentScene   { get; private set; }
    private static bool      IsSceneLoading { get; set; }
    
    public static void LoadScene(string sceneName)
    {
        CurrentScene.StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private static IEnumerator LoadSceneCoroutine(string sceneName)
    {
        IsSceneLoading = true;
        
        var beforeScene = CurrentScene;
        
        DontDestroyOnLoad(beforeScene.gameObject);
        
        beforeScene.OnReleaseScene();

        var loadingUI = UI.Open<LoadingUI>();
        DontDestroyOnLoad(loadingUI.gameObject);

        yield return SceneManager.LoadSceneAsync(sceneName);

        var newGameScene = FindObjectsOfType<GameScene>()
            .FirstOrDefault(scene => scene.gameObject.scene.name != "DonDestroyOnLoad");

        if (newGameScene == null)
            throw new($"[{sceneName}] Scene has no SceneTrigger.");

        yield return new WaitUntil(() => newGameScene._isSceneReady);
        
        loadingUI.Close(true);
        Destroy(beforeScene.gameObject);
    }
    
    /******************************************************************************************************************/
    /******************************************************************************************************************/

    private bool _isSceneReady;

    protected virtual IEnumerator OnPrepareScene()
    {
        yield return null;
    }
    
    protected abstract void        OnSceneStarted();
    protected abstract void        OnReleaseScene();

    private void Awake()
    {
        _isSceneReady = false;
        CurrentScene = this;
        StartCoroutine(PrepareSceneCoroutine());
    }

    private IEnumerator PrepareSceneCoroutine()
    {
        yield return OnPrepareScene();

        _isSceneReady = true;
    }

    private void Start()
    {
        StartCoroutine(WaitForReadyCoroutine());
    }

    private IEnumerator WaitForReadyCoroutine()
    {
        yield return new WaitUntil(() => _isSceneReady);
        
        OnSceneStarted();
    }
}
