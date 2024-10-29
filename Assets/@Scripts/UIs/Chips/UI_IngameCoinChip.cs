using TMPro;

public class UI_IngameCoinChip : UI_Base
{
  enum Texts
  {
    Text
  }

  enum Images
  {
    Outline,
    Icon
  }

  protected override bool Init()
  {
    if (base.Init() == false)
      return false;

    Bind<TextMeshProUGUI>(typeof(Texts));
    SetCatnipText(Managers.Game.Catnip);
    return true;
  }

  void Start()
  {
    Managers.Game.OnCatnipChanged += SetCatnipText;
  }

  void OnDestroy()
  {
    Managers.Game.OnCatnipChanged -= SetCatnipText;
  }

  private void SetCatnipText(int value)
  {
    Get<TextMeshProUGUI>((int)Texts.Text).text = value.ToString("N0");
  }
}