using UnityEngine;
using UnityEngine.UI;


public class UI_SoundSlider : UI_Base
{
  private enum GameObjects
  {
    Button,
    Slider,
  }

  protected override bool Init()
  {
    if (base.Init() == false)
      return false;

    BindObjects(typeof(GameObjects));

    GetObject((int)GameObjects.Slider).GetComponent<Slider>().onValueChanged.AddListener(OnValueChanged);
    return true;
  }

  public void OnValueChanged(float value)
  {
  }

  public void OnClickButton()
  {
    if (GetObject((int)GameObjects.Slider).GetComponent<Slider>().value == 0f)
      GetObject((int)GameObjects.Slider).GetComponent<Slider>().value = 0.5f;

    else
      GetObject((int)GameObjects.Slider).GetComponent<Slider>().value = 0f;
  }
}