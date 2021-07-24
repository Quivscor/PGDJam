using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFadeController : MonoBehaviour
{
    public static ScreenFadeController Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

}
