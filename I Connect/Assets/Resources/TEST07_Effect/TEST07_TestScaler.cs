using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST07_TestScaler : MonoBehaviour
{
    [SerializeField]
    private GameObject Particle;
    private BackToMe myParticleSystem;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            ttAction();
    }
    void ttAction()
    {
        GameObject particle = GameObject.Instantiate(Particle);
        particle.transform.parent = this.transform;
        particle.transform.localPosition = new Vector3(0, 0, 0);
        particle.transform.localScale = new Vector3(1, 1, 1);

        myParticleSystem = particle.GetComponent<BackToMe>();
        myParticleSystem.myTransform = this.transform;
        myParticleSystem.PlayParticle();
    }
}
