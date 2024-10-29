using UnityEngine;
using UnityEngine.UI;

public class UI_StorePanel : UI_Panel
{
    private enum GameObjects
    {
        ScrollView,
        Packages,
        Items,
        Coins,
        Dias
    }

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObjects(typeof(GameObjects));
        // 다른 초기화 코드...

        return true;
    }

    public void InitializeUI()
    {
        Transform coins = GetObject((int)GameObjects.Coins).transform;
        Transform dias = GetObject((int)GameObjects.Dias).transform;
        Transform items = GetObject((int)GameObjects.Items).transform;
        Transform packages = GetObject((int)GameObjects.Packages).transform;

        coins.DestroyChildren();
        dias.DestroyChildren();
        items.DestroyChildren();
        packages.DestroyChildren();

        foreach (var iap in Managers.Data.Purchases)
        {

        }

    }

    public void ScrollToObject(string objectName)
    {
        ScrollRect scrollRect = GetObject((int)GameObjects.ScrollView).GetComponent<ScrollRect>();
        RectTransform content = scrollRect.content;
        RectTransform targetObject = content.Find(objectName) as RectTransform;

        if (targetObject != null)
        {
            Canvas.ForceUpdateCanvases();
            Vector2 viewportLocalPosition = scrollRect.viewport.localPosition;
            Vector2 targetLocalPosition = content.InverseTransformPoint(targetObject.position);
            float normalizedPosition = (viewportLocalPosition.y - targetLocalPosition.y) / (content.rect.height - scrollRect.viewport.rect.height);
            scrollRect.verticalNormalizedPosition = Mathf.Clamp01(1 - normalizedPosition);
        }
    }
}
