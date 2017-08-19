using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ConnectDisplayer : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer[] SokectImages;
    private int MaxConnection;

    [SerializeField]
    private Color ConnectedColor;

    [SerializeField]
    private Color EmptyColor;

    private int ConnectIndex;
    private int ConnectIndexer
    {
        set { ConnectIndex = value < 0 ? 0 : value > MaxConnection ? MaxConnection : value; }
        get { return ConnectIndex; }
    }

    // Use this for initialization
    void Awake()
    {
        MaxConnection = SokectImages.Length-1;
        ConnectIndexer = 0;
    }
    
    public void AddConnection()
    {
        SokectImages[ConnectIndexer++].color = ConnectedColor;
    }
    public void DeleteConnection()
    {
        SokectImages[ConnectIndexer--].color = EmptyColor;
    }
}
