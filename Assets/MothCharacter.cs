using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothCharacter : Character
{
    private FlyingEnemy enemyController;
    private SpriteRenderer renderer;
    private PlayerDetector pd;
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider2D;
    private Animator animator;

    public Sprite dedmoth;

    private void Awake()
    {
        enemyController = GetComponent<FlyingEnemy>();
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        pd = GetComponentInChildren<PlayerDetector>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    public override void Die()
    {
        OnDeath?.Invoke(this);
        pd.GetComponent<CircleCollider2D>().radius = 0.1f;
        Destroy(animator);
        renderer.sprite = dedmoth;
        
        Destroy(enemyController);
        boxCollider2D.isTrigger = true;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.gravityScale = 3f;
        rb2d.AddForce(Vector2.up);

        Destroy(this.gameObject, 3f);
    }
}
