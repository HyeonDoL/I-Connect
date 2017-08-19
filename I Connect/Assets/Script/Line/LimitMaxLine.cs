using UnityEngine;

public enum MaxLineForType
{
    EndDevice = 1,
    Router = 3,
    Switch = 5,
    AP = 99

}
public class LimitMaxLine : MonoBehaviour
{
    [SerializeField]
    private MaxLineForType deviceType;

    private int maxLine;

    private int lineCount;

    public bool IsCanConnect { get; set; }

    private void Awake()
    {
        lineCount = 0;
        maxLine = (int)deviceType;

        IsCanConnect = true;
    }

    public void Connect()
    {
        if (!IsCanConnect)
            return;

        lineCount += 1;

        if (lineCount >= maxLine)
            IsCanConnect = false;
    }

    public void DisConnect()
    {
        lineCount -= 1;

        if (lineCount < maxLine)
            IsCanConnect = true;
    }

    public MaxLineForType GetDeviceType()
    {
        return deviceType;
    }
}