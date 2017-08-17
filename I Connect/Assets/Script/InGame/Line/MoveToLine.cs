using UnityEngine;
using System.Collections;

public class MoveToLine : MonoBehaviour
{
    public enum MoveType
    {
        None,
        Right,
        Left,
        Up,
        Down
    }

    [SerializeField]
    private float speed;

    [SerializeField]
    private bool loop;

    [SerializeField]
    private CurveLine curveLine;

    private const int pointCount = 4;

    private int pointIndex;

    private Vector2[] points = new Vector2[pointCount];

    private MoveType[] moveTypes = new MoveType[pointCount];

    private MoveType GetMoveType(Vector2 start, Vector2 end)
    {
        if (start.x > end.x)
            return MoveType.Left;

        if (start.x < end.x)
            return MoveType.Right;

        if (start.y > end.y)
            return MoveType.Down;

        if (start.y < end.y)
            return MoveType.Up;

        return MoveType.None;
    }

    public void Move()
    {
        for (int i = 0; i < pointCount; i++)
            points[i] = curveLine.GetPoint(i);

        for (int i = 1; i < pointCount; i++)
            moveTypes[i] = GetMoveType(points[i - 1], points[i]);

        transform.position = points[0];

        pointIndex = 0;

        NextMove();
    }

    private void NextMove()
    {
        pointIndex++;

        if (pointIndex >= pointCount)
        {
            if (loop)
                Move();

            return;
        }

        Vector2 direction = (points[pointIndex] - (Vector2)transform.position).normalized;

        StartCoroutine(Move(direction, points[pointIndex], moveTypes[pointIndex]));
    }

    private IEnumerator Move(Vector2 direction, Vector2 end, MoveType moveType)
    {
        switch(moveType)
        {
            case MoveType.Right:
                while (transform.position.x <= end.x)
                {
                    transform.position = (Vector2)transform.position + speed * Time.deltaTime * direction;

                    yield return null;
                }
                break;

            case MoveType.Left:
                while (transform.position.x >= end.x)
                {
                    transform.position = (Vector2)transform.position + speed * Time.deltaTime * direction;

                    yield return null;
                }
                break;

            case MoveType.Up:
                while (transform.position.y <= end.y)
                {
                    transform.position = (Vector2)transform.position + speed * Time.deltaTime * direction;

                    yield return null;
                }
                break;

            case MoveType.Down:
                while (transform.position.y >= end.y)
                {
                    transform.position = (Vector2)transform.position + speed * Time.deltaTime * direction;

                    yield return null;
                }
                break;
        }

        NextMove();
    }
}