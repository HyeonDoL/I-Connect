using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceHotspot : MonoBehaviour
{
    

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private Vector3 DefaultScale;

    [SerializeField]
    private GameObject OriginWirelessLine;

    [SerializeField]
    private float SendRate;

    [SerializeField]
    private float DataMoveTime;

    private Vector3 WirelessDataDefaultScale;
    private CircleCollider2D HotspotConnectDetector;
    private Transform myDeviceTransform;
    private Transform myTransform;

    private Dictionary<Collider2D, Transform> DetectedDevices = new Dictionary<Collider2D, Transform>();

    [SerializeField]
    private bool[] TestButtons;

    private void Awake()
    {
        WirelessDataDefaultScale = OriginWirelessLine.transform.localScale;
        myTransform = this.transform;
        myDeviceTransform = myTransform.parent;
        HotspotConnectDetector = this.GetComponent<CircleCollider2D>();
        if (!HotspotConnectDetector.isTrigger)
            HotspotConnectDetector.isTrigger = true;
        this.transform.localScale = Vector3.zero;

    }
    private void Start()
    {
        StartCoroutine(ConnectedDevices());
    }
    private void Update()
    {
        if (myTransform.localScale.x != 0)
            this.transform.Rotate(0, 0, rotateSpeed);
    }
    private void DevOnly_Input()
    {
        #region TestInput
        if (TestButtons[0])
        {
            TestButtons[0] = false;
            OnActive();
        }
        if (TestButtons[1])
        {
            TestButtons[1] = false;
            OnDeActive();

            LimitMaxLine[] test = this.GetDetectedDevices();
            Debug.Log(test.Length);

            for (int i = 0; i < test.Length; ++i)
            {
                Debug.Log(test[i].name);
            }


        }
        #endregion

    }
    private IEnumerator ConnectedDevices()
    {
        LimitMaxLine[] ConnectedDevices = this.GetDetectedDevices();
        while (this.enabled)
        {
            if (DetectedDevices.Count != 0)
            {
                ConnectedDevices = this.GetDetectedDevices();
                for (int i = 0; i < ConnectedDevices.Length; ++i)
                {
                    AttachLine(ConnectedDevices[i].transform);
                }
            }
            yield return new WaitForSeconds(SendRate);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("kljk");
        if (collision.transform == myDeviceTransform)
            return;
        if (DetectedDevices.ContainsKey(collision))
            return;
        if (!collision.transform.GetComponent<LimitMaxLine>())
            return;
        if (!collision.transform.GetComponent<EndDevice>())
            return;
        Debug.Log("kljk");
        DetectedDevices.Add(collision, collision.transform);

    }
    
    private void AttachLine(Transform target)
    {
        GameObject WirelessDataSend = ObjectPoolManager.Instance.GetObject(ObjectPoolType.WirelessLines,Vector3.zero);
        GameObject WirelessDataRecive = ObjectPoolManager.Instance.GetObject(ObjectPoolType.WirelessLines, Vector3.zero);

        WirelessDataSend.transform.localScale = Vector3.one;
        WirelessDataRecive.transform.localScale = Vector3.one;

        WirelessDataSend.transform.position = myTransform.position;
        WirelessDataRecive.transform.position = target.position;

        WirelessDataSend.transform.LookAt(target);
        WirelessDataRecive.transform.LookAt(myTransform);

        WirelessDataSend.transform.Rotate(0, -90, 0);
        WirelessDataRecive.transform.Rotate(0, -90, 0);

        StartCoroutine(MoveTo(WirelessDataSend.transform, target.position, DataMoveTime));
        StartCoroutine(MoveTo(WirelessDataRecive.transform, myTransform.position, DataMoveTime));
    }//regacy code.

    private IEnumerator MoveTo(Transform target, Vector3 end, float time)
    {
        

        Vector3 start = target.position;

        float lengh = (end - start).magnitude;
        float dtMove = lengh / time;
        float moveProgress = 0;

        while (moveProgress < lengh)
        {
            target.position += (end - start).normalized * dtMove * Time.deltaTime;
            moveProgress += dtMove*Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        ObjectPoolManager.Instance.Free(target.gameObject);
    }

    public LimitMaxLine[] GetDetectedDevices()
    {
        LimitMaxLine[] returner = new LimitMaxLine[DetectedDevices.Count];
        int index = 0;
        foreach (var kvp in DetectedDevices)
        {
            Transform inserter;
            if(!DetectedDevices.TryGetValue(kvp.Key, out inserter))
                Debug.Log("DetectedDevices.TryGetValue is False..");
            returner[index] = inserter.GetComponent<LimitMaxLine>();
            
            ++index;
        }

        return returner;
    }

    public void OnActive()
    {
        StartCoroutine(Tween.TweenTransform.LocalScale(myTransform, DefaultScale, 0.7f));
    }

    public void OnDeActive()
    {
        StartCoroutine(Tween.TweenTransform.LocalScale(myTransform, Vector3.zero, 0.7f));
        DetectedDevices.Clear();
    }

}
