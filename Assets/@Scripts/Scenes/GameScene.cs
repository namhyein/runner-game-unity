using UnityEngine;
using static Define;

public class GameScene : BaseScene
{
  private CameraController mainCamera = null;

  protected override bool Init()
  {
    if (base.Init() == false)
      return false;

    SceneType = EScene.GameScene;
    mainCamera = Camera.main.gameObject.GetOrAddComponent<CameraController>();

    AttachUIs();
    Managers.Game.Init();
    return true;
  }

  public override void Clear()
  {

  }

  private void AttachUIs()
  {
    GameObject sceneObj = GameObject.Find("@UI_Root");

    Canvas screenCanvas = sceneObj.GetComponent<Canvas>();
    screenCanvas.renderMode = RenderMode.ScreenSpaceCamera;
    screenCanvas.worldCamera = Camera.main;
    screenCanvas.planeDistance = 100;
    screenCanvas.sortingOrder = 20;
  }

  #region Unity Life Cycle
  void Start()
  {
    Managers.Game.Start();
  }

  void Update()
  {
    if (!Managers.Game.isRunning) return;

    Managers.Game.MoveHeroForward();
    Managers.Game.UpdateCheckpointStat();
  }

  void LateUpdate()
  {
    if (!Managers.Game.isRunning) return;

    Managers.Game.MoveHeroForward();
    mainCamera.FollowHero(Managers.Game.GetHeroPosition());
    Managers.Map.UpdateMap();
  }
  #endregion
}
