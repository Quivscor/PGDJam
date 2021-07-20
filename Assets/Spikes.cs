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
            collision.transform.GetComponent<PlayerMovement>().ForceMove((collision.transform.position - this.transform.position).normalized * knockbackForce);
        }
    }
}
