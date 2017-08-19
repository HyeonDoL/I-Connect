using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDevice : MonoBehaviour
{
    public void Connect()
    {
        InGameManager.Instance.Complete();
    }
}