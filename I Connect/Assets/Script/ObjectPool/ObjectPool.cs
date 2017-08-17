using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> unusedList = new List<GameObject>();

    public List<GameObject> UnusedList { get { return this.unusedList; } }

    public int MaxCount { get; set; }

    public GameObject Prefab { get; set; }

    public GameObject Group { get; set; }
}