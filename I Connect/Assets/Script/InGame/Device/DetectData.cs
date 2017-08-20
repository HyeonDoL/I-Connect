using System.Collections.Generic;
using UnityEngine;

public class DetectData : MonoBehaviour
{
    [SerializeField]
    private DeviceInfo deviceInfo;

    private List<DeviceData> deviceList;

    private Transform currentTrans;
    private Rigidbody2D currentRigid;
    private InGameNodeData currentData;
    private List<DeviceData> currentDeviceList;

    private bool isConnected;

    private void Awake()
    {
        deviceList = deviceInfo.ConnectedDeviceList();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Data"))
        {
            currentTrans = collision.transform;
            currentRigid = collision.GetComponent<Rigidbody2D>();
            currentData = collision.GetComponent<InGameNodeData>();
            currentDeviceList = currentData.NodeDeviceInfo.ConnectedDeviceList();

            isConnected = false;

            for (int i = 0; i < deviceList.Count; i++)
            {
                if (deviceList[i].id == currentData.NodeDeviceInfo.GetDeviceId())
                    isConnected = true;
            }

            if (ConnectData())
                return;

            if (ConnectedSuchEndNode())
                return;

            if (ConnectedSuchDevice())
                return;

            currentData.DeleteNode();
        }
    }

    private bool ConnectData()
    {
        if (deviceInfo.GetDeviceId() == currentData.EndNodeID && !isConnected)
        {
            deviceList.Add(currentData.NodeDeviceInfo.GetDeviceData());

            currentData.DeleteNode();

            InGameManager.Instance.Complete();

            return true;
        }

        return false;
    }

    private bool ConnectedSuchEndNode()
    {
        for (int i = 0; i < deviceList.Count; i++)
        {
            if (deviceList[i].id == currentData.EndNodeID)
            {
                StartCoroutine(Tween.TweenRigidbody2D.MoveData(currentRigid, this.transform.position, currentTrans.position, 0.8f, currentData.GetBoxCollider2D()));
                return true;
            }
        }

        return false;
    }

    private bool ConnectedSuchDevice()
    {
        for (int i = 0; i < deviceList.Count; i++)
        {
            if (deviceList[i].type == DeviceType.Router ||
                deviceList[i].type == DeviceType.Switch)
            {
                StartCoroutine(Tween.TweenRigidbody2D.MoveData(currentRigid, this.transform.position, deviceList[i].trans.position, 0.8f, currentData.GetBoxCollider2D()));
                return true;
            }
        }
        return false;
    }
}