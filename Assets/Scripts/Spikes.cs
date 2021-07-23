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
            PlayerMovement mv = collision.transform.GetComponent<PlayerMovement>();
            mv.ForceMove((-mv.velocity.normalized + Vector2.up).normalized * knockbackForce);
            if (collision.transform.TryGetComponent<PlayerCombat>(out pc))
            {
                pc.SendTakeDamageEvent(dmg);
            }
        }
    }
}
