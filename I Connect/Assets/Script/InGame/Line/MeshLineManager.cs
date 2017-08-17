using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshLineManager : MonoBehaviour
{
    [SerializeField]
    private float clearTime = 0.5f;

    [SerializeField]
    private UIMeshLine tempLine;

    [SerializeField]
    private DisConnect tempDisConnect;

    public void Clear(UIMeshLine meshLine)
    {
        StartCoroutine(_Clear(meshLine));
    }
    
    private IEnumerator _Clear(UIMeshLine meshLine)
    {
        float t = 0f;
        
        while(t < 1f)
        {
            t += Time.deltaTime / clearTime;

            meshLine.lengthRatio = Mathf.Lerp(1, 0, t);

            yield return null;
        }
    }

    public void connect(UIMeshLine meshLine)
    {
        meshLine.lengthRatio = 1f;
        tempLine.lengthRatio = 1f;

        Vector2 startPoint = meshLine.GetPointInfo(0).point;
        Vector2 endPoint = meshLine.GetPointInfo(1).point;

        Vector2 centerPoint = (endPoint - startPoint) / 2 + startPoint;

        meshLine.SetPointPosition(0, startPoint);
        meshLine.SetPointPosition(1, centerPoint);

        tempLine.SetPointPosition(0, endPoint);
        tempLine.SetPointPosition(1, centerPoint);

        tempDisConnect.SetMeshLine1(meshLine);
        tempDisConnect.SetMeshLine2(tempLine);

        tempDisConnect.GetComponent<RectTransform>().anchoredPosition = centerPoint;

        tempDisConnect.gameObject.SetActive(true);
    }
}