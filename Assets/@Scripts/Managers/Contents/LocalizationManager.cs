using System;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationManager
{
  private const string LanguagePrefsKey = "SelectedLanguage";

  private Locale _currentLanguage;
  public Action<Locale> OnLanguageChanged;
  public Locale CurrentLanguage
  {
    get { return _currentLanguage; }
    private set
    {
      _currentLanguage = value;
      OnLanguageChanged?.Invoke(_currentLanguage);
    }
  }

  public void Init()
  {
    LocalizationSettings.InitializationOperation.WaitForCompletion();

    string savedLanguage = PlayerPrefs.GetString(LanguagePrefsKey);
    if (!string.IsNullOrEmpty(savedLanguage))
    {
      Locale locale = LocalizationSettings.AvailableLocales.GetLocale(savedLanguage);
      if (locale != null)
        SetLanguage(locale);
      else
        SetLanguage(LocalizationSettings.AvailableLocales.GetLocale("en"));
    }
    else
    {
      CurrentLanguage = LocalizationSettings.SelectedLocale;
    }
  }

  public void SetLanguage(Locale locale)
  {
    // 언어 설정
    CurrentLanguage = locale;
    LocalizationSettings.SelectedLocale = locale;
    PlayerPrefs.SetString(LanguagePrefsKey, locale.Identifier.Code);
    PlayerPrefs.Save();
  }

  public string GetLocalizedString(string tableReference, string entryReference)
  {
    // 현재 선택된 언어로 문자열 가져오기
    var localizedString = LocalizationSettings.StringDatabase.GetLocalizedString(tableReference, entryReference);
    return localizedString;
  }

  public string GetLocalizedString(string key)
  {
    return Managers.Data.Localizations[key].GetType().GetProperty(CurrentLanguage.Identifier.Code).GetValue(Managers.Data.Localizations[key]).ToString();
  }
}
