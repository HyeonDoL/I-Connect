using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private void End()
    {
        GameManager.Instance.StageLv = 5;

        SceneChanger.Instance.ChangeScene(SceneType.Menu);
    }
}