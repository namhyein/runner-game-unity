using static Theme;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;

public class UI_LanguageButton : UI_Base
{
    public Locale languageCode;
    private enum Texts 
    {
      Text
    }

    private void Start()
    {
      Managers.Localization.OnLanguageChanged += InitUI;
    }

    private void OnDestroy()
    {
      Managers.Localization.OnLanguageChanged -= InitUI;
    }

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindTexts(typeof(Texts));

        gameObject.GetOrAddComponent<Button>().BindButtonEvent(HandleOnClick);
        return true;
    }

    private void InitUI(Locale language = null)
    {
      if (Managers.Localization.CurrentLanguage.Identifier.Code == languageCode.Identifier.Code)
      {
        GetComponent<Button>().colors = new ColorBlock() 
        {
          normalColor = ColorTheme.PurpleLight01,
          highlightedColor = ColorTheme.PurpleLight01,
          pressedColor = ColorTheme.PurpleDark01,
          selectedColor = ColorTheme.PurpleLight01,
          colorMultiplier = 1.0f,
        };
        GetText((int)Texts.Text).color = ColorTheme.PurpleDark02;
      }
      else 
      {
        GetComponent<Button>().colors = new ColorBlock() 
        {
          normalColor = ColorTheme.PurpleDark01,
          highlightedColor = ColorTheme.PurpleDark01,
          pressedColor = ColorTheme.PurpleDefault,
          selectedColor = ColorTheme.PurpleDark01,
          colorMultiplier = 1.0f,
        };
        GetText((int)Texts.Text).color = ColorTheme.PurpleLight01;
      }
    }

    private void HandleOnClick()
    {
        if (languageCode != null)
            Managers.Localization.SetLanguage(languageCode);
    }

    public void SetLanguage(Locale languageCode)
    {
        this.languageCode = languageCode;

        InitUI(languageCode);
        GetText((int)Texts.Text).text = languageCode.Identifier.Code;
    }
}
