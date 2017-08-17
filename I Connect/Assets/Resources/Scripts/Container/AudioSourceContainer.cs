using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceContainer : MonoBehaviour 
{
    [SerializeField]
    private AudioSource[] m_Sources;

    public AudioSource this[int index]
    {
        get
        {
            return m_Sources[index];
        }
    }
    public int Length { get { return m_Sources.Length; } }
}