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

    [SerializeField]
    private RectTransform CanvasRect;

    private float DefaultScale;

    private bool isGoTarget;

    private RectTransform myTransform;

    private UIMeshLine line;

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


    private void Awake()
    {
        AudioManager.Instance.DoMyBestPlay(AudioManager.AudioClipIndex.Medieval_Courtyard);

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
        AudioManager.Instance.DoMyBestPlay(AudioManager.AudioClipIndex.MenuClick);

        if (!isGoTarget)
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