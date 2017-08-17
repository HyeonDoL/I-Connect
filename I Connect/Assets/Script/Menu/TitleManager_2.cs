using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager_2 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Names;

    [SerializeField]
    private bool tester;
    [SerializeField]
    private Transform Marker;

    private Quaternion defaultRotate;
    private Quaternion ReverseRotate;

    private void Awake()
    {
        for(int i=0;i<Names.Length;++i)
        {
            Names[i].SetActive(false);
            Names[i].transform.position = Marker.position;
        }
        defaultRotate = Quaternion.Euler(-180, 0, 0);
    }

    private void Update()
    {
        if(tester)
        {
            tester = false;
            StartCoroutine(StartTitle());
        }
    }
    public IEnumerator StartTitle()
    {
        int indexer = 0;
        Names[indexer].SetActive(true);
        while (indexer < Names.Length-1)
        {
            yield return new WaitForSeconds(0.5f);
            defaultRotate = Quaternion.Euler(Random.Range(1500, 2700), Random.Range(1500, 2700), Random.Range(1500, 2700));
            StartCoroutine(Tween.TweenTransform.Rotation(Names[indexer].transform, defaultRotate, 0.25f));
            yield return new WaitForSeconds(0.35f);
            defaultRotate = Quaternion.Euler(Random.Range(1500, 2700), Random.Range(1500, 2700), Random.Range(1500, 2700));
            StartCoroutine(Tween.TweenTransform.Rotation(Names[indexer].transform, defaultRotate, 0.25f));
            yield return new WaitForSeconds(0.24f);
            Names[indexer].SetActive(false);
            if (indexer + 1 >= Names.Length)    break;
            Names[++indexer].SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Tween.TweenTransform.Rotation(Names[8].transform, Quaternion.Euler(0,-135,0), 0.25f));
        yield return new WaitForSeconds(0.5f);

        yield return null;
    }
}
