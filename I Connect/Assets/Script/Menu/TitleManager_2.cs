using System.Collections;
using UnityEngine;
using System;


public class TitleManager_2 : MonoBehaviour
{
    [Serializable]
    public struct Word
    {
        public Material[] materials;
        public Transform[] rotationTrans;
    }

    [SerializeField]
    private GameObject[] Names;

    [SerializeField]
    private Word[] words;

    private const int MaxCharacter = 9;
 
    private void Awake()
    {
        StartCoroutine(StartTitle());

        AudioManager.Instance.DoMyBestPlay(AudioManager.AudioClipIndex.Figuring_it_All_Out);
    }

    public IEnumerator StartTitle()
    {
        yield return new WaitForSeconds(0.5f);

        for(int  i = 0; i < words.Length; i++)
        {
            for(int  j = 0; j < MaxCharacter - 1; j++)
            {
                yield return new WaitForSeconds(0.25f);
                StartCoroutine(Rotater(Names[j].transform, words[i].materials[j], 0.8f));
            }

            yield return StartCoroutine(Rotater(Names[MaxCharacter - 1].transform, words[i].materials[MaxCharacter - 1], 0.8f));

            for(int j = 0; j < MaxCharacter - 1; j++)
            {
                yield return new WaitForSeconds(0.25f);
                StartCoroutine(Tween.TweenTransform.Rotation(Names[j].transform, words[i].rotationTrans[j].rotation, 0.5f));
            }

            yield return StartCoroutine(Tween.TweenTransform.Rotation(Names[MaxCharacter - 1].transform, words[i].rotationTrans[MaxCharacter - 1].rotation, 0.5f));
        }

        yield return new WaitForSeconds(1.5f);

        SceneChanger.Instance.ChangeScene(SceneType.Menu);
    }
    private IEnumerator Rotater(Transform target, Material material, float EndTime)
    {
        float timer=0f;
        Vector3 eularRandom = new Vector3(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5)) * 3f;
        while (timer< EndTime)
        {
            if (timer > EndTime / 2)
                target.GetComponent<MeshRenderer>().material = material;

            target.Rotate(eularRandom);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
