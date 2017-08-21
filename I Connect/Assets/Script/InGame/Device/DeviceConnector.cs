using UnityEngine;
using System;
using System.Collections;

public class DeviceConnector : MonoBehaviour
{
    [SerializeField]
    private DeviceInfo deviceInfo1;

    [SerializeField]
    private DeviceInfo deviceInfo2;

    private bool isConnected;

    private UIMeshLine line;

    private InGameData inGameData;

    private void Awake()
    {
        inGameData = InGameManager.Instance.GetInGameSheet().m_data[GameManager.Instance.StageLv - 1];
    }

    public void Connect()
    {
        isConnected = true;

        line = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Line, Vector3.zero).GetComponent<UIMeshLine>();

        Vector2 objectPosition = Camera.main.WorldToViewportPoint(deviceInfo1.GetDeviceData().trans.position);

        Vector2 position = new Vector2((objectPosition.x - 0.5f) * 1600, (objectPosition.y - 0.5f) * 900);

        line.SetPointPosition(0, position);

        objectPosition = Camera.main.WorldToViewportPoint(deviceInfo2.GetDeviceData().trans.position);

        position = new Vector2((objectPosition.x - 0.5f) * 1600, (objectPosition.y - 0.5f) * 900);

        line.SetPointPosition(1, position);

        deviceInfo1.GetLimitMaxLine().Connect();
        deviceInfo2.GetLimitMaxLine().Connect();

        MeshLineManager.Instance.Connect(line, deviceInfo1, deviceInfo2);

        deviceInfo1.ConnectedDeviceList().Add(deviceInfo2.GetDeviceData());
        deviceInfo2.ConnectedDeviceList().Add(deviceInfo1.GetDeviceData());

        if (deviceInfo1.GetDeviceType() == DeviceType.EndDevice)
            StartCoroutine(TransmitData(deviceInfo1.gameObject.transform.position, deviceInfo2.gameObject.transform.position, deviceInfo1));

        if (deviceInfo2.GetDeviceType() == DeviceType.EndDevice)
            StartCoroutine(TransmitData(deviceInfo2.gameObject.transform.position, deviceInfo1.gameObject.transform.position, deviceInfo2));
    }

    private IEnumerator TransmitData(Vector2 startPos, Vector2 endPos, DeviceInfo deviceInfo)
    {
        while (isConnected)
        {
            InGameNodeData inGameNodeData = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Data, startPos).GetComponent<InGameNodeData>();
            inGameNodeData.transform.localScale = new Vector3(0.1f, 0.1f);
            inGameNodeData.NodeDeviceInfo = deviceInfo;
            inGameNodeData.EndNodeID = inGameData.path[deviceInfo.GetDeviceId()].end;
            StartCoroutine(Tween.TweenRigidbody2D.MoveData(inGameNodeData.GetComponent<Rigidbody2D>(), startPos, endPos, 1f, inGameNodeData.GetBoxCollider2D()));

            yield return new WaitForSeconds(2f);
        }
    }
}