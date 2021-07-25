using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    public MapEvent mapEvent;
    private bool triggered = false;
    public Sprite pressed;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile p;
        if(collision.TryGetComponent(out p))
        {
            if(p.userTag == "Player" && !triggered)
            {
                mapEvent.StartEvent();
                GetComponent<SpriteRenderer>().sprite = pressed;
                triggered = true;
            }
        }
    }
}
