using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private void End()
    {
        SceneChanger.Instance.ChangeScene(SceneType.Menu);
    }
}