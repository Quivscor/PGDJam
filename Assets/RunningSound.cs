using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip [] runningSounds;

    public void PlayRandomStepSound()
    {
        //audioSource.clip = runningSounds[Random.Range(0, runningSounds.Length)];
        audioSource.PlayOneShot(runningSounds[Random.Range(0, runningSounds.Length)]);
    }
}
