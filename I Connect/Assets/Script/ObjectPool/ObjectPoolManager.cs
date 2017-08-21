using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectPoolType
{
    Line,
    DisConnectButton,
    WirelessLines,
    Switch,
    Router,
    Data
}

[System.Serializable]
public struct PoolInfo
{
    public GameObject pool;
    public Transform group;
    public int count;
    public ObjectPoolType type;
}

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager instance = null;
    public static ObjectPoolManager Instance
    {
        get
        {
            if (instance)
                return instance;
            else
                return instance = new GameObject("ObjectPoolManager").AddComponent<ObjectPoolManager>();
        }
    }

    [SerializeField]
    private int defaultCount = 10;

    [SerializeField]
    private PoolInfo[] infoList;

    [SerializeField]
    private Hashtable objectPoolList = new Hashtable();

    [SerializeField]
    private List<DeviceConnector> connectors = new List<DeviceConnector>();

    private void Awake()
    {
        instance = this;

        StartCoroutine(InitObjectPool());
    }

    IEnumerator InitObjectPool()
    {
        for (int count = 0; count < infoList.Length; ++count)
        {
            infoList[count].pool.name = infoList[count].type.ToString();

            ObjectPool objectPool = new ObjectPool();

            objectPool.Prefab = infoList[count].pool;
            objectPool.Prefab.name = infoList[count].type.ToString();

            objectPoolList[infoList[count].type.ToString()] = objectPool;

            objectPool.Group = infoList[count].group.gameObject;

            int poolCount = defaultCount;

            if (infoList[count].count != 0)
                poolCount = infoList[count].count;

            for (int count2 = 0; count2 < poolCount; ++count2)
            {
                   
                GameObject clone = Instantiate(objectPool.Prefab);
                clone.SetActive(false);
                clone.name = infoList[count].type.ToString();
                clone.transform.parent = infoList[count].group;

                objectPool.UnusedList.Add(clone);

                yield return new WaitForFixedUpdate();
            }

            objectPool.MaxCount = count;
        }

        for (int i = 0; i < connectors.Count; i++)
        {
            if (connectors[i].transform.parent.gameObject.activeSelf)
                connectors[i].Connect();
        }
    }

    public GameObject GetObject(ObjectPoolType type, Vector3 startPosition)
    {
        if (!objectPoolList.ContainsKey(type.ToString()))
            return null;
        
        ObjectPool pool = (ObjectPool)objectPoolList[type.ToString()];

        GameObject obj;

        if (pool.UnusedList.Count > 0)
        {
            obj = pool.UnusedList[0];
            pool.UnusedList.RemoveAt(0);
            obj.SetActive(true);
        }
        else
        {
            obj = Instantiate(pool.Prefab);
            obj.transform.name = pool.Prefab.name;
            obj.transform.parent = pool.Group.transform;
        }

        obj.transform.position = startPosition;
        obj.transform.rotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;

        return obj;
    }

    public void Free(GameObject obj)
    {
        string keyName = obj.transform.name;

        if (!objectPoolList.ContainsKey(keyName))
            return;

        ObjectPool pool = (ObjectPool)objectPoolList[keyName];
        obj.SetActive(false);
        pool.UnusedList.Add(obj);
    }
}