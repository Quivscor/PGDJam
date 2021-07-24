using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public AudioClip hitting;

    public string userTag;
    public ParticleSystem hitParticles;
    public ParticleSystem trailParticles;

    private float damage;
    private Collider2D collider;
    private Rigidbody2D rb2d;
    private SpriteRenderer renderer;
    private bool dealtDamage = false;

    private float timer;
    private bool startTimer = false;

    public Action FiredProjectile;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        collider.enabled = false;
    }

    public void Construct(float damage, string userTag)
    {
        this.damage = damage;
        this.userTag = userTag;
        StartCoroutine(EnableCollisions());

        startTimer = true;
        if(trailParticles != null)
            trailParticles.Play();

        FiredProjectile?.Invoke();
    }

    public void Update()
    {
        if (!startTimer)
            return;

        timer += Time.deltaTime;
        if (timer > 30)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(userTag) || collision.CompareTag("NoCollisions") || collision.CompareTag("Projectile"))
            return;

        if (dealtDamage)
            return;

        Character enemy;
        PlayerCombat pc;
        if(collision.TryGetComponent<Character>(out enemy) && enemy.GetComponent<DestroyableBlock>() == null)
        {
            enemy.GetHit(damage);
            StatsController.Instance.PlayHittingEnemySound();
        }
        else if(collision.TryGetComponent<PlayerCombat>(out pc))
        {
            pc.SendTakeDamageEvent(damage);
        }

        dealtDamage = true;

        if (hitParticles != null)
            hitParticles.Play();
        if(trailParticles != null)
            trailParticles.Stop();

        rb2d.velocity = Vector2.zero;
        Destroy(this.renderer);
        Destroy(this.gameObject, .55f);
    }

    public IEnumerator EnableCollisions()
    {
        yield return new WaitForSeconds(.2f);
        collider.enabled = true;
    }

    public void LateUpdate()
    {
        Vector2 dir = rb2d.velocity;
        if (!startTimer && dir != Vector2.zero)
            return;

        //turn to face the direction
        transform.right = dir;
        if (dir.x < 0)
            renderer.flipY = true;
    }
}
