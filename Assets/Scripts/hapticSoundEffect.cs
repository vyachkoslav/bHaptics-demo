using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hapticSoundEffect : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClips;
    [SerializeField] AudioSource audioSource;
    public bool isPlaying => audioSource.isPlaying;
    public void Play()
    {
        int i = Random.Range(0, audioClips.Count);
        audioSource.PlayOneShot(audioClips[i]);
    }
}