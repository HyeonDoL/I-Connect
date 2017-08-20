using UnityEngine;
using System.Collections.Generic;

/*
private static MoveToPathManager instance = null;
public static MoveToPathManager Instance
{
    get
    {
        if (instance)
            return instance;
        else
            return instance = new GameObject("MoveToPathManager").AddComponent<MoveToPathManager>();
    }
}

public struct PathNode
{
    public int id;
    public Transform trans;
    public DeviceLineManager deviceLineManager;
}

[SerializeField]
private List<PathNode> pathNodes = new List<PathNode>();

private InGameSheet inGameSheet;

private InGameData.DataMovePath[] dataMovePath;

private const int maxNode = 10;

private int[,] adjacencyMatrix = new int[maxNode, maxNode];

public bool testDebug;

private void Awake()
{
    instance = this;

    inGameSheet = InGameManager.Instance.GetInGameSheet();

    dataMovePath = inGameSheet.m_data[GameManager.Instance.StageLv - 1].path;

    InitAdjacencyMatrix();
}

private void InitAdjacencyMatrix()
{
    for(int i = 0; i < maxNode; i++)
    {
        for(int j = 0; j < maxNode; j++)
        {
            adjacencyMatrix[i, j] = 0;
        }
    }
}

public void AddNode(int id, Transform trans, DeviceLineManager device)
{
    PathNode pathNode = new PathNode();

    pathNode.id = id;
    pathNode.trans = trans;
    pathNode.deviceLineManager = device;

    pathNodes.Add(pathNode);

    adjacencyMatrix[id, id] = -1;
}

public void ConnectNode(int nodeId, int targetId)
{
    adjacencyMatrix[nodeId, targetId] += 1;
}
*/

public class MoveToPathManager : MonoBehaviour
{

}