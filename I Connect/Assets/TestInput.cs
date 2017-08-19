using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            this.GetComponent<ConnectDisplayer>().AddConnection();

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
            this.GetComponent<ConnectDisplayer>().DeleteConnection();
    }
}
