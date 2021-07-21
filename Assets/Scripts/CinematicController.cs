using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicController : MonoBehaviour
{
    public static CinematicController Instance;

    public Animator cinematicAnimator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void StartCinematic()
    {
        cinematicAnimator.SetTrigger("Start");
    }

    public void EndCinematic()
    {
        cinematicAnimator.SetTrigger("End");
    }

}
