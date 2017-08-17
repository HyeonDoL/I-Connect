using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisConnect : MonoBehaviour
{
    private UIMeshLine meshLine1;
    private UIMeshLine meshLine2;

    public void SetMeshLine1(UIMeshLine meshLine1)
    {
        this.meshLine1 = meshLine1;
    }
    public void SetMeshLine2(UIMeshLine meshLine2)
    {
        this.meshLine2 = meshLine2;
    }

    public void DisConnecting()
    {
        MeshLineManager.Instance.Clear(meshLine1);
        MeshLineManager.Instance.Clear(meshLine2);

        ObjectPoolManager.Instance.Free(this.gameObject);
    }
}