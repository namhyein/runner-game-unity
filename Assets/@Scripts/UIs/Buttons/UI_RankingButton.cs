using UnityEngine.UI;

public class RankingButton : UI_Base
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
    Managers.UI.CloseAllPanelUI();
    Managers.UI.ShowPanelUI<UI_RankingPanel>();
  }
}