using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public static float dmg = 1;
    public float knockbackForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            PlayerCombat pc;
            collision.transform.TryGetComponent(out pc);
            if (pc.IsInvulnerable())
                return;
            PlayerMovement mv = collision.transform.GetComponent<PlayerMovement>();
            mv.ForceMove((Vector2.up).normalized * knockbackForce, .5f);
            if (pc)
            {
                pc.SendTakeDamageEvent(dmg);
            }
        }
    }
}
