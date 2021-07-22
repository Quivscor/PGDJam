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

    [SerializeField] private float dashCd;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;
    private float dashTimeCurrent;
    private bool hasDashedAirborne = false;
    private float dashCdCurrent;

    private Vector2 velocity;
    private float velocityXSmoothing;
    private float maxJumpVelocity;
    private float gravity;

    private bool isForcedMoving = false;
    private Vector2 forcedMoveVelocity;
    private float forcedMoveTimeCurrent;

    private void Start()
    {
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    private void Update()
    {
        float targetVelocity;
        RegisteredInputs inputs = PlayerInput.GetPlayerInput();
        if (!isForcedMoving)
        {
            if (inputs.jump && state != ActorMovementState.AIRBORNE)
            {
                velocity.y += maxJumpVelocity;
                state = ActorMovementState.AIRBORNE;
            }
            if (inputs.releasedJump)
            {
                velocity.y *= variableJumpVelocityMultiplier;
            }
            targetVelocity = inputs.axis.x * moveSpeed;
        }
        else
        {
            targetVelocity = 0;
            forcedMoveTimeCurrent -= Time.deltaTime;
        }
        
        if(state == ActorMovementState.AIRBORNE)
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity, ref velocityXSmoothing, smoothTimeAirborne);
        else if(state == ActorMovementState.GROUNDED)
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity, ref velocityXSmoothing, smoothTime);
        else if(state == ActorMovementState.FORCEDMOVE)
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity, ref velocityXSmoothing, .3f);

        //dash
        if(inputs.dash && dashCdCurrent <= 0)
        {
            velocity.x = dashForce * Mathf.Sign(inputs.axis.x);
            dashCdCurrent = dashCd;
            dashTimeCurrent = dashTime;
            velocity.y = 0;
            if (state == ActorMovementState.AIRBORNE)
                hasDashedAirborne = true;
        }

        if (dashTimeCurrent > 0)
            dashTimeCurrent -= Time.deltaTime;
        else
            velocity.y += gravity * Time.deltaTime;

        Move(velocity * Time.deltaTime);

        if (collisions.above || collisions.below)
        {
            velocity.y = 0;
            if (collisions.below)
            {
                state = ActorMovementState.GROUNDED;
                hasDashedAirborne = false;
            }
            else if(!isForcedMoving)
                state = ActorMovementState.AIRBORNE;
        }
        if(isForcedMoving)
        {
            forcedMoveVelocity = velocity;
            if (forcedMoveTimeCurrent <= 0)
                isForcedMoving = false;
        }
        dashCdCurrent -= Time.deltaTime;
    }

    public void ForceMove(Vector2 force, float time = 1f)
    {
        isForcedMoving = true;
        forcedMoveVelocity = force;
        state = ActorMovementState.FORCEDMOVE;
        velocity = force;
        forcedMoveTimeCurrent = time;
    }
}
