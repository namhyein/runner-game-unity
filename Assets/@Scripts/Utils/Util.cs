using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Util
{
	public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
	{
		T component = go.GetComponent<T>();
		if (component == null)
			component = go.AddComponent<T>();

		return component;
	}

	public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
	{
		Transform transform = FindChild<Transform>(go, name, recursive);
		if (transform == null)
			return null;

		return transform.gameObject;
	}

	public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
	{
		if (go == null)
			return null;

		if (recursive == false)
		{
			for (int i = 0; i < go.transform.childCount; i++)
			{
				Transform transform = go.transform.GetChild(i);
				if (string.IsNullOrEmpty(name) || transform.name == name)
				{
					T component = transform.GetComponent<T>();
					if (component != null)
						return component;
				}
			}
		}
		else
		{
			foreach (T component in go.GetComponentsInChildren<T>())
			{
				if (string.IsNullOrEmpty(name) || component.name == name)
					return component;
			}
		}

		return null;
	}

	public static T ParseEnum<T>(string value)
	{
		return (T)Enum.Parse(typeof(T), value, true);
	}

	public static GameObject StretchAll(GameObject go)
	{
		/**************************************************************************
		 * Summary:
		 * GameObject의 RectTransform를 Stretch All로 설정
		 * 
		 * Parameters:
		 * GameObject go: 크기를 Stretch All로 설정할 GameObject
		 * 
		 * Returns:
		 * GameObject: 크기를 Stretch All로 설정한 GameObject
		 ************************************************************************/

		RectTransform rectTransform = go.GetComponent<RectTransform>();
		if (rectTransform == null)
			return go;

		// 앵커 프리셋을 Stretch All로 설정
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(1, 1);

		// left, right, top, bottom을 0으로 설정
		rectTransform.offsetMin = new Vector2(0, 0); // left, bottom
		rectTransform.offsetMax = new Vector2(0, 0); // right, top

		// scale을 (1, 1, 0)으로 설정
		rectTransform.localScale = new Vector3(1, 1, 0);

		return go;
	}

	public static Color32 ConvertHexToColor(string hex, byte a = 0xFF)
	{
		byte r = byte.Parse(hex.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);


		return new Color32(r, g, b, a);
	}
}