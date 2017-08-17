using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CurveLine : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Vector2 startPoint;

    public bool isDrag { get; set; }

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        isDrag = false;
    }

    private void Update()
    {
        if (isDrag)
            OnDrag();
    }

    private void OnDrag()
    {
        Vector2 nowPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float centerPointX = (nowPoint.x - startPoint.x) / 2 + startPoint.x;

        lineRenderer.SetPosition(1, new Vector2(centerPointX, startPoint.y));
        lineRenderer.SetPosition(2, new Vector2(centerPointX, nowPoint.y));
        lineRenderer.SetPosition(3, nowPoint);
    }

    public void SetStartPoint(Vector2 startPoint)
    {
        lineRenderer.SetPosition(0, startPoint);

        this.startPoint = startPoint;
    }
    public Vector2 GetStartPoint()
    {
        return lineRenderer.GetPosition(0);
    }

    public void SetEndPoint(Vector2 endPoint)
    {
        float centerPointX = (endPoint.x - startPoint.x) / 2 + startPoint.x;

        lineRenderer.SetPosition(1, new Vector2(centerPointX, startPoint.y));
        lineRenderer.SetPosition(2, new Vector2(centerPointX, endPoint.y));
        lineRenderer.SetPosition(3, endPoint);
    }
    public Vector2 GetEndPoint()
    {
        return lineRenderer.GetPosition(3);
    }

    public Vector2 GetPoint(int index)
    {
        return lineRenderer.GetPosition(index);
    }

    public void ClearLine()
    {
        for(int i = 0; i < 4; i++)
            lineRenderer.SetPosition(i, Vector2.zero);
    }
}