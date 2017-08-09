using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private static SceneChanger instance = null;
    public static SceneChanger Instance
    {
        get
        {
            if (instance)
                return instance;
            else
                return instance = new GameObject("SceneChanger").AddComponent<SceneChanger>();
        }
    }

    public string SceneName { set; get; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ChangeScene(string sceneName)
    {
        SceneName = sceneName;

        SceneManager.LoadScene("Load");
    }
}

public struct SceneType
{
    public static string Title = "Title";
    public static string Menu = "Menu";
    public static string InGame = "InGame";
}