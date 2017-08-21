using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public struct Testing
{
    public Sprite[] Sprites;
    public int correctAnswer;
}


public class Menu_CheckStage : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
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
    

    [SerializeField]
    private RectTransform CanvasRect;

    private float DefaultScale;

    private bool isGoTarget;

    private RectTransform myTransform;

    private UIMeshLine line;
    [Header("0: Qusetion, 1: Left, 2: right")]
    [SerializeField]
    private Image[] UsingImages;

    [SerializeField]
    private Testing[] Tests;
    
    private Vector2 spaceSupportModuler;
    private Vector2 SpaceSupportModuler
    {
        set
        {
            spaceSupportModuler = new Vector2
                (value.x * CanvasRect.rect.width - CanvasRect.rect.width * 0.5f,
                value.y * CanvasRect.rect.height - CanvasRect.rect.height * 0.5f);
        }
        get
        {
            return spaceSupportModuler;
        }
    }

    private int StageIndex;
    private void Awake()
    {
        myTransform = this.GetComponent<RectTransform>();
        DefaultScale = myTransform.localScale.x;
        for (int i = 0; i < Stages.Length; ++i)
        {
            Stages[i].Stage.position = myTransform.position;
            Stages[i].Stage.gameObject.SetActive(false);
        }

        isGoTarget = false;
        StageIndex = GameManager.Instance.StageLv - 1;

        StageIndex = 0;
        for (int i=1;i<3;++i)
        {
            UsingImages[i].sprite = Tests[StageIndex].Sprites[i];
        }
        
    }

    public void OnSelect()
    {
        StartCoroutine(GoTargetPosition());
        UsingImages[0].sprite = Tests[StageIndex].Sprites[0];
        UsingImages[0].GetComponent<RectTransform>().sizeDelta = new Vector2(Tests[StageIndex].Sprites[0].rect.width, Tests[StageIndex].Sprites[0].rect.height);
    }

    private IEnumerator GoTargetPosition()
    {
        for (int i = 0; i < Stages.Length; ++i)
        {
            Stages[i].Stage.gameObject.SetActive(true);
        }
        while (GoTimer < CurrentSetWaitTime)
        {
            for (int i = 0; i < Stages.Length; ++i)
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

        isGoTarget = true;

        yield return null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isGoTarget)
            return;
        line = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Line, Vector3.zero).GetComponent<UIMeshLine>();
        SpaceSupportModuler = Camera.main.WorldToViewportPoint(new Vector3(myTransform.position.x, myTransform.position.y, 0));
        Vector2 position = SpaceSupportModuler;
        line.SetPointPosition(0, position);
        line.SetPointPosition(1, position);

        line.lengthRatio = 1f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isGoTarget)
            return;

        SpaceSupportModuler = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 position = SpaceSupportModuler;
        line.SetPointPosition(1, position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isGoTarget)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100.0f);

        if (hit.transform != null)
        {
            if (hit.collider.CompareTag("Device"))
            {
                SpaceSupportModuler = Camera.main.WorldToViewportPoint(hit.transform.position);
                Vector2 position = SpaceSupportModuler;
                line.SetPointPosition(1, position);

                MeshLineManager.Instance.Connect(line);
                
                GameManager.Instance.SetStageClear(StageIndex, hit.transform.gameObject.name.Equals(Tests[StageIndex].correctAnswer.ToString()));

                SceneChanger.Instance.ChangeScene(SceneChanger.Instance.SceneName);
            }
        }
        else
        {
            MeshLineManager.Instance.Clear(line);
        }
    }

}
