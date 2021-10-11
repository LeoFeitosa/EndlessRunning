using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioUtility
{
    //play sfx
    public static void PlayAudioCue(AudioSource source, AudioClip clip)
    {
        if (source.outputAudioMixerGroup == null)
        {
            Debug.LogError("Error: Todo AudioSource deve ter um AudioMixerGroup setado");
        }
        else
        {
            source.clip = clip;
            source.loop = false;
            source.Play();
        }
    }

    public static void PlayMusic(AudioSource source, AudioClip clip)
    {
        if (source.outputAudioMixerGroup == null)
        {
            Debug.LogError("Error: Todo AudioSource deve ter um AudioMixerGroup setado");
        }
        else
        {
            source.clip = clip;
            source.loop = true;
            source.Play();
        }
    }
}
