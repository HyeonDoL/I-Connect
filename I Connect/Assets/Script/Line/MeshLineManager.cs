using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshLineManager : MonoBehaviour
{
    private static MeshLineManager instance = null;
    public static MeshLineManager Instance
    {
        get
        {
            if (instance)
                return instance;
            else
                return instance = new GameObject("MeshLineManager").AddComponent<MeshLineManager>();
        }
    }

    [SerializeField]
    private float clearTime = 0.5f;

    public void Clear(UIMeshLine meshLine)
    {
        StartCoroutine(_Clear(meshLine));
    }
    
    private IEnumerator _Clear(UIMeshLine meshLine)
    {
        float t = 0f;
        
        while(t < 1f)
        {
            t += Time.deltaTime / clearTime;

            meshLine.lengthRatio = Mathf.Lerp(1, 0, t);

            yield return null;
        }

        ObjectPoolManager.Instance.Free(meshLine.gameObject);
    }

    public void Connect(UIMeshLine meshLine1)
    {
        UIMeshLine meshLine2 = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Line, Vector3.zero).GetComponent<UIMeshLine>();
        DisConnect disConnect = ObjectPoolManager.Instance.GetObject(ObjectPoolType.DisConnectButton, Vector3.zero).GetComponent<DisConnect>();
 
        meshLine1.lengthRatio = 1f;
        meshLine2.lengthRatio = 1f;

        Vector2 startPoint = meshLine1.GetPointInfo(0).point;
        Vector2 endPoint = meshLine1.GetPointInfo(1).point;

        Vector2 centerPoint = (endPoint - startPoint) / 2 + startPoint;

        meshLine1.SetPointPosition(0, startPoint);
        meshLine1.SetPointPosition(1, centerPoint);

        meshLine2.SetPointPosition(0, endPoint);
        meshLine2.SetPointPosition(1, centerPoint);

        disConnect.SetMeshLine(meshLine1);
        disConnect.SetDuplicationMeshLine(meshLine2);

        disConnect.GetComponent<RectTransform>().anchoredPosition = centerPoint;
    }

    public void Connect(UIMeshLine meshLine1, DeviceInfo thisDeviceInfo, DeviceInfo targetDeviceInfo)
    {
        UIMeshLine meshLine2 = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Line, Vector3.zero).GetComponent<UIMeshLine>();
        DisConnect disConnect = ObjectPoolManager.Instance.GetObject(ObjectPoolType.DisConnectButton, Vector3.zero).GetComponent<DisConnect>();

        meshLine1.lengthRatio = 1f;
        meshLine2.lengthRatio = 1f;

        Vector2 startPoint = meshLine1.GetPointInfo(0).point;
        Vector2 endPoint = meshLine1.GetPointInfo(1).point;

        Vector2 centerPoint = (endPoint - startPoint) / 2 + startPoint;

        meshLine1.SetPointPosition(0, startPoint);
        meshLine1.SetPointPosition(1, centerPoint);

        meshLine2.SetPointPosition(0, endPoint);
        meshLine2.SetPointPosition(1, centerPoint);

        disConnect.SetMeshLine(meshLine1);
        disConnect.SetDuplicationMeshLine(meshLine2);

        disConnect.SetTargetDeviceInfo(targetDeviceInfo);
        disConnect.SetThisDeviceInfo(thisDeviceInfo);

        disConnect.GetComponent<RectTransform>().localScale = new Vector2(80, 80);
        disConnect.GetComponent<RectTransform>().anchoredPosition = centerPoint;
    }
}