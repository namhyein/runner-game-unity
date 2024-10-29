using System;
using System.Collections.Generic;

namespace Ingame
{
  #region Checkpoint
  [Serializable]
  public class Checkpoint
  {
    public string id;
    public float speed;
    public float seconds;
    public int distance;
    public int cumulative;
    public float probabilityIncrease;
  }

  [Serializable]
  public class CheckpointLoader : ILoader<string, Checkpoint>
  {
    public List<Checkpoint> items = new();

    public Dictionary<string, Checkpoint> MakeDict()
    {
      Dictionary<string, Checkpoint> dict = new();
      foreach (Checkpoint item in items)
        dict.Add(item.id, item);

      return dict;
    }
  }
  #endregion

  #region Item
  [Serializable]
  public class Item
  {
    public string id;
    public int type;
    public float speed;
    public int obstacle;
    public int monster;
    public int hole;
    public int magnet;
    public int manipulation;
    public float probability;
  }

  [Serializable]
  public class ItemLoader : ILoader<string, Item>
  {
    public List<Item> items = new();

    public Dictionary<string, Item> MakeDict()
    {
      Dictionary<string, Item> dict = new();

      foreach (Item item in items)
        dict.Add(item.id, item);

      return dict;
    }
  }
  #endregion

  #region Pattern
  [Serializable]
  public class MapPattern
  {
    public string id;
    public int checkpoint;
    public List<string> placementPatternIds;
    public float probability;
  }

  [Serializable]
  public class MapPatternLoader : ILoader<string, MapPattern>
  {
    public List<MapPattern> items = new List<MapPattern>();
    public Dictionary<string, MapPattern> MakeDict()
    {
      Dictionary<string, MapPattern> dict = new Dictionary<string, MapPattern>();
      foreach (MapPattern item in items)
        dict.Add(item.id, item);
      return dict;
    }
  }

  [Serializable]
  public class PlacementPattern
  {
    public string id;
    public string patternId;
    public int row;
    public int col1;
    public int col2;
    public int col3;
    public int col4;
    public int col5;
  }

  [Serializable]
  public class PlacementPatternLoader : ILoader<string, PlacementPattern>
  {
    public List<PlacementPattern> items = new List<PlacementPattern>();
    public Dictionary<string, PlacementPattern> MakeDict()
    {
      Dictionary<string, PlacementPattern> dict = new Dictionary<string, PlacementPattern>();
      foreach (PlacementPattern item in items)
        dict.Add(item.id, item);
      return dict;
    }
  }
  #endregion

  #region Monster
  [Serializable]
  public class Monster
  {
    public string id;
    public int minCatnip;
    public float catnip;
    public int attack;
    public int attackCatnip;
    public float attackCatnipPercent;
  }

  [Serializable]
  public class MonsterLoader : ILoader<string, Monster>
  {
    public List<Monster> items = new();

    public Dictionary<string, Monster> MakeDict()
    {
      Dictionary<string, Monster> dict = new();
      foreach (Monster item in items)
        dict.Add(item.id, item);
      return dict;
    }
  }

  [Serializable]
  public class MonsterRate
  {
    public string id;
    public int checkpoint;
    public int monsterId;
    public float probability;
  }

  [Serializable]
  public class MonsterRateLoader : ILoader<string, MonsterRate>
  {
    public List<MonsterRate> items = new();

    public Dictionary<string, MonsterRate> MakeDict()
    {
      Dictionary<string, MonsterRate> dict = new();

      foreach (MonsterRate item in items)
        dict.Add(item.id, item);

      return dict;
    }
  }
  #endregion
}
