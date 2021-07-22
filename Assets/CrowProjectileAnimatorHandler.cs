using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowProjectileAnimatorHandler : MonoBehaviour
{
    private bool hasFired;
    private SpriteRenderer renderer;
    private Rigidbody2D rb2d;

    private void Start()
    {
        GetComponent<Projectile>().FiredProjectile += FiredProjectile;
        renderer = GetComponentInChildren<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void FiredProjectile()
    {
        this.GetComponentInChildren<Animator>().SetBool("fired", true);
        hasFired = true;
    }

    public void LateUpdate()
    {
        Vector2 dir = rb2d.velocity;
        if (!hasFired && dir != Vector2.zero)
            return;

        //turn to face the direction
        transform.right = dir;
        if (dir.x < 0)
            renderer.flipY = true;
        //float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        //if (angle < 180)
        //    renderer.flipY = true;
        //else
        //    renderer.flipY = false;
        //transform.rotation = Quaternion.AngleAxis(angle % 180, Vector3.forward);
    }
}
