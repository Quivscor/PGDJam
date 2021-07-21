using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDetector : MonoBehaviour
{
    private StoryEvent storyEvent;

    private void Start()
    {
        storyEvent = GetComponentInParent<StoryEvent>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!storyEvent.wasPlayed && collision.GetComponentInParent<PlayerMovement>())
        {
            storyEvent.StartEvent();
        }
    }
}
