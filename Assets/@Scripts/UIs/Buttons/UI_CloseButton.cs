using UnityEngine;
using UnityEngine.UI;

public class UI_CloseButton : UI_Base
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
    Managers.UI.ClosePanelUI();
  }
}
