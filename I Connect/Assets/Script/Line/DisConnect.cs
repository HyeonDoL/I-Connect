using System.Collections.Generic;
using UnityEngine;

public class DisConnect : MonoBehaviour
{
    [SerializeField]
    private Animator disConnectAni;

    private DeviceInfo thisDeviceInfo;
    private DeviceInfo targetDeviceInfo;

    private UIMeshLine meshLine;
    private UIMeshLine duplicationMeshLine;

    public void SetMeshLine(UIMeshLine meshLine)
    {
        this.meshLine = meshLine;
    }
    public void SetDuplicationMeshLine(UIMeshLine meshLine)
    {
        this.duplicationMeshLine = meshLine;
    }

    public void SetThisDeviceInfo(DeviceInfo device)
    {
        thisDeviceInfo = device;
    }
    public void SetTargetDeviceInfo(DeviceInfo device)
    {
        targetDeviceInfo = device;
    }

    public void PlayDisConnect()
    {
        disConnectAni.SetTrigger("DIsConnect");
    }

    public void DisConnecting()
    {
        MeshLineManager.Instance.Clear(meshLine);
        MeshLineManager.Instance.Clear(duplicationMeshLine);

        if (SceneChanger.Instance.SceneName == SceneType.InGame)
        {
            if (targetDeviceInfo.GetDeviceType() == DeviceType.EndDevice)
                InGameManager.Instance.DisConnect();

            targetDeviceInfo.ConnectedDeviceList().Remove(thisDeviceInfo.GetDeviceData());
            thisDeviceInfo.ConnectedDeviceList().Remove(targetDeviceInfo.GetDeviceData());

            targetDeviceInfo.GetLimitMaxLine().DisConnect();
            thisDeviceInfo.GetLimitMaxLine().DisConnect();
        }
    }

    public void Free()
    {
        ObjectPoolManager.Instance.Free(this.gameObject);
    }
}