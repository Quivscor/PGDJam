using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;

    [Header("Audio clips")]
    [SerializeField] private AudioClip basicAmbient;
    [SerializeField] private AudioClip fightSounds;
    [SerializeField] private AudioClip eventSound;
    [SerializeField] private AudioClip forestAmbient;
    [SerializeField] private AudioClip howlingAmbient;

    [Header("Audio sources")]
    [SerializeField] private AudioSource basicSource;
    [SerializeField] private AudioSource forestSource;
    [SerializeField] private AudioSource howlingSource;
    [SerializeField] private AudioSource fightSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        basicSource.clip = basicAmbient;
        basicSource.Play();

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

    public void ToggleFightMusic(bool toggle)
    {
        if (toggle)
        {
            fightSource.clip = fightSounds;
            fightSource.Play();
            basicSource.Stop();
        }
        else
        {
            basicSource.Play();
            fightSource.Stop();
        }
            
    }
}
