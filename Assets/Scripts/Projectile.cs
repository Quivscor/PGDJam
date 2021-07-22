using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private string userTag;

    private float damage;
    private Collider2D collider;
    private Rigidbody2D rb2d;

    private float timer;
    private bool startTimer = false;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        collider.enabled = false;
    }

    public void Construct(float damage, string userTag)
    {
        this.damage = damage;
        this.userTag = userTag;
        StartCoroutine(EnableCollisions());

        startTimer = true;
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

        Character enemy;
        PlayerCombat pc;
        if(collision.TryGetComponent<Character>(out enemy))
        {
            enemy.GetHit(damage);
        }
        else if(collision.TryGetComponent<PlayerCombat>(out pc))
        {
            pc.SendTakeDamageEvent(damage);
        }

        rb2d.velocity = Vector2.zero;
        Destroy(this.gameObject, .2f);
    }

    public IEnumerator EnableCollisions()
    {
        yield return new WaitForSeconds(.2f);
        collider.enabled = true;
    }
}
