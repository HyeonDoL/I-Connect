using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisConnect : MonoBehaviour
{
    [SerializeField]
    private MeshLineManager lineManager;

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
        lineManager.Clear(meshLine1);
        lineManager.Clear(meshLine2);

        this.gameObject.SetActive(false);
    }
}