using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlatformerActor
{
    [Header("Movement data")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float variableJumpVelocityMultiplier;
    [SerializeField] private float timeToJumpApex;
    [SerializeField] private float smoothTime;
    [SerializeField] private float smoothTimeAirborne;

    private Vector2 velocity;
    private float velocityXSmoothing;
    private float maxJumpVelocity;
    private float gravity;

    private void Start()
    {
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space) && state != ActorMovementState.AIRBORNE)
        {
            velocity.y += maxJumpVelocity;
            state = ActorMovementState.AIRBORNE;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            velocity.y *= variableJumpVelocityMultiplier;
        }

        float targetVelocity = input.x * moveSpeed;
        if(state == ActorMovementState.AIRBORNE)
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity, ref velocityXSmoothing, smoothTimeAirborne);
        else
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity, ref velocityXSmoothing, smoothTime);
        velocity.y += gravity * Time.deltaTime;

        Move(velocity * Time.deltaTime);

        if (collisions.above || collisions.below)
        {
            velocity.y = 0;
            if (collisions.below)
                state = ActorMovementState.GROUNDED;
            else
                state = ActorMovementState.AIRBORNE;
        }
            
    }
}
