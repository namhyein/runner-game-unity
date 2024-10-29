using UnityEngine;


public class UI_PackageItem : UI_Base
{
  private enum Images
  {
    Image,
  }

  private enum Texts
  {
    Name,
    Price
  }

  private enum GameObjects
  {
    Rewards
  }

  protected override bool Init()
  {
    if (base.Init() == false)
      return false;

    BindObjects(typeof(GameObjects));
    BindImages(typeof(Images));
    BindTexts(typeof(Texts));

    return true;
  }

  public void InitializeUI(string packageId)
  {
    GetImage((int)Images.Image).sprite = Managers.Resource.Load<Sprite>(packageId);
    GetText((int)Texts.Name).text = Managers.Localization.GetLocalizedString(packageId);
    GetText((int)Texts.Price).text = Managers.IAP.GetProductLocalPriceString(packageId);

    SetReward(packageId);
  }

  private void SetReward(string packageId)
  {
    Transform rewards = GetObject((int)GameObjects.Rewards).transform;
    rewards.DestroyChildren();

    if (Managers.Data.Purchases[packageId].dia > 0)
    {
      UI_RewardChip rewardChip = Managers.UI.MakeSubItem<UI_RewardChip>(rewards);
      rewardChip.SetReward("Diamond", Managers.Data.Purchases[packageId].dia);
    }

    if (Managers.Data.Purchases[packageId].coin > 0)
    {
      UI_RewardChip rewardChip = Managers.UI.MakeSubItem<UI_RewardChip>(rewards);
      rewardChip.SetReward("Coin", Managers.Data.Purchases[packageId].coin);
    }

    if (Managers.Data.Purchases[packageId].heart > 0)
    {
      UI_RewardChip rewardChip = Managers.UI.MakeSubItem<UI_RewardChip>(rewards);
      rewardChip.SetReward("Heart", Managers.Data.Purchases[packageId].heart);
    }

    if (Managers.Data.Purchases[packageId].potion > 0)
    {
      UI_RewardChip rewardChip = Managers.UI.MakeSubItem<UI_RewardChip>(rewards);
      rewardChip.SetReward("Potion", Managers.Data.Purchases[packageId].potion);
    }
  }

  public void HandleOnClick()
  {
  }
}