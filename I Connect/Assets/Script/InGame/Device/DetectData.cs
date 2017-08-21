using System.Collections;
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

    public bool test = false;

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

            if (ConnectData())
                return;

            isConnected = false;

            for (int i = 0; i < deviceList.Count; i++)
            {
                if (deviceList[i].id == currentData.NodeDeviceInfo.GetDeviceId())
                    isConnected = true;
            }

            if (!isConnected)
                deviceList.Add(currentData.NodeDeviceInfo.GetDeviceData());


            if (ConnectedSuchEndNode())
                return;


            if (ConnectedSuchDevice())
                return;


            currentData.DeleteNode();
        }
    }

    private bool ConnectData()
    {
        if (deviceInfo.GetDeviceId() == currentData.EndNodeID && deviceList.Count == 0)
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
                StartCoroutine(TransmitNextDevice(deviceList[i].trans.position));
                return true;
            }
        }

        return false;
    }

    private bool ConnectedSuchDevice()
    {
        for (int i = 0; i < deviceList.Count; i++)
        {
            if (deviceList[i].type != DeviceType.EndDevice)
            {
                StartCoroutine(TransmitNextDevice(deviceList[i].trans.position));
                return true;
            }
        }
        return false;
    }

    private IEnumerator TransmitNextDevice(Vector2 nextDevicePosition)
    {
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Tween.TweenRigidbody2D.MoveData(currentRigid, this.transform.position, nextDevicePosition, 0.8f, currentData.GetBoxCollider2D()));
    }

    private void Update()
    {
        if(test)
        {
            test = false;

            Debug.Log("-------------" + this.transform.name + "----------------");

            for(int i = 0; i < deviceList.Count; i++)
            {
                Debug.Log(i + " : " + deviceList[i].trans.name);
            }
        }
    }
}