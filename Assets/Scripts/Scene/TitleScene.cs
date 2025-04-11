public class TitleScene : GameScene
{
    protected override void OnSceneStarted()
    {
        UI.Open<TitleUI>();
    }
    
    protected override void OnReleaseScene()
    {
        
    }
}