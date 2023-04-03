using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound Effect Player", fileName = "newSoundEffectPlayer.asset")]
public class SoundEffectPlayer : ScriptableObject
{
    public SfxAudioSource SfxAudioSourcePrefab;

    List<SfxAudioSource> spawnedAudioSources = new List<SfxAudioSource>();

    public void Play (AudioClip clip)
    {
        if (spawnedAudioSources.Any(s => s == null))
        {
            // clean up when restarting in player
            spawnedAudioSources.Clear();
        }

        SfxAudioSource source = spawnedAudioSources.FirstOrDefault(s => !s.Playing);

        if (source == null)
        {
            source = Instantiate(SfxAudioSourcePrefab);
            spawnedAudioSources.Add(source);
        }

        source.Play(clip);
    }
}
