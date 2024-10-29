using UnityEngine;
using UnityEngine.UI;

public class UI_UserInfoChip : UI_Base
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        gameObject.GetOrAddComponent<Button>().BindButtonEvent(HandleOnClick);

        return true;
    }

    private void HandleOnClick()
    {
      // UI_StorePanel panel = Managers.UI.ShowPanelUI<UI_NicknamePanel>();
    }
}
