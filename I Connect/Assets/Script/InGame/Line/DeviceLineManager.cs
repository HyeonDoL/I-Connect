using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceLineManager : MonoBehaviour
{
    private UIMeshLine line;

    void OnMouseDown()
    {
        line = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Line, Vector3.zero).GetComponent<UIMeshLine>();

        Vector2 objectPosition = Camera.main.WorldToViewportPoint(transform.position);

        Vector2 position = new Vector2((objectPosition.x - 0.5f) * Screen.width, (objectPosition.y - 0.5f) * Screen.height);

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
                Vector2 objectPosition = Camera.main.WorldToViewportPoint((Vector2)hit.transform.position);

                Vector2 position = new Vector2((objectPosition.x - 0.5f) * Screen.width, (objectPosition.y - 0.5f) * Screen.height);

                line.SetPointPosition(1, position);

                MeshLineManager.Instance.Connect(line);
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

        Vector2 position = new Vector2((mousePosition.x - 0.5f) * Screen.width, (mousePosition.y - 0.5f) * Screen.height);

        line.SetPointPosition(1, position);
    }
}