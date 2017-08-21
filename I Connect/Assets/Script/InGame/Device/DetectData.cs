using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectData : MonoBehaviour
{
    [SerializeField]
    private DeviceInfo deviceInfo;

    private List<DeviceData> deviceList;

    private int currentId;

    private void Awake()
    {
        deviceList = deviceInfo.ConnectedDeviceList();

        currentId = 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Data"))
        {
            Rigidbody2D currentRigid = collision.GetComponent<Rigidbody2D>();
            InGameNodeData currentData = collision.GetComponent<InGameNodeData>();

            if (currentId == currentData.NodeDeviceInfo.GetDeviceData().id)
            {
                currentData.DeleteNode();
                return;
            }

            if (deviceInfo.GetDeviceId() == currentData.EndNodeID &&
                    currentId != currentData.NodeDeviceInfo.GetDeviceId())
            {
                currentId = currentData.NodeDeviceInfo.GetDeviceId();

                currentData.DeleteNode();

                InGameManager.Instance.Complete();

                return;
            }

            for (int i = 0; i < deviceList.Count; i++)
            {
                if (deviceList[i].id == currentData.EndNodeID)
                {
                    StartCoroutine(TransmitNextDevice(currentRigid, deviceList[i].trans.position, currentData));
                    return;
                }
            }

            for (int i = 0; i < deviceList.Count; i++)
            {
                if (deviceList[i].type != DeviceType.EndDevice)
                {
                    StartCoroutine(TransmitNextDevice(currentRigid, deviceList[i].trans.position, currentData));
                    return;
                }
            }

            currentData.DeleteNode();
        }
    }

    private IEnumerator TransmitNextDevice(Rigidbody2D currentRigid, Vector2 nextDevicePosition, InGameNodeData currentData)
    {
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Tween.TweenRigidbody2D.MoveData(currentRigid, this.transform.position, nextDevicePosition, 0.8f, currentData.GetBoxCollider2D()));
    }
}