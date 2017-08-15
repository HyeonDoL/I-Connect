using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceLineManager : MonoBehaviour
{
    [SerializeField]
    private CurveLine curveLine;

    void OnMouseDown()
    {
        curveLine.isDrag = true;

        curveLine.SetStartPoint((Vector2)transform.position);
    }

    void OnMouseUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100.0f);

        if(hit.transform != null)
        {
            if (hit.collider.CompareTag("Device"))
                curveLine.SetEndPoint((Vector2)hit.transform.position);
        }

        else
            curveLine.ClearLine();

        curveLine.isDrag = false;
    }
}