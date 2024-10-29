using UnityEngine;
using UnityEngine.EventSystems;

public class UI_TouchPad : UI_Base
{
    private Vector2 _touchPos;
    private float _jumpThreshold = 100f;
    private float _rollThreshold = 100f;
    private bool isDragging = false;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        gameObject.BindEvent(OnPointerDown, type: Define.EUIEvent.PointerDown);
        gameObject.BindEvent(OnPointerUp, type: Define.EUIEvent.PointerUp);
        // gameObject.BindEvent(OnDrag, type: Define.EUIEvent.Drag);

        return true;
    }

    #region Event
    public void OnPointerDown(PointerEventData eventData)
    {
        _touchPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 touchDir = eventData.position - _touchPos;
        if (touchDir.y > _jumpThreshold)
            Managers.Game.HeroJump();
        else if (touchDir.y < -_rollThreshold)
            Managers.Game.UseItem();
    }

    #endregion
}
