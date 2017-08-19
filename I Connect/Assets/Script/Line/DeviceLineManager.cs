using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceLineManager : MonoBehaviour
{
    private List<Vector2> connectedPositionList = new List<Vector2>();

    private UIMeshLine line;

    private ParticleSystem connectParticle;

    public List<Vector2> ConnectedPositionList()
    {
        return connectedPositionList;
    }

    private void Awake()
    {
        connectParticle = InGameManager.Instance.GetConnectParticle();
    }

    void OnMouseDown()
    {
        line = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Line, Vector3.zero).GetComponent<UIMeshLine>();

        Vector2 objectPosition = Camera.main.WorldToViewportPoint(transform.position);

        Vector2 position = new Vector2((objectPosition.x - 0.5f) * 1600, (objectPosition.y - 0.5f) * 900);

        line.SetPointPosition(0, position);
        line.SetPointPosition(1, position);

        line.lengthRatio = 1f;
    }

    void OnMouseUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100.0f);

        if(hit.transform != null)
        {
            if (hit.collider.CompareTag("Device"))
            {
                List<Vector2> targetConnectedList = hit.transform.GetComponent<DeviceLineManager>().ConnectedPositionList();

                for(int  i = 0; i < connectedPositionList.Count; i++)
                {
                    if(connectedPositionList[i] == (Vector2)hit.transform.position)
                    {
                        MeshLineManager.Instance.Clear(line);
                        return;
                    }
                }

                for (int i = 0; i < targetConnectedList.Count; i++)
                {
                    if (targetConnectedList[i] == (Vector2)this.transform.position)
                    {
                        MeshLineManager.Instance.Clear(line);
                        return;
                    }
                }

                LimitMaxLine lineLimit = hit.transform.GetComponent<LimitMaxLine>();

                if (!lineLimit.IsCanConnect || hit.transform.position == this.transform.position)
                {
                    MeshLineManager.Instance.Clear(line);
                    return;
                }

                connectedPositionList.Add(hit.transform.position);

                Vector2 objectPosition = Camera.main.WorldToViewportPoint((Vector2)hit.transform.position);

                Vector2 position = new Vector2((objectPosition.x - 0.5f) * 1600, (objectPosition.y - 0.5f) * 900);

                line.SetPointPosition(1, position);

                lineLimit.Connect();
                MeshLineManager.Instance.Connect(line, lineLimit, connectedPositionList);

                if (lineLimit.GetDeviceType() == MaxLineForType.EndDevice)
                {
                    InGameManager.Instance.Complete();
                }

                connectParticle.transform.position = hit.transform.position;

                connectParticle.Play();
            }
        }
        else
        {
            MeshLineManager.Instance.Clear(line);
        }
    }

    void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        Vector2 position = new Vector2((mousePosition.x - 0.5f) * 1600, (mousePosition.y - 0.5f) * 900);

        line.SetPointPosition(1, position);
    }
}