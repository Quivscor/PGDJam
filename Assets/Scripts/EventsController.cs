using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsController : MonoBehaviour
{
    public static EventsController Instance;

    private bool isEventInSanctuarium;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        isEventInSanctuarium = false;
    }
    
    public bool IsEventInSanctuarium()
    {
        return isEventInSanctuarium;
    }

    public void SetEventInSanctuarium(bool toggle)
    {
        isEventInSanctuarium = toggle;
    }

}
