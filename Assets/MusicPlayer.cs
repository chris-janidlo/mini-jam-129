using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip LoopClip;
    public AudioSource AudioSource;

    void Update()
    {
        if (!AudioSource.isPlaying)
        {
            AudioSource.loop = true;
            AudioSource.clip = LoopClip;
            AudioSource.Play();
        }
    }
}
