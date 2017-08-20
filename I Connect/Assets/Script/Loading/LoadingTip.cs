using UnityEngine.UI;
using UnityEngine;

public class LoadingTip : MonoBehaviour
{
    [SerializeField]
    private TipSheet sheet;

    [SerializeField]
    private Image TipImage;

    [SerializeField]
    private Text TipText;

    [SerializeField]
    private Vector2[] StageTipRange;

    private void Awake()
    {
        int index = Random.Range(Mathf.RoundToInt(StageTipRange[GameManager.Instance.StageLv - 1].x), Mathf.RoundToInt(StageTipRange[GameManager.Instance.StageLv - 1].y));
        TipImage.sprite =  sheet.m_data[index].tip;
        TipText.text = sheet.m_data[index].TipText;
    }
}