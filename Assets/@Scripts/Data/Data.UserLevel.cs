using System;
using System.Collections.Generic;


namespace UserLevel
{
  [Serializable]
  public class UserLevel
  {
    public string id;
    public int level;
    public int exp;
    public int cumulative_exp;
    public int dia;
    public int coin;
    public int potion;
    public int heart;
  }

  [Serializable]
  public class UserLevelLoader : ILoader<string, UserLevel>
  {
    public List<UserLevel> items = new();

    public Dictionary<string, UserLevel> MakeDict()
    {
      Dictionary<string, UserLevel> dict = new();
      foreach (UserLevel item in items)
        dict.Add(item.id, item);

      return dict;
    }
  }
}