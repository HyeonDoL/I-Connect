using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectData : MonoBehaviour
{
    [SerializeField]
    private DeviceInfo deviceInfo;

    private List<DeviceData> deviceList;

    private int currentId;

    private Transform currentTrans;
    private Rigidbody2D currentRigid;
    private InGameNodeData currentData;

    private void Awake()
    {
        deviceList = deviceInfo.ConnectedDeviceList();

        currentId = 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Data"))
        {
            currentTrans = collision.transform;
            currentRigid = collision.GetComponent<Rigidbody2D>();
            currentData = collision.GetComponent<InGameNodeData>();

            if(currentId == currentData.NodeDeviceInfo.GetDeviceData().id)
                currentData.DeleteNode();


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
        if (deviceInfo.GetDeviceId() == currentData.EndNodeID &&
            currentId != currentData.NodeDeviceInfo.GetDeviceId())
        {
            currentId = currentData.NodeDeviceInfo.GetDeviceId();

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
}