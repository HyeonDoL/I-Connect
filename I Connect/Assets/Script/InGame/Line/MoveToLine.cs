using UnityEngine;

public class MoveToLine : MonoBehaviour
{
    [SerializeField]
    private CurveLine curveLine;

    private bool isCanMove;

    public bool IsMove { get; set; }

    private void Awake()
    {
        IsMove = false;
        isCanMove = true;
    }

    private void Update()
    {
        if (IsMove)
            Move();
    }

    private void Move()
    {
        if (!isCanMove)
            return;

        isCanMove = false;


    }
}