public class OutGameScene : GameScene
{
    protected override void OnSceneStarted()
    {
        UI.Open<OutGameUI>();
    }
    
    protected override void OnReleaseScene()
    {
        
    }
}