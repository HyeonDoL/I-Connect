using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMe : MonoBehaviour
{
    ParticleSystem.Particle[] cloud = new ParticleSystem.Particle[1000];
    ParticleSystem _particleSystem;

    [Header("CallTime :default -> 0")]
    [SerializeField]
    private float CallTime;

    [SerializeField]
    private float CallMultipler;
    [Header("BackSpeed :  default -> Call multipler / (1-callMultipler)")]
    [SerializeField]
    private float BackSpeed;

    private bool Playing
    {
        set
        {
            if (value)
            {
                _particleSystem.Play();
                StartCoroutine(EffectFunc(CallTime));
            }
            else
            {
                _particleSystem.Stop();
                _particleSystem.Clear();
            }
        }
        get
        {
            return _particleSystem.isPlaying;
        }
    }
    public Transform myTransform;

    private void Awake()
    {
        _particleSystem = this.GetComponent<ParticleSystem>();

        if (CallTime <= 0f)
        {
            CallTime = CallMultipler * _particleSystem.main.startLifetime.curveMultiplier;
        }
    }
    private IEnumerator EffectFunc(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        int numParticlesAlive = _particleSystem.GetParticles(cloud);

        for (int i = 0; i < numParticlesAlive; ++i)
        {
            cloud[i].velocity *= -1f * BackSpeed;
            _particleSystem.SetParticles(cloud, numParticlesAlive);
        }
        _particleSystem.SetParticles(cloud, numParticlesAlive);
        numParticlesAlive = _particleSystem.GetParticles(cloud);

        yield return null;
    }
    public void PlayParticle()
    {
        Playing = true;
    }
    public void StopParticle()
    {
        Playing = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Playing = false;
            Playing = true;
        }
    }
}
