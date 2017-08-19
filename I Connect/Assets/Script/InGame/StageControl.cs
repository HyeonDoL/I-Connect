using UnityEngine;

public class StageControl : MonoBehaviour
{
    public void Restart()
    {
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
        SceneChanger.Instance.ChangeScene(SceneType.Menu);
    }
}