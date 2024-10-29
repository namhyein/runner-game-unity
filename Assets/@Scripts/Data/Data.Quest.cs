using System;
using System.Collections.Generic;


namespace Quest
{
  [Serializable]
  public class Quest
  {
    public string id;
    public int type;
    public int priority;
    public int score;
    public int init;
    public int rule;
    public int ex;
  }

  [Serializable]
  public class QuestLoader : ILoader<string, Quest>
  {
    public List<Quest> items = new();
    public Dictionary<string, Quest> MakeDict()
    {
      Dictionary<string, Quest> dict = new();
      foreach (Quest item in items)
        dict.Add(item.id, item);

      return dict;
    }
  }
}