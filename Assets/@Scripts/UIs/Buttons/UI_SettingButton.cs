using UnityEngine.UI;

public class UI_SettingButton : UI_Base
{
  protected override bool Init()
  {
    if (base.Init() == false)
      return false;

    gameObject.GetOrAddComponent<Button>().BindButtonEvent(HandleOnClick);

    return true;
  }

  public void HandleOnClick()
  {
    Managers.UI.ShowPanelUI<UI_SettingPanel>();
  }
}