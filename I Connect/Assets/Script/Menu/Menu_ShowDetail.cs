using System.Collections;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public struct StagePair
{
    public RectTransform Stage;
    public Vector3 TargetPosition;
}

public class Menu_ShowDetail : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
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
    private Text bottomText;

    [SerializeField]
    private ScrollRect myRect;

    private float DefaultScale;

    private bool isGoTarget;

    private RectTransform myTransform;

    private UIMeshLine line;

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
    }

    public void OnSelect()
    {
        if(!isGoTarget)
            StartCoroutine(FadeText());

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

        isGoTarget = true;

        yield return null;
    }

    private IEnumerator FadeText()
    {
        yield return StartCoroutine(_FadeText(0f, 0.5f));

        bottomText.text = "Connecting to Start";

        yield return StartCoroutine(_FadeText(1f, 0.5f));
    }
    private IEnumerator _FadeText(float alpha, float time)
    {
        float t = 0;

        Color startColor = bottomText.color;
        Color endColor = new Color(bottomText.color.r, bottomText.color.g, bottomText.color.b, alpha);

        while(t < 1f)
        {
            t += Time.deltaTime / time;

            bottomText.color = Color.Lerp(startColor, endColor, t);

            yield return null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isGoTarget)
            return;

        line = ObjectPoolManager.Instance.GetObject(ObjectPoolType.Line, Vector3.zero).GetComponent<UIMeshLine>();

        Vector2 objectPosition = myTransform.localPosition;
        Debug.Log("myRect.normalizedPosition.x : "+ myRect.normalizedPosition.x);
        Vector2 position = new Vector2(objectPosition.x - (8000 * myRect.normalizedPosition.x), (objectPosition.y )) ;

        line.SetPointPosition(0, position);
        line.SetPointPosition(1, position);

        line.lengthRatio = 1f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isGoTarget)
            return;

        Vector2 mousePosition = Camera.main.ScreenToViewportPoint( Input.mousePosition);

        Vector2 position = new Vector2((mousePosition.x )*1600 -800f , (mousePosition.y )*900 - 450f);

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
                Vector2 objectPosition = Camera.main.WorldToViewportPoint((Vector2)hit.transform.position);

                Vector2 position = new Vector2((objectPosition.x - 0.5f) * 1600, (objectPosition.y - 0.5f) * 900);

                line.SetPointPosition(1, position);

                MeshLineManager.Instance.Connect(line);

                int lv;

                int.TryParse(hit.transform.gameObject.name, out lv);

                GameManager.Instance.StageLv = lv;

                SceneChanger.Instance.ChangeScene(SceneType.InGame);
            }
        }
        else
        {
            MeshLineManager.Instance.Clear(line);
        }
    }
}