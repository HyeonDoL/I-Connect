using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceLineManager : MonoBehaviour
{
    [SerializeField]
    private DeviceInfo deviceInfo;

    private UIMeshLine line;

    private ParticleSystem connectParticle;

    private void Awake()
    {
        connectParticle = InGameManager.Instance.GetConnectParticle();
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

                Vector2 objectPosition = Camera.main.WorldToViewportPoint((Vector2)hit.transform.position);

                Vector2 position = new Vector2((objectPosition.x - 0.5f) * 1600, (objectPosition.y - 0.5f) * 900);

                line.SetPointPosition(1, position);

                lineLimit.Connect();
                maxLine.Connect();

                MeshLineManager.Instance.Connect(line, deviceInfo, targetDeviceInfo);

                InGameNodeData inGameNodeData = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Data, this.transform.position).GetComponent<InGameNodeData>();
                InGameNodeData inGameTargetNodeData = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Data, this.transform.position).GetComponent<InGameNodeData>();

                inGameNodeData.transform.localScale = new Vector3(0.1f, 0.1f);
                inGameNodeData.NodeDeviceInfo = deviceInfo;
                inGameNodeData.EndNodeID = targetDeviceInfo.GetDeviceId();

                inGameTargetNodeData.transform.localScale = new Vector3(0.1f, 0.1f);
                inGameTargetNodeData.NodeDeviceInfo = targetDeviceInfo;
                inGameTargetNodeData.EndNodeID = deviceInfo.GetDeviceId();

                StartCoroutine(Tween.TweenRigidbody2D.MoveData(inGameNodeData.GetComponent<Rigidbody2D>(), this.transform.position, hit.transform.position, 1f, inGameNodeData.GetBoxCollider2D()));
                StartCoroutine(Tween.TweenRigidbody2D.MoveData(inGameTargetNodeData.GetComponent<Rigidbody2D>(), hit.transform.position, this.transform.position, 1f, inGameTargetNodeData.GetBoxCollider2D()));

                connectParticle.transform.position = hit.transform.position;

                connectParticle.Play();

                //AudioManager.Instance.DoMyBestPlay(AudioManager.AudioClipIndex.connect);
            }
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
}