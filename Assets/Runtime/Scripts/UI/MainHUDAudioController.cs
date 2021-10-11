using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainHUDAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip countdownSound;
    [SerializeField] private AudioClip countdownFinishedSound;
    private AudioSource audioSource;
    private AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayButtonSound()
    {
        Play(buttonSound);
    }

    public void PlayCountdownSound()
    {
        Play(countdownSound);
    }

    public void PlayCountdownFinishedSound()
    {
        Play(countdownFinishedSound);
    }

    void Play(AudioClip clip)
    {
        AudioUtility.PlayAudioCue(AudioSource, clip);
    }
}
