using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    [SerializeField]
    private float controlTime = 2f;

    [SerializeField]
    private Transform menuTrans;

    [SerializeField]
    private Transform openTrans;

    [SerializeField]
    private Transform closeTrans;

    private bool isOpen;

    private void Awake()
    {
        isOpen = false;
    }

    public void Toggle()
    {
        if(isOpen)
            StartCoroutine(Tween.TweenTransform.Position(menuTrans, closeTrans.position, controlTime));

        else
            StartCoroutine(Tween.TweenTransform.Position(menuTrans, openTrans.position, controlTime));

        isOpen = !isOpen;
    }
}