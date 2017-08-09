using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    private readonly float minimumTime = 2f;

    [SerializeField]
    private Image loadingBar;

    private bool isDone = false;
    private float time = 0f;
    AsyncOperation asyncOperation;

    private void Start()
    {
        StartCoroutine(StartLoad(SceneChanger.Instance.SceneName));
    }

    private void Update()
    {
        time += Time.deltaTime;
        loadingBar.fillAmount = time;

        if (time >= minimumTime)
            asyncOperation.allowSceneActivation = true;
    }

    public IEnumerator StartLoad(string sceneName)
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        if(!isDone)
        {
            isDone = true;

            while(asyncOperation.progress < 0.9f)
            {
                loadingBar.fillAmount = asyncOperation.progress;   // asyncOperation.progress는 로딩 진척율 0~1

                yield return null;
            }
        }
    }
}