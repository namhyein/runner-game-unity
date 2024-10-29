public class UI_LobbyScene : UI_Base
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.UI.SetCanvas(gameObject, false, 3);
        return true;
    }
}
