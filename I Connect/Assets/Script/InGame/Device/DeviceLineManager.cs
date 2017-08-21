using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceLineManager : MonoBehaviour
{
    [SerializeField]
    private DeviceInfo deviceInfo;

    private InGameData inGameData;

    private UIMeshLine line;

    private ParticleSystem connectParticle;

    public bool IsConnected { get; set; }

    private void Awake()
    {
        IsConnected = false;

        connectParticle = InGameManager.Instance.GetConnectParticle();

        inGameData = InGameManager.Instance.GetInGameSheet().m_data[GameManager.Instance.StageLv - 1];
    }

    void OnMouseDown()
    {
        if (!GameManager.Instance.IsCanTouch)
            return;

        if (!InGameManager.Instance.isCanConnect)
            return;

        line = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Line, Vector3.zero).GetComponent<UIMeshLine>();

        Vector2 objectPosition = Camera.main.WorldToViewportPoint(transform.position);

        Vector2 position = new Vector2((objectPosition.x - 0.5f) * 1600, (objectPosition.y - 0.5f) * 900);

        line.SetPointPosition(0, position);
        line.SetPointPosition(1, position);

        line.lengthRatio = 1f;
    }

    void OnMouseUp()
    {
        if (!GameManager.Instance.IsCanTouch)
            return;

        if (!InGameManager.Instance.isCanConnect)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100.0f);

        if(hit.transform != null)
        {
            if (hit.collider.CompareTag("Device"))
            {
                #region Exception
                DeviceInfo targetDeviceInfo = hit.transform.GetComponent<DeviceInfo>();

                List <DeviceData> targetConnectedList = targetDeviceInfo.ConnectedDeviceList();
                List<DeviceData> connectedList = deviceInfo.ConnectedDeviceList();

                for(int  i = 0; i < connectedList.Count; i++)
                {
                    if((Vector2)connectedList[i].trans.position == (Vector2)hit.transform.position)
                    {
                        MeshLineManager.Instance.Clear(line);
                        return;
                    }
                }

                for (int i = 0; i < targetConnectedList.Count; i++)
                {
                    if ((Vector2)targetConnectedList[i].trans.position == (Vector2)this.transform.position)
                    {
                        MeshLineManager.Instance.Clear(line);
                        return;
                    }
                }

                LimitMaxLine lineLimit = targetDeviceInfo.GetLimitMaxLine();
                LimitMaxLine maxLine = deviceInfo.GetLimitMaxLine();

                if (!lineLimit.IsCanConnect|| !maxLine.IsCanConnect || hit.transform.position == this.transform.position)
                {
                    MeshLineManager.Instance.Clear(line);
                    return;
                }

                if(InGameManager.Instance.SelectCableType == CableType.CrossOver &&
                    targetDeviceInfo.GetDeviceData().type != deviceInfo.GetDeviceData().type)
                {
                    MeshLineManager.Instance.Clear(line);
                    return;
                }

                if (InGameManager.Instance.SelectCableType == CableType.StraightThrough &&
                    targetDeviceInfo.GetDeviceData().type == deviceInfo.GetDeviceData().type)
                {
                    MeshLineManager.Instance.Clear(line);
                    return;
                }

                #endregion

                IsConnected = true;

                Vector2 objectPosition = Camera.main.WorldToViewportPoint((Vector2)hit.transform.position);

                Vector2 position = new Vector2((objectPosition.x - 0.5f) * 1600, (objectPosition.y - 0.5f) * 900);

                line.SetPointPosition(1, position);

                lineLimit.Connect();
                maxLine.Connect();

                MeshLineManager.Instance.Connect(line, deviceInfo, targetDeviceInfo, this);

                deviceInfo.ConnectedDeviceList().Add(targetDeviceInfo.GetDeviceData());

                targetDeviceInfo.ConnectedDeviceList().Add(deviceInfo.GetDeviceData());

                if (deviceInfo.GetDeviceType() == DeviceType.EndDevice)
                    StartCoroutine(TransmitData(this.transform.position, hit.transform.position, deviceInfo));

                if (targetDeviceInfo.GetDeviceType() == DeviceType.EndDevice)
                    StartCoroutine(TransmitData(hit.transform.position, this.transform.position, targetDeviceInfo));

                connectParticle.transform.position = hit.transform.position;

                connectParticle.Play();

                AudioManager.Instance.DoMyBestPlay(AudioManager.AudioClipIndex.connect);
            }
            else
                MeshLineManager.Instance.Clear(line);
        }
        else
        {
            MeshLineManager.Instance.Clear(line);
        }
    }

    void OnMouseDrag()
    {
        if (!GameManager.Instance.IsCanTouch)
            return;

        if (!InGameManager.Instance.isCanConnect)
            return;

        Vector2 mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        Vector2 position = new Vector2((mousePosition.x - 0.5f) * 1600, (mousePosition.y - 0.5f) * 900);

        line.SetPointPosition(1, position);
    }

    private IEnumerator TransmitData(Vector2 startPos, Vector2 endPos, DeviceInfo deviceInfo)
    {
        while (IsConnected)
        {
            InGameNodeData inGameNodeData = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Data, startPos).GetComponent<InGameNodeData>();
            inGameNodeData.transform.localScale = new Vector3(0.1f, 0.1f);
            inGameNodeData.NodeDeviceInfo = deviceInfo;
            inGameNodeData.EndNodeID = inGameData.path[deviceInfo.GetDeviceId()].end;
            StartCoroutine(Tween.TweenRigidbody2D.MoveData(inGameNodeData.GetComponent<Rigidbody2D>(), startPos, endPos, 0.8f, inGameNodeData.GetBoxCollider2D()));

            yield return new WaitForSeconds(2f);
        }
    }
}