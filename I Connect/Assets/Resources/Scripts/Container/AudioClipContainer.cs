using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipContainer : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] m_Clips;

    public AudioClip this[int index]
    {
        get
        {
            return m_Clips[index];
        }
    }
}
