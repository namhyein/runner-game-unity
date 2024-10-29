using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
  private int _order = 10;

  public Action OnPanelOpen;
  private bool _panelOpen = false;
  public bool PanelOpened
  {
    get { return _panelOpen; }
    set
    {
      _panelOpen = value;
      OnPanelOpen?.Invoke();
    }
  }

  private Stack<UI_Panel> _panelStack = new Stack<UI_Panel>();

  private UI_Scene _sceneUI = null;
  public UI_Scene SceneUI
  {
    set { _sceneUI = value; }
    get { return _sceneUI; }
  }

  public GameObject Root
  {
    get
    {
      GameObject root = GameObject.Find("@UI_Root");
      if (root == null)
        root = new GameObject { name = "@UI_Root" };
      return root;
    }
  }

  public void SetCanvas(GameObject go, bool sort = true, int sortOrder = 0)
  {
    Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
    if (canvas == null)
    {
      canvas.renderMode = RenderMode.ScreenSpaceCamera;
      canvas.overrideSorting = true;
    }

    CanvasScaler cs = go.GetOrAddComponent<CanvasScaler>();
    if (cs != null)
    {
      cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
      cs.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
      cs.referenceResolution = new Vector2(390, 844);
    }

    go.GetOrAddComponent<GraphicRaycaster>();

    if (sort)
    {
      canvas.sortingOrder = _order;
      _order++;
    }
    else
    {
      canvas.sortingOrder = sortOrder;
    }
  }

  public T GetSceneUI<T>() where T : UI_Base
  {
    return _sceneUI as T;
  }

  public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
  {
    if (string.IsNullOrEmpty(name))
      name = typeof(T).Name;

    GameObject go = Managers.Resource.Instantiate($"{name}");
    if (parent != null)
      go.transform.SetParent(parent);

    Canvas canvas = go.GetOrAddComponent<Canvas>();
    canvas.renderMode = RenderMode.WorldSpace;
    canvas.worldCamera = Camera.main;

    return Util.GetOrAddComponent<T>(go);
  }

  public T MakeSubItem<T>(Transform parent = null, string name = null, bool pooling = true) where T : UI_Base
  {
    if (string.IsNullOrEmpty(name))
      name = typeof(T).Name;

    GameObject go = Managers.Resource.Instantiate(name, parent, pooling);
    go.transform.SetParent(parent);

    return Util.GetOrAddComponent<T>(go);
  }

  public T ShowBaseUI<T>(string name = null, Transform parent = null) where T : UI_Base
  {
    if (string.IsNullOrEmpty(name))
      name = typeof(T).Name;

    GameObject go = Managers.Resource.Instantiate(name);
    T baseUI = Util.GetOrAddComponent<T>(go);

    if (parent == null)
      parent = Root.transform;
    go.transform.SetParent(parent);

    return baseUI;
  }

  public T ShowSceneUI<T>(string name = null) where T : UI_Scene
  {
    if (string.IsNullOrEmpty(name))
      name = typeof(T).Name;

    GameObject go = Managers.Resource.Instantiate(name);
    T sceneUI = Util.GetOrAddComponent<T>(go);
    _sceneUI = sceneUI;

    go.transform.SetParent(Root.transform);

    return sceneUI;
  }

  public T ShowPanelUI<T>(string name = null, GameObject parent = null) where T : UI_Panel
  {
    if (string.IsNullOrEmpty(name))
      name = typeof(T).Name;

    if (parent == null)
      parent = Root;

    GameObject go = Managers.Resource.Instantiate(name);
    T panel = Util.GetOrAddComponent<T>(go);
    _panelStack.Push(panel);

    go.transform.SetParent(parent.transform);
    Util.StretchAll(go);
    PanelOpened = true;
    return panel;
  }

  public void ClosePanelUI(UI_Panel panel)
  {
    if (_panelStack.Count == 0)
      return;

    if (_panelStack.Peek() != panel)
      return;

    ClosePanelUI();
  }

  public void ClosePanelUI()
  {
    if (_panelStack.Count == 0)
      return;

    UI_Panel panel = _panelStack.Pop();
    Managers.Resource.Destroy(panel.gameObject);
    _order--;
    PanelOpened = _panelStack.Count > 0;
  }

  public void CloseAllPanelUI()
  {
    while (_panelStack.Count > 0)
      ClosePanelUI();
  }

  public int GetPanelCount()
  {
    return _panelStack.Count;
  }

  public void Clear()
  {
    CloseAllPanelUI();
    _sceneUI = null;
  }
}
