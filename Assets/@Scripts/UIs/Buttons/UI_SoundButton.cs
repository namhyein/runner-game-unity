using UnityEngine;
using UnityEngine.UI;

public class UI_SoundButton : UI_Base
{
    public Define.ESound soundType;
    private bool _isOn = true;

    private enum Images
    {
      Icon
    }

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;
        BindImages(typeof(Images));
        gameObject.GetOrAddComponent<Button>().BindButtonEvent(HandleOnClick);

        UpdateButtonImage();

        return true;
    }

    private void HandleOnClick()
    {
        _isOn = !_isOn;
        UpdateSoundState();
        UpdateButtonImage();
    }

    private void UpdateSoundState()
    {
        if (_isOn)
            Managers.Audio.Play(soundType);
        else
            Managers.Audio.Stop(soundType);
    }

    private void UpdateButtonImage()
    {
        // 여기에서 _isOn 상태에 따라 버튼 이미지를 변경하는 로직을 구현하세요.
        // 예: _buttonImage.sprite = _isOn ? onSprite : offSprite;
    }
}
