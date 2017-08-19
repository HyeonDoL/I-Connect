using UnityEngine;
using UnityEngine.UI;

public enum MenuElement
{
    Connection = 0,
    StraightThroughConnection,
    CrossOverConnection,
    Disconnection,
    Switch,
    Router,
    AccessPoint,
    Hotspot,
    IpTag,
    AccessListTag,
    VlanTag,
    PowerSwitch
}

public class SortMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] elementTrans;

    [SerializeField]
    private RectTransform[] orderTrans;

    [SerializeField]
    private Material offMaterial;

    private const int maxElement = 12;

    private bool[] isCanUse = new bool[maxElement];

    private MenuElement[] sortArray = new MenuElement[maxElement];

    private InGameData inGameData;

    public bool[] IsCanUse() { return isCanUse; }

    public RectTransform[] GetElementsTrans()
    {
        return elementTrans;
    }

    public MenuElement[] GetSortArray()
    {
        return sortArray;
    }

    private void Awake()
    {
        inGameData = InGameManager.Instance.GetInGameData(GameManager.Instance.StageLv - 1);

        SetIsUseList();

        SortArray();

        SortPosition();
    }

    private void SetIsUseList()
    {
        isCanUse[0] = inGameData.checkMenu.connection.isUse;
        isCanUse[1] = inGameData.checkMenu.straightThroughConnection.isUse;
        isCanUse[2] = inGameData.checkMenu.crossOverConnection.isUse;
        isCanUse[3] = inGameData.checkMenu.disconnection.isUse;
        isCanUse[4] = inGameData.checkMenu._switch.isUse;
        isCanUse[5] = inGameData.checkMenu.router.isUse;
        isCanUse[6] = inGameData.checkMenu.accessPoint.isUse;
        isCanUse[7] = inGameData.checkMenu.hotspot.isUse;
        isCanUse[8] = inGameData.checkMenu.ipTag.isUse;
        isCanUse[9] = inGameData.checkMenu.accessListTag.isUse;
        isCanUse[10] = inGameData.checkMenu.vlanTag.isUse;
        isCanUse[11] = inGameData.checkMenu.powerSwitch.isUse;
    }

    private void SortArray()
    {
        int index = 0;

        for (int count = 0; count < isCanUse.Length; count++)
        {
            if (isCanUse[count])
                sortArray[index++] = (MenuElement)count;
        }

        for (int count = 0; count < isCanUse.Length; count++)
        {
            if (!isCanUse[count])
            {
                elementTrans[count].GetComponent<Image>().color = offMaterial.color;
                sortArray[index++] = (MenuElement)count;
            }
        }
    }

    private void SortPosition()
    {
        for(int i = 0; i < isCanUse.Length; i++)
            elementTrans[(int)sortArray[i]].anchoredPosition = orderTrans[i].anchoredPosition;
    }
}