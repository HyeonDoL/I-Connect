using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DeviceType
{
    EndDevice,
    Switch,
    Router
}

[Serializable]
public struct DeviceData
{
    public int id;
    public Transform trans;
    public DeviceType type;
}

public class DeviceInfo : MonoBehaviour
{
    [SerializeField]
    private DeviceData deviceData;

    [SerializeField]
    private LimitMaxLine maxLine;

    private List<DeviceData> connectedDeviceList = new List<DeviceData>();

    public int GetDeviceId()
    {
        return deviceData.id;
    }

    public DeviceType GetDeviceType()
    {
        return deviceData.type;
    }

    public DeviceData GetDeviceData()
    {
        return deviceData;
    }

    public LimitMaxLine GetLimitMaxLine()
    {
        return maxLine;
    }

    public List<DeviceData> ConnectedDeviceList()
    {
        return connectedDeviceList;
    }
}