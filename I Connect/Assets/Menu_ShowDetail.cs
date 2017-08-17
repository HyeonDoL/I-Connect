using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct StagePair
{
    public RectTransform Stage;
    public Vector3 TargetPosition;
}

public class Menu_ShowDetail : MonoBehaviour
{
    [SerializeField]
    private StagePair[] Stages;

    [SerializeField]
    private float CurrentSetWaitTime;
    private float GoTimer;

    [SerializeField]
    private float accuracy;

    [SerializeField]
    private float TargetScale;
    private float DefaultScale;
    private RectTransform myTransform;
    private void Awake()
    {
        myTransform = this.GetComponent<RectTransform>();
        DefaultScale = myTransform.localScale.x;
        for (int i = 0; i < Stages.Length; ++i)
        {
            Stages[i].Stage.position = myTransform.position;
            Stages[i].Stage.gameObject.SetActive(false);
        }
    }
    public void OnSelect()
    {
        StartCoroutine(GoTargetPosition());
    }
    private IEnumerator ScaleUp()
    {
        yield return null;
    }
    private IEnumerator GoTargetPosition()
    {
        for (int i = 0; i < Stages.Length; ++i)
        {
            Stages[i].Stage.gameObject.SetActive(true);
        }
        while (GoTimer<CurrentSetWaitTime)
        {
            for(int i=0;i<Stages.Length;++i)
            {
                Stages[i].Stage.localPosition += (Stages[i].TargetPosition - Stages[i].Stage.localPosition) * 0.1f;
            }
            myTransform.localScale *= TargetScale;
            GoTimer += accuracy;
            yield return new WaitForSeconds(accuracy);
        }
        for (int i = 0; i < Stages.Length; ++i)
        {
            Stages[i].Stage.localPosition = Stages[i].TargetPosition;
        }

        yield return null;
    }
}