using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    private static InGameManager instance;
    public static InGameManager Instance
    {
        get
        {
            if (instance)
                return instance;
            else
                return instance = new GameObject("InGameManager").AddComponent<InGameManager>();
        }
    }

    [SerializeField]
    private GameObject clearWindow;

    [SerializeField]
    private InGameSheet inGameSheet;

    [SerializeField]
    private ParticleSystem connectParticle;

    [SerializeField]
    private GameObject[] lvList;

    private int maxCompleteCount;
    private int completeCount;

    public bool isCanConnect { get; set; }

    public bool isClickButton { get; set; }

    private void Awake()
    {
        instance = this;

        isCanConnect = false;
        isClickButton = false;

        completeCount = 0;

        maxCompleteCount = inGameSheet.m_data[GameManager.Instance.StageLv - 1].maxComplete;

        lvList[GameManager.Instance.StageLv - 1].SetActive(true);
    }

    public void DisConnect()
    {
        completeCount -= 1;
    }

    public void Complete()
    {
        completeCount += 1;

        if (completeCount >= maxCompleteCount)
            Success();
    }

    public void Success()
    {
        clearWindow.SetActive(true);
    }

    public ParticleSystem GetConnectParticle()
    {
        return connectParticle;
    }

    public InGameData GetInGameData(int index)
    {
        return inGameSheet.m_data[index];
    }
}