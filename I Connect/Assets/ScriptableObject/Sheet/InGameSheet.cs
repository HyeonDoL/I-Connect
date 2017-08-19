using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class InGameData
{
    public int maxComplete;

    public CheckInGameMenu checkMenu;

    public DataMovePath[] path;

    [Serializable]
    public struct CheckInGameMenu
    {
        public bool connection;
        public bool straightThroughConnection;
        public bool crossOverConnection;
        public bool disconnection;
        public bool _switch;
        public bool router;
        public bool accessPoint;
        public bool hotspot;
        public bool ipTag;
        public bool accessListTag;
        public bool vlanTag;
        public bool powerSwitch;
    }

    [Serializable]
    public struct DataMovePath
    {
        public string start;
        public string end;
    }
}

public class InGameSheet : Sheet<InGameData>
{
#if UNITY_EDITOR
    [MenuItem("DataSheet/InGameSheet")]
    public static void CreateData()
    {
        ScriptableObjectUtillity.CreateAsset<InGameSheet>();
    }
#endif
}