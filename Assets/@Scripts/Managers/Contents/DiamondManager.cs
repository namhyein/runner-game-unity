
using System;

public class DiamondManager
{
  public Action<int> OnDiamondChanged;

  private int _diamond;
  public int Diamond
  {
    get { return _diamond; }
    set
    {
      _diamond = value;
      OnDiamondChanged?.Invoke(value);
    }
  }

  public void Init()
  {
    Diamond = GetDiamondCount();
  }

  public void ChargeDiamond(int diamond)
  {
    Diamond += diamond;
  }

  public void UseDiamond(int diamond)
  {
    if (Diamond - diamond < 0) return;

    Diamond -= diamond;
  }

  public int GetDiamondCount()
  {
    return 60;
  }
}