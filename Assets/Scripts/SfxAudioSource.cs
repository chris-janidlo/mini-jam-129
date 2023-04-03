using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class SfxAudioSource : MonoBehaviour
{
    public bool Playing => audioSource.isPlaying;

    [SerializeField] private AudioSource audioSource;

    void Start ()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Play (AudioClip clip, SfxOptions options)
    {
        if (options.RandomizePitch)
        {
            audioSource.pitch = RandomExtra.Range(options.RandomPitchRange);
        }
        else
        {
            audioSource.pitch = 1;
        }

        audioSource.PlayOneShot(clip, options.Volume);
    }
}
