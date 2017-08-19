using System.Collections.Generic;
using UnityEngine;

public class DisConnect : MonoBehaviour
{
    private Vector2 currentPosition;
    private List<Vector2> currentPositionList;

    private LimitMaxLine currentDevice;

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
    public void SetCurrentDevice(LimitMaxLine device)
    {
        currentDevice = device;
    }
    public void SetCurrentPositionList(List<Vector2> list)
    {
        currentPositionList = list;
        currentPosition = currentPositionList[currentPositionList.Count - 1];
    }


    public void DisConnecting()
    {
        MeshLineManager.Instance.Clear(meshLine);
        MeshLineManager.Instance.Clear(duplicationMeshLine);

        if (SceneChanger.Instance.SceneName == SceneType.InGame)
        {
            if(currentDevice.GetDeviceType() == MaxLineForType.EndDevice)
                InGameManager.Instance.DisConnect();

            currentPositionList.Remove(currentPosition);

            currentDevice.DisConnect();
        }
        ObjectPoolManager.Instance.Free(this.gameObject);
    }
}