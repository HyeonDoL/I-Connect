using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField]
    private Text lvText;

    private void Start()
    {
        lvText.text = "Lv : " + GameManager.Instance.StageLv.ToString();
    }
}