using static Define;

using UnityEngine;
using System.Collections;

public class UI_SplashScene : UI_Scene
{
    private enum GameObjects
    {
        SoldierL,
        SoldierR,
        Hero
    }

    private enum Images
    {
        Button
    }

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;
        BindObjects(typeof(GameObjects));
        BindImages(typeof(Images));

        LoadAllAssets();

        return true;
    }

    public void SplashAnimation()
    {
        GetObject((int)GameObjects.Hero).GetComponent<Hero>().Attack();

        GetComponent<Animator>().SetTrigger("Finish");
    }

    private void LoadAllAssets()
    {
        Managers.Resource.LoadAllAsync<Object>("Preload", (key, count, totalCount) =>
        {
            if (count == totalCount)
            {
                Managers.InitializeOnAddressable();
                StartCoroutine(FinishSplashAndLoadLobby());
            }
        });
    }

    private IEnumerator FinishSplashAndLoadLobby()
    {
        Animator animator = GetComponent<Animator>();
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Finish"));

        GetObject((int)GameObjects.SoldierL).GetComponent<Monster>().Damaged();
        GetObject((int)GameObjects.SoldierR).GetComponent<Monster>().Damaged();
        yield return new WaitForSeconds(.5f);

        Managers.Scene.LoadScene(EScene.LobbyScene);
    }
}
