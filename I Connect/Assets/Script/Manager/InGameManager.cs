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
    private CompleteSheet completeSheet;

    [SerializeField]
    private Text lvText;

    [SerializeField]
    private ParticleSystem connectParticle;

    [SerializeField]
    private GameManager[] LvList;

    private int maxCompleteCount;
    private int completeCount;

    private void Awake()
    {
        instance = this;

        completeCount = 0;

        maxCompleteCount = completeSheet.m_data[GameManager.Instance.StageLv - 1].maxCompleteCount;

        lvText.text = "Lv : " + GameManager.Instance.StageLv.ToString();
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
}