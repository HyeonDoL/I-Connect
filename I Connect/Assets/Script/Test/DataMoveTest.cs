﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMoveTest : MonoBehaviour
{
    [SerializeField]
    private MoveToLine moveToLine;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            moveToLine.Move();
        }
    }
}