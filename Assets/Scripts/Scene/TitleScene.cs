public class TitleScene : GameScene
{
    protected override void OnSceneStarted()
    {
        GameScene.LoadScene("OutGameScene");
    }
    
    protected override void OnReleaseScene()
    {
        
    }
}