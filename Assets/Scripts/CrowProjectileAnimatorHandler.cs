using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowProjectileAnimatorHandler : MonoBehaviour
{
    SpriteRenderer renderer;

    private void Start()
    {
        GetComponent<Projectile>().FiredProjectile += FiredProjectile;
        renderer = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(EnableRendererAfterTime(.5f));
    }

    public void FiredProjectile()
    {
        this.GetComponentInChildren<Animator>().SetBool("fired", true);
    }

    public IEnumerator EnableRendererAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        renderer.color = Color.white;
    }
}
