
using System;

public class PotionManager
{
  public Action<int> OnPotionChanged;

  private int _potion;
  public int Potion
  {
    get { return _potion; }
    set
    {
      _potion = value;
      OnPotionChanged?.Invoke(value);
    }
  }

  public void Init()
  {
    Potion = GetPotionCount();
  }

  public void ChargePotion(int potion)
  {
    Potion += potion;
  }

  public void UsePotion(int potion)
  {
    if (Potion - potion < 0) return;

    Potion -= potion;
  }

  public int GetPotionCount()
  {
    return 60;
  }
}