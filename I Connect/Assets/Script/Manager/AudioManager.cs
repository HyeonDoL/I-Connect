using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum AudioClipIndex : int
    {
        connect = 0,
        Disconnect = 1,
        MenuClick = 2,
        Bedtime_Story_Adventures = 10,
        Quiet_Night_at_the_Shack = 11,
        Figuring_it_All_Out = 12,
        Medieval_Courtyard = 13
    }

    private static AudioManager instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (instance)
                return instance;
            else
            {
                instance = new GameObject("AudioManager").AddComponent<AudioManager>();
                instance.transform.parent = Camera.main.transform;
                return instance;
            }
        }
    }

    #region readonly

    private AudioClipContainer audioClipContainer = null;
    public AudioClipContainer audioClipContainer_readonly
    {
        get { return audioClipContainer; }
        set { if (!audioClipContainer) audioClipContainer = value; }
    }

    private AudioSourceContainer audioSourceContainer = null;
    public AudioSourceContainer audioSourceContainer_readonly
    {
        get { return audioSourceContainer; }
        set { if (!audioSourceContainer) audioSourceContainer = value; }
    }

    #endregion

    public void DoMyBestPlay(AudioClipIndex index)
    {
        this.DoMyBestPlay((int)index);
    }

    public void DoMyBestPlay(int index)
    {
        int SourceContainerLen = audioSourceContainer.Length;
        int NotPlayingSourceIndex = SourceContainerLen - 1;

        for (int i = 0; i < SourceContainerLen; ++i)
        {
            if (!audioSourceContainer[i].isPlaying)
            {
                NotPlayingSourceIndex = i;
                break;
            }
        }

        audioSourceContainer[NotPlayingSourceIndex].clip = audioClipContainer[index];

        audioSourceContainer[NotPlayingSourceIndex].Play();

    }
}