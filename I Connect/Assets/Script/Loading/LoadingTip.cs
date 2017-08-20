using UnityEngine.UI;
using UnityEngine;

public class LoadingTip : MonoBehaviour
{
    [SerializeField]
    private TipSheet sheet;

    [SerializeField]
    private Image tipText;

    [SerializeField]
    private Vector2[] StageTipRange;

    private void Awake()
    {
        tipText.sprite =  sheet.m_data[Random.Range(Mathf.RoundToInt( StageTipRange[ GameManager.Instance.StageLv-1].x), Mathf.RoundToInt( StageTipRange[GameManager.Instance.StageLv-1].y))].tip;
    }
}