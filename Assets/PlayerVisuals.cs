using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer renderer;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void HandleAnimation(Vector2 velocity, ActorMovementState state)
    {
        if (Mathf.Sign(velocity.x) > 0)
            renderer.flipX = false;
        else if (Mathf.Sign(velocity.x) < 0)
            renderer.flipX = true;
    }
}
