using UnityEngine.UI;
using UnityEngine;

public class LoadingTip : MonoBehaviour
{
    [SerializeField]
    private TipSheet sheet;

    [SerializeField]
    private Text tipText;

    private void Awake()
    {
        tipText.text = "Tip : " + sheet.m_data[Random.Range(0, sheet.m_data.Count)].tip;
    }
}