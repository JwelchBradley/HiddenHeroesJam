using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{

    public AudioClip[] clips;

    private void Awake()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource)
        {
            audioSource.clip = clips[Random.Range(0, clips.Length)];
        }
        audioSource.Play();
    }
}
