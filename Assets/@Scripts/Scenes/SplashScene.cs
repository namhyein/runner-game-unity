using static Define;

public class SplashScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = EScene.SplashScene;
        return true;
    }

    public override void Clear()
    {

    }
}
