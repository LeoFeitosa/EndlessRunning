using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip startMenuMusic;
    [SerializeField] private AudioClip mainTrackMusic;
    private AudioSource audioSource;
    private AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayStartMenuMusic()
    {
        PlayMusic(startMenuMusic);
    }

    public void PlayMainTrackMusic()
    {
        PlayMusic(mainTrackMusic);
    }

    void PlayMusic(AudioClip clip)
    {
        AudioSource.clip = clip;
        AudioSource.loop = true;
        AudioSource.Play();
    }

    public void StopMusic()
    {
        AudioSource.Stop();
    }
}
