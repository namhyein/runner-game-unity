using UnityEngine;

public class UI_ContactButton : UI_Base
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        gameObject.GetOrAddComponent<UnityEngine.UI.Button>().BindButtonEvent(HandleOnClick);

        return true;
    }

    private void HandleOnClick()
    {
        string email = "dev@gamerecipe.io";
        string mailtoUrl = $"mailto:{email}";

        Application.OpenURL(mailtoUrl);
    }
}