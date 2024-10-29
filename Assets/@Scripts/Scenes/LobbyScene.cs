using UnityEngine;
using static Define;

public class LobbyScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = EScene.LobbyScene;
        return true;
    }

    public override void Clear()
    {

    }

    void Start()
    {
        Managers.Map.Init();
        Managers.Map.LoadMap();

        SpawnHero();
    }

    private void SpawnHero()
    {
        Vector3 position = Managers.Map.GetTilemapCenter();
        position.y -= 1f;

        Transform hero = GameObject.Find("@Heroes").transform;
        GameObject go = Managers.Resource.Instantiate("HeroFront", hero);

        go.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        go.transform.position = position;
        go.name = "Hero";
    }
}
