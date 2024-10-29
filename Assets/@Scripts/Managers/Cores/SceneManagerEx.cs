using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene currentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.EScene type)
    {
        //Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    private string GetSceneName(Define.EScene type)
    {
        string name = System.Enum.GetName(typeof(Define.EScene), type);
        return name;
    }

    public void Clear()
    {
        //currentScene.Clear();
    }
}
