using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;

public class DataTransformer : EditorWindow
{
#if UNITY_EDITOR
    [MenuItem("Tools/ParseExcel %#K")]
    public static void ParseExcelDataToJson()
    {
        ParseExcelDataToJson<CustomLocalization.LocalizationLoader, CustomLocalization.Localization>("CustomLocalization");
        ParseExcelDataToJson<UserLevel.UserLevelLoader, UserLevel.UserLevel>("UserLevel");
        ParseExcelDataToJson<Ingame.CheckpointLoader, Ingame.Checkpoint>("Checkpoint");
        ParseExcelDataToJson<IAP.PurchaseLoader, IAP.Purchase>("InAppPurchase");
        ParseExcelDataToJson<Quest.QuestLoader, Quest.Quest>("DailyQuest");
        ParseExcelDataToJson<Quest.QuestLoader, Quest.Quest>("WeeklyQuest");
        ParseExcelDataToJson<Quest.QuestLoader, Quest.Quest>("Achievement");
        ParseExcelDataToJson<Ingame.ItemLoader, Ingame.Item>("Item");
        ParseExcelDataToJson<Ingame.MonsterRateLoader, Ingame.MonsterRate>("MonsterRate");
        ParseExcelDataToJson<Ingame.MapPatternLoader, Ingame.MapPattern>("MapPattern");
        ParseExcelDataToJson<Ingame.PlacementPatternLoader, Ingame.PlacementPattern>("PlacementPattern");
        ParseExcelDataToJson<Ingame.MonsterLoader, Ingame.Monster>("Monster");
    }

    #region Helpers
    private static void ParseExcelDataToJson<Loader, LoaderData>(string filename) where Loader : new() where LoaderData : new()
    {
        Loader loader = new();
        FieldInfo field = loader.GetType().GetFields()[0];
        field.SetValue(loader, ParseExcelDataToList<LoaderData>(filename));

        string jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/@Resources/Data/JsonData/{filename}.json", jsonStr);
        AssetDatabase.Refresh();
    }

    private static List<LoaderData> ParseExcelDataToList<LoaderData>(string filename) where LoaderData : new()
    {
        List<LoaderData> loaderDatas = new List<LoaderData>();

        string[] lines = File.ReadAllText($"{Application.dataPath}/@Resources/Data/ExcelData/{filename}.csv").Split('\n');

        for (int l = 1; l < lines.Length; l++)
        {
            string[] row = ParseCSVLine(lines[l]);
            if (row.Length == 0 || string.IsNullOrEmpty(row[0]))
                continue;

            LoaderData loaderData = new();

            FieldInfo[] fields = typeof(LoaderData).GetFields();
            for (int f = 0; f < fields.Length && f < row.Length; f++)
            {
                FieldInfo field = loaderData.GetType().GetField(fields[f].Name);
                Type type = field.FieldType;

                if (type.IsGenericType)
                {
                    object value = ConvertList(row[f], type);
                    field.SetValue(loaderData, value);
                }
                else
                {
                    object value = ConvertValue(row[f], field.FieldType);
                    field.SetValue(loaderData, value);
                }
            }

            loaderDatas.Add(loaderData);
        }

        return loaderDatas;
    }

    private static string[] ParseCSVLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string field = "";

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (line[i] == ',' && !inQuotes)
            {
                result.Add(field.Trim());
                field = "";
            }
            else
            {
                field += line[i];
            }
        }

        if (!string.IsNullOrEmpty(field))
        {
            result.Add(field.Trim());
        }

        return result.ToArray();
    }

    private static object ConvertValue(string value, Type type)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        TypeConverter converter = TypeDescriptor.GetConverter(type);
        return converter.ConvertFromString(value);
    }

    private static object ConvertList(string value, Type type)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        // Reflection
        Type valueType = type.GetGenericArguments()[0];
        Type genericListType = typeof(List<>).MakeGenericType(valueType);
        var genericList = Activator.CreateInstance(genericListType) as IList;

        // Parse Excel
        var list = value.Split('&').Select(x => ConvertValue(x, valueType)).ToList();

        foreach (var item in list)
            genericList.Add(item);

        return genericList;
    }
    #endregion

#endif
}