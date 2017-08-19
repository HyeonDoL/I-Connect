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

    [Serializable]
    public struct CheckInGameMenu
    {
        public bool connector;
        public bool crossCable;
        public bool cutter;
        public bool direct;
        public bool label;
        public bool router;
        public bool setting;
        public bool _switch;
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