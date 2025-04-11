using System.Collections;

public class EntryScene : GameScene
{
    protected override void OnSceneStarted()
    {
        StartCoroutine(EntryCoroutine());
    }
    
    protected override void OnReleaseScene()
    {
        
    }

    private IEnumerator EntryCoroutine()
    {
        yield return UI.Open<LogoUI>().PlayFade();
        
        GameScene.LoadScene("TitleScene");
    }
}