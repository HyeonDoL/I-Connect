using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CableType
{
    None,
    CrossOver,
    StraightThrough
}

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

        InGameManager.Instance.SelectCableType = CableType.StraightThrough;

        SelectOutline(MenuElement.StraightThroughConnection);
    }

    public void ClickCrossOverConnection()
    {
        if (!isCanUse[(int)MenuElement.CrossOverConnection])
            return;

        Select();

        InGameManager.Instance.SelectCableType = CableType.CrossOver;

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
        InGameManager.Instance.isClickButton = true;

        spawnType = SelectSpawnType.None;

        disConnectGroup.gameObject.SetActive(false);

        InGameManager.Instance.isCanConnect = false;
    }

    private void Update()
    {
        if(spawnType != SelectSpawnType.None)
        {
            if(Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100.0f);

                if(hit.transform != null)
                {
                    if((hit.transform.name == "Switch" && spawnType == SelectSpawnType.Switch) ||
                        (hit.transform.name == "Router" && spawnType == SelectSpawnType.Router))
                    {
                        StartCoroutine(Free(hit.transform));
                    }
                }

                else
                {
                    if (InGameManager.Instance.isClickButton)
                        return;

                    if (spawnType != SelectSpawnType.None)
                        Spawn(spawnType);
                }
            }

            else if(Input.GetMouseButtonDown(0))
                InGameManager.Instance.isClickButton = false;

        }
    }

    private IEnumerator Free(Transform target)
    {
        yield return StartCoroutine(Tween.TweenTransform.LocalScale(target.transform, new Vector2(0.05f, 0.05f), 0.5f));

        ObjectPoolManager.Instance.Free(target.transform.gameObject);
    }

    private void Spawn(SelectSpawnType type)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject spawnObject;

        ObjectPoolType poolType = SelectSpawnType.Router == type ? ObjectPoolType.Router : ObjectPoolType.Switch;

        spawnObject = ObjectPoolManager.Instance.GetObject(poolType, mousePosition);
        spawnObject.transform.localScale = new Vector2(0.05f, 0.05f);

        StartCoroutine(Tween.TweenTransform.LocalScale(spawnObject.transform, new Vector2(0.2f, 0.2f), 0.5f));
    }
}