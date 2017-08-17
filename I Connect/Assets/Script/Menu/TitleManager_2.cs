using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager_2 : MonoBehaviour
{
    [SerializeField]
    private GameObject Model;
    [SerializeField]
    private GameObject[] Names;
    [SerializeField]
    private Material[] Names_Materials;
    
    [SerializeField]
    private Transform Marker;

    private const int MaxCharacter = 9;

    private Quaternion[] defaultRotate = new Quaternion[MaxCharacter];
    private Quaternion RandomRotate;

    private Vector3[] defaultPosition = new Vector3[MaxCharacter];
    
    private void Awake()
    {
        for(int i=0;i<Names.Length;++i)
        {
            Names[i].SetActive(false);
            //Names[i].transform.position = Marker.position;
            defaultRotate[i] = Names[i].transform.rotation;

            defaultPosition[i] = Names[i].transform.position;
        }

        Model.transform.rotation = defaultRotate[0];

        StartCoroutine(StartTitle());
    }

    public IEnumerator StartTitle()
    {
        yield return new WaitForSeconds(0.5f);

        int indexer = 0;
        while (indexer < Names.Length)
        {
            yield return new WaitForSeconds(0.25f);
            yield return StartCoroutine(Rotater(Model.transform, 0.8f, ++indexer));
            yield return StartCoroutine(Tween.TweenTransform.Rotation(Model.transform, defaultRotate[indexer], 0.5f));

            if (indexer ==8)    break;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Tween.TweenTransform.Rotation(Model.transform, Quaternion.Euler(0,-135,0), 0.25f));//t
        yield return new WaitForSeconds(0.5f);

        yield return null;

        StartCoroutine(SetObjectActives());
    }
    private IEnumerator Rotater(Transform target, float EndTime, int indexer)
    {
        float timer=0f;
        Vector3 eularRandom = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)) * 3f;
        while (timer< EndTime)
        {
            if(timer > EndTime / 2)
                Model.GetComponent<MeshRenderer>().material = Names_Materials[indexer];


            target.Rotate(eularRandom);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private IEnumerator SetObjectActives()
    {
        yield return StartCoroutine(Tween.TweenTransform.LocalScale(Model.transform, Vector3.one, 0.5f));

        for(int i = 0; i < MaxCharacter; i++)
        {
            Names[i].transform.position = Model.transform.position;

            Names[i].SetActive(true);
        }

        Model.SetActive(false);

        for (int i = 0; i < MaxCharacter; i++)
        {
            StartCoroutine(Tween.TweenTransform.Position(Names[i].transform, defaultPosition[i], 0.5f));
        }

        yield return new WaitForSeconds(1.0f);

        SceneChanger.Instance.ChangeScene(SceneType.Menu);
    }
}
