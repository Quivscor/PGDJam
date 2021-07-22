using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    [Header("Audio clips")]
    [SerializeField] private AudioClip[] ambientSounds;
    [SerializeField] private AudioClip[] fightSounds;
    [SerializeField] private AudioClip eventSound;
    [SerializeField] private AudioClip forestAmbient;
    [SerializeField] private AudioClip howlingAmbient;

    [Header("Audio sources")]
    [SerializeField] private AudioSource forestSource;
    [SerializeField] private AudioSource howlingSource;

    private void Start()
    {
        forestSource.clip = forestAmbient;
        forestSource.Play();

        howlingSource.clip = howlingAmbient;
        howlingSource.Play();
    }

    private void Update()
    {
        //if (!basicSource.isPlaying)
        //{
        //    audioSource.clip = clips[currentMusic];
        //    audioSource.Play();
        //}
    }
    public void PlayAmbientMusic()
    {

    }
}
