using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevOnly_Displayer : MonoBehaviour
{
    [SerializeField]
    private ScrollRect Displayer;

    [SerializeField]
    private Vector2 value;
    // Update is called once per frame
    void Update()
    {
        value = Displayer.normalizedPosition;
    }
}
