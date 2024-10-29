using UnityEngine;


public class UI_RewardChip : UI_Base
{
  private enum Images
  {
    Image
  }

  private enum Texts
  {
    Count
  }

  protected override bool Init()
  {
    if (base.Init() == false)
      return false;

    BindImages(typeof(Images));
    BindTexts(typeof(Texts));

    return true;
  }

  public void SetReward(string image, int count)
  {
    GetImage((int)Images.Image).sprite = Managers.Resource.Load<Sprite>(image);
    GetText((int)Texts.Count).text = count.ToString();
  }
}