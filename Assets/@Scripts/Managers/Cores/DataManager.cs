using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public interface ILoader<Key, Value>
{
  Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
  public Dictionary<string, Quest.Quest> Achievements { get; private set; }
  public Dictionary<string, Quest.Quest> DailyQuests { get; private set; }
  public Dictionary<string, Quest.Quest> WeeklyQuests { get; private set; }
  public Dictionary<string, Ingame.Checkpoint> CheckPoints { get; private set; }
  public Dictionary<string, Ingame.Item> Items { get; private set; }
  public Dictionary<string, IAP.Purchase> Purchases { get; private set; }
  public Dictionary<string, UserLevel.UserLevel> UserLevels { get; private set; }
  public Dictionary<string, CustomLocalization.Localization> Localizations { get; private set; }
  public Dictionary<string, Ingame.MapPattern> MapPatterns { get; private set; }
  public Dictionary<string, Ingame.PlacementPattern> PlacementPatterns { get; private set; }
  public Dictionary<string, Ingame.MonsterRate> MonsterRates { get; private set; }
  public Dictionary<string, Ingame.Monster> Monsters { get; private set; }

  public void Init()
  {
    Achievements = LoadJson<Quest.QuestLoader, string, Quest.Quest>("Achievement").MakeDict();
    DailyQuests = LoadJson<Quest.QuestLoader, string, Quest.Quest>("DailyQuest").MakeDict();
    WeeklyQuests = LoadJson<Quest.QuestLoader, string, Quest.Quest>("WeeklyQuest").MakeDict();
    CheckPoints = LoadJson<Ingame.CheckpointLoader, string, Ingame.Checkpoint>("Checkpoint").MakeDict();
    Items = LoadJson<Ingame.ItemLoader, string, Ingame.Item>("Item").MakeDict();
    Purchases = LoadJson<IAP.PurchaseLoader, string, IAP.Purchase>("InAppPurchase").MakeDict();
    UserLevels = LoadJson<UserLevel.UserLevelLoader, string, UserLevel.UserLevel>("UserLevel").MakeDict();
    Localizations = LoadJson<CustomLocalization.LocalizationLoader, string, CustomLocalization.Localization>("CustomLocalization").MakeDict();
    MapPatterns = LoadJson<Ingame.MapPatternLoader, string, Ingame.MapPattern>("MapPattern").MakeDict();
    PlacementPatterns = LoadJson<Ingame.PlacementPatternLoader, string, Ingame.PlacementPattern>("PlacementPattern").MakeDict();
    MonsterRates = LoadJson<Ingame.MonsterRateLoader, string, Ingame.MonsterRate>("MonsterRate").MakeDict();
    Monsters = LoadJson<Ingame.MonsterLoader, string, Ingame.Monster>("MonsterStat").MakeDict();
  }

  private Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
  {
    TextAsset textAsset = Managers.Resource.Load<TextAsset>(path);
    return JsonConvert.DeserializeObject<Loader>(textAsset.text);
  }
}
