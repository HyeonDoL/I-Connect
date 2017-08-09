using UnityEngine;

public class InGameButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject completeWindow;
    
    public void ReturnMenu()
    {
        SceneChanger.Instance.ChangeScene(SceneType.Menu);
    }

    public void Complete()
    {
        completeWindow.SetActive(true);
    }
}