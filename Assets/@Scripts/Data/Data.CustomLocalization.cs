using System;
using System.Collections.Generic;


namespace CustomLocalization
{
  #region Localization
  [Serializable]
  public class Localization
  {
    public string id;
    public string ko;
    public string en;
    public string ja;
    public string zh;
    public string fr;
    public string es;
  }

  [Serializable]
  public class LocalizationLoader : ILoader<string, Localization>
  {
    public List<Localization> items = new();

    public Dictionary<string, Localization> MakeDict()
    {
      Dictionary<string, Localization> dict = new();
      foreach (Localization item in items)
        dict.Add(item.id, item);

      return dict;
    }
  }
  #endregion
}