using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectSpawnType
{
    None,
    Switch,
    Router
}

public class SelectElement : MonoBehaviour
{
    [SerializeField]
    private SortMenu sortMenu;

    [SerializeField]
    private RectTransform selectOutline;

    [SerializeField]
    private RectTransform disConnectGroup;

    private bool[] isCanUse;

    private SelectSpawnType spawnType;

    private RectTransform[] elements;

    private void Awake()
    {
        elements = sortMenu.GetElementsTrans();

        isCanUse = sortMenu.IsCanUse();
    }

    public void ClickConnection()
    {
        if (!isCanUse[(int)MenuElement.Connection])
            return;

        Select();

        InGameManager.Instance.isCanConnect = true;

        SelectOutline(MenuElement.Connection);
    }

    public void ClickStraightThroughConnection()
    {
        if (!isCanUse[(int)MenuElement.StraightThroughConnection])
            return;

        Select();

        SelectOutline(MenuElement.StraightThroughConnection);
    }

    public void ClickCrossOverConnection()
    {
        if (!isCanUse[(int)MenuElement.CrossOverConnection])
            return;

        Select();

        SelectOutline(MenuElement.CrossOverConnection);
    }

    public void ClickDisconnection()
    {
        if (!isCanUse[(int)MenuElement.Disconnection])
            return;

        Select();

        disConnectGroup.gameObject.SetActive(true);

        SelectOutline(MenuElement.Disconnection);
    }

    public void ClickSwitch()
    {
        if (!isCanUse[(int)MenuElement.Switch])
            return;

        Select();

        spawnType = SelectSpawnType.Switch;

        SelectOutline(MenuElement.Switch);
    }

    public void ClickRouter()
    {
        if (!isCanUse[(int)MenuElement.Router])
            return;

        Select();

        spawnType = SelectSpawnType.Router;

        SelectOutline(MenuElement.Router);
    }

    //public void ClickAccessPoint()
    //{

    //}

    public void ClickHotspot()
    {
        if (!isCanUse[(int)MenuElement.Hotspot])
            return;

        Select();

        SelectOutline(MenuElement.Hotspot);
    }

    //public void ClickIpTag()
    //{

    //}

    //public void ClickAccessListTag()
    //{

    //}

    //public void ClickVlanTag()
    //{

    //}

    public void ClickPowerSwitch()
    {
        if (!isCanUse[(int)MenuElement.PowerSwitch])
            return;

        Select();

        SelectOutline(MenuElement.PowerSwitch);
    }

    private void SelectOutline(MenuElement index)
    {
        selectOutline.anchoredPosition = elements[(int)index].anchoredPosition;
    }

    private void Select()
    {
        spawnType = SelectSpawnType.None;

        disConnectGroup.gameObject.SetActive(false);

        InGameManager.Instance.isCanConnect = false;
    }

    private void Update()
    {
        if(spawnType != SelectSpawnType.None)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100.0f);

                if(hit.transform != null)
                {
                    if(hit.transform.name == "Switch" && spawnType == SelectSpawnType.Switch)
                    {
                        ObjectPoolManager.Instance.Free(hit.transform.gameObject);
                    }
                    else if(hit.transform.name == "Router" && spawnType == SelectSpawnType.Router)
                    {
                        ObjectPoolManager.Instance.Free(hit.transform.gameObject);
                    }
                }
                else
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    GameObject spawnObject;

                    if (spawnType == SelectSpawnType.Switch)
                    {
                        spawnObject = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Switch, mousePosition);
                        spawnObject.transform.localScale = new Vector2(0.2f, 0.2f);
                    }
                    else if (spawnType == SelectSpawnType.Router)
                    {
                        spawnObject = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Router, mousePosition);
                        spawnObject.transform.localScale = new Vector2(0.2f, 0.2f);
                    }
                }
            }
        }
    }
}