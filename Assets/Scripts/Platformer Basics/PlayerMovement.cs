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
    [SerializeField] private float coyoteTime = .15f;
    private float coyoteTimeCurrent;
    private bool landed = true;

    [SerializeField] private float dashCd;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;

    [Header("AudioSources")]
    [SerializeField] private AudioSource jumpSource;
    [SerializeField] private AudioSource dashSource;

    private float dashTimeCurrent;
    private bool hasDashedAirborne = false;
    private float dashCdCurrent;

    public Vector2 velocity;

    private float velocityXSmoothing;
    private float maxJumpVelocity;
    private float gravity;

    private bool isForcedMoving = false;
    private Vector2 forcedMoveVelocity;
    private float forcedMoveTimeCurrent;

    private PlayerVisuals playerVisuals;

    private void Start()
    {
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        playerVisuals = GetComponent<PlayerVisuals>();
    }

    private void Update()
    {
        float targetVelocity;
        coyoteTimeCurrent -= Time.deltaTime;
        RegisteredInputs inputs = PlayerInput.GetPlayerInput();
        if (!isForcedMoving)
        {
            
            if (inputs.jump && ((state != ActorMovementState.AIRBORNE || (state == ActorMovementState.AIRBORNE && coyoteTimeCurrent > 0)) 
                && state != ActorMovementState.DASH))
            {
                jumpSource.Play();
                velocity.y += maxJumpVelocity;
                state = ActorMovementState.AIRBORNE;
                landed = false;
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
        if(StatsController.Instance.canDash)
        {
            if (inputs.dash && dashCdCurrent <= 0 && !hasDashedAirborne)
            {
                velocity.x = dashForce * Mathf.Sign(inputs.axis.x);
                dashCdCurrent = dashCd;
                dashTimeCurrent = dashTime;
                velocity.y = 0;
                if (state == ActorMovementState.AIRBORNE)
                    hasDashedAirborne = true;

                state = ActorMovementState.DASH;

                dashSource.Play();
            }
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
                landed = true;
            }
            else if(!isForcedMoving)
            {
                state = ActorMovementState.AIRBORNE;
                if(landed)
                {
                    coyoteTimeCurrent = coyoteTime;
                    landed = false;
                }   
            }
        }
        else if(!collisions.above)
        {
            state = ActorMovementState.AIRBORNE;
            if (landed)
            {
                coyoteTimeCurrent = coyoteTime;
                landed = false;
            }
        }

        if(isForcedMoving)
        {
            forcedMoveVelocity = velocity;
            if (forcedMoveTimeCurrent <= 0)
                isForcedMoving = false;
        }
        dashCdCurrent -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        playerVisuals.HandleAnimation(velocity, state);
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
