
using System;

public class CoinManager
{
  public Action<int> OnCoinChanged;

  private int _coin;
  public int Coin
  {
    get { return _coin; }
    set
    {
      _coin = value;
      OnCoinChanged?.Invoke(value);
    }
  }

  public void Init()
  {
    Coin = GetCoinCount();
  }

  public void ChargeCoin(int coin)
  {
    Coin += coin;
  }

  public void UseCoin(int coin)
  {
    if (Coin - coin < 0) return;

    Coin -= coin;
  }

  public int GetCoinCount()
  {
    return 60;
  }
}