using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_GameStartButton : UI_Base
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        gameObject.GetOrAddComponent<Button>().BindButtonEvent(HandleOnClick);

        return true;
    }

    private void HandleOnClick()
    {
        StartCoroutine(WaitUntilFinish());

        // 예: 게임 시작 사운드 재생
        // Managers.Audio.Play(Define.ESound.Effect, "GameStartSound");
    }

    private IEnumerator WaitUntilFinish()
    {
        Animator animator = GameObject.Find("Hero").GetComponent<Animator>();
        animator.SetTrigger("GameStart");

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("GameStart"));
        Managers.Scene.LoadScene(EScene.GameScene);
    }
}
