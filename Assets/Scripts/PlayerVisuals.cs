using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer renderer;

    private bool dashTrigger = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetDmg()
    {
        animator.SetTrigger("dmg");
    }

    public void HandleAnimation(Vector2 velocity, ActorMovementState state, bool landed)
    {
        if (Mathf.Sign(velocity.x) > 0)
            renderer.flipX = false;
        else if (Mathf.Sign(velocity.x) < 0)
            renderer.flipX = true;

        animator.SetFloat("velocityX", velocity.x);
        animator.SetFloat("velocityY", velocity.y);
        animator.SetBool("landed", landed);

        if(state == ActorMovementState.AIRBORNE)
        {
            animator.SetTrigger("airborne");
        }

        if(state == ActorMovementState.DASH && !dashTrigger)
        {
            animator.SetTrigger("dash");
            dashTrigger = true;
        }

        //reset flag preventing dash trigger being set every frame
        if(dashTrigger && state != ActorMovementState.DASH)
        {
            dashTrigger = false;
        }
    }
}
