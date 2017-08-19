using UnityEngine;

public class StageControl : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseWindow;

    public void Restart()
    {
        GameManager.Instance.IsCanTouch = true;

        Time.timeScale = 1;

        SceneChanger.Instance.ChangeScene(SceneType.InGame);
    }

    public void SelectStage(int lv)
    {
        GameManager.Instance.StageLv = lv;

        SceneChanger.Instance.ChangeScene(SceneType.InGame);
    }

    public void NextStage()
    {
        GameManager.Instance.StageLv = GameManager.Instance.StageLv + 1;

        SceneChanger.Instance.ChangeScene(SceneType.InGame);
    }

    public void ReturnMenu()
    {
        GameManager.Instance.IsCanTouch = true;

        Time.timeScale = 1;

        SceneChanger.Instance.ChangeScene(SceneType.Menu);
    }

    public void Pause()
    {
        GameManager.Instance.IsCanTouch = false;

        pauseWindow.SetActive(true);

        Time.timeScale = 0;
    }

    public void ExitPause()
    {
        GameManager.Instance.IsCanTouch = true;

        pauseWindow.SetActive(false);

        Time.timeScale = 1;
    }
}