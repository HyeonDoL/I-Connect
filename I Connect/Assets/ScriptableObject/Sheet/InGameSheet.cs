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
        public MenuElementPair connection;
        public MenuElementPair straightThroughConnection;
        public MenuElementPair crossOverConnection;
        public MenuElementPair disconnection;
        public MenuElementPair _switch;
        public MenuElementPair router;
        public MenuElementPair accessPoint;
        public MenuElementPair hotspot;
        public MenuElementPair ipTag;
        public MenuElementPair accessListTag;
        public MenuElementPair vlanTag;
        public MenuElementPair powerSwitch;
    }

    [Serializable]
    public struct MenuElementPair
    {
        public bool isUse;
        public int useCount;
    }

    [Serializable]
    public struct DataMovePath
    {
        public int start;
        public int end;
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