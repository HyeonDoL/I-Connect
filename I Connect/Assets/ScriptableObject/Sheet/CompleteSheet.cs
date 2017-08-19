using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class CompleteData
{
    public int maxCompleteCount;
}

public class CompleteSheet : Sheet<CompleteData>
{
#if UNITY_EDITOR
    [MenuItem("DataSheet/CompleteSheet")]
    public static void CreateData()
    {
        ScriptableObjectUtillity.CreateAsset<CompleteSheet>();
    }
#endif
}