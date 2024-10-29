using System;
using System.Collections.Generic;

namespace IAP
{
  [Serializable]
  public class Purchase
  {
    public string id;
    public string days;
    public int type;
    public int removead;
    public int dia;
    public int coin;
    public int potion;
    public int heart;
  }

  [Serializable]
  public class PurchaseLoader : ILoader<string, Purchase>
  {
    public List<Purchase> items = new();

    public Dictionary<string, Purchase> MakeDict()
    {
      Dictionary<string, Purchase> dict = new();
      foreach (Purchase item in items)
        dict.Add(item.id, item);

      return dict;
    }
  }
}