using UnityEngine;
using UnityEngine.Localization;

public class UI_SettingPanel : UI_Panel
{
  private enum GameObjects
  {
    LanguageSection,
  }

  protected override bool Init()
  {
    if (base.Init() == false)
      return false;

    BindObjects(typeof(GameObjects));
    InitLanguageButtons();
    return true;
  }

  public void InitLanguageButtons()
  {
    var locales = UnityEngine.Localization.Settings.LocalizationSettings.AvailableLocales.Locales;
    GameObject languageSection = GetObject((int)GameObjects.LanguageSection);

    foreach (Locale language in locales)
    {
      UI_LanguageButton languageButton = Managers.UI.MakeSubItem<UI_LanguageButton>(languageSection.transform);

      if (languageButton != null)
        languageButton.SetLanguage(language);
    }
  }
}
