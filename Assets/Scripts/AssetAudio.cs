using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetAudio : MonoBehaviour
{
    public Sound[] sounds;
    public AudioSource aSource;

    private string audioPlayed;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }
    public void PlaySound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                audioPlayed = s.name;
                aSource.clip = s.clip;
                aSource.loop = s.loop;
                aSource.volume = s.volume;
                aSource.outputAudioMixerGroup = s.mixerGroup;
                aSource.Play();
            }
        }
    }

    public void StopSound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                aSource.Stop();
            }
        }
    }

    public void PauseSound()
    {
        aSource.Pause();
    }

    public string GetNameSound()
    {
        return audioPlayed;
    }
}
