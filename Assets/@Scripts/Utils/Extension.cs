using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public static class Extension
{
  public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
  {
    return Util.GetOrAddComponent<T>(go);
  }

  public static void BindButtonEvent(this Button go, UnityAction action = null)
  {
    UI_Base.BindButtonEvent(go, action);
  }

  public static void BindEvent(this GameObject go, Action<PointerEventData> action = null, Define.EUIEvent type = Define.EUIEvent.Click)
  {
    UI_Base.BindEvent(go, action, type);
  }

  public static bool IsValid(this GameObject go)
  {
    return go != null && go.activeSelf;
  }

  public static void DestroyChildren(this Transform transform)
  {
    foreach (Transform child in transform)
      Managers.Resource.Destroy(child.gameObject);
  }

  public static void DestroyChilds(this GameObject go)
  {
    foreach (Transform child in go.transform)
      Managers.Resource.Destroy(child.gameObject);
  }

  // public static void TranslateEx(this Transform transform, Vector3 dir)
  // {
  //   BaseObject bo = transform.gameObject.GetComponent<BaseObject>();
  //   if (bo != null)
  //     bo.TranslateEx(dir);
  // }

  public static void Shuffle<T>(this IList<T> list)
  {
    int n = list.Count;

    while (n > 1)
    {
      n--;
      int k = UnityEngine.Random.Range(0, n + 1);
      (list[k], list[n]) = (list[n], list[k]); //swap
    }
  }
}
