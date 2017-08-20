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

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log(GameManager.Instance.GetStageClear(0));
            GameManager.Instance.SetStageClear(0, true);
            Debug.Log(GameManager.Instance.GetStageClear(0));
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(GameManager.Instance.GetStageClear(1));
            GameManager.Instance.SetStageClear(1, true);
            Debug.Log(GameManager.Instance.GetStageClear(1));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log(GameManager.Instance.GetStageClear(2));
            GameManager.Instance.SetStageClear(2, true);
            Debug.Log(GameManager.Instance.GetStageClear(2));
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            GameManager.Instance.SetStageClear(0, false);
            GameManager.Instance.SetStageClear(1, false);
            GameManager.Instance.SetStageClear(2, false);

        }
    }
}
