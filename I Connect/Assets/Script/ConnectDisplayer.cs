using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ConnectDisplayer : MonoBehaviour
{
    public enum DeviceType
    {
        EndDevice = 0,
        Switch = 1,
        Router = 2
    }
    [SerializeField]
    private LimitMaxLine myDeviceInfo;

    [SerializeField]
    private DeviceType myType;
    [SerializeField]
    private SpriteRenderer[] SokectImages;
    private int MaxConnection;

    [SerializeField]
    private Transform[] DisplayContainers;

    [SerializeField]
    private Color ConnectedColor;

    [SerializeField]
    private Color EmptyColor;

    private int ConnectIndex;
    private int ConnectIndexer
    {
        set { ConnectIndex = value < 0 ? 0 : value > MaxConnection ? MaxConnection : value; }
        get { return ConnectIndex; }
    }

    // Use this for initialization
    void Awake()
    {
        myDeviceInfo = this.transform.parent.GetComponent<LimitMaxLine>();
        //myDeviceInfo.Displayer = this;

        MaxConnection = SokectImages.Length-1;
        ConnectIndexer = 0;
        Debug.Log("ConnectIndexer :" + ConnectIndexer);
        for (int i = 0; i < DisplayContainers.Length; ++i)
            SetActiveChilds(DisplayContainers[i], false);
        SetActiveChilds(DisplayContainers[(int)myType], true);

        int len =-1;

        switch (myType)
        {
            case DeviceType.EndDevice: len = 1;break;
            case DeviceType.Switch: len = 5; break;
            case DeviceType.Router: len = 3; break;
        }

        SokectImages = new SpriteRenderer[len];

        for(int i=0;i<len;++i)
            SokectImages[i] = DisplayContainers[(int)myType].GetChild(i).GetComponent<SpriteRenderer>();
        

    }

    private void SetActiveChilds(Transform target,bool value)
    {
        for (int i=0;i<target.childCount; ++i)
            target.GetChild(i).gameObject.SetActive(value);
        
    }

    public bool AddConnection()
    {
        SokectImages[ConnectIndexer++].color = ConnectedColor;
        return true;
    }

    public bool DeleteConnection()
    {
        SokectImages[ConnectIndexer--].color = EmptyColor;
        return true;
    }
}