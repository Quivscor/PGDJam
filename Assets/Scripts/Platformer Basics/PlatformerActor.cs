using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlatformerActor : MonoBehaviour
{
    private BoxCollider2D col2d;
    private RaycastOrigins origins;
    protected CollisionInfo collisions;

    const float skinWidth = .015f;
    [SerializeField] private int horizontalRayCount = 4;
    [SerializeField] private int verticalRayCount = 4;

    [SerializeField] private float horizontalRaySpacing;
    [SerializeField] private float verticalRaySpacing;

    public LayerMask collisionMask;
    public string softPlatformTag;
    public ActorMovementState state;

    protected virtual void Awake()
    {
        col2d = GetComponent<BoxCollider2D>();
        origins = new RaycastOrigins();
        collisions = new CollisionInfo();

        CalculateRaySpacing();
    }

    public void Move(Vector2 velocity)
    {
        UpdateRaycastOrigins();
        collisions.Reset();

        if (velocity.x != 0)
            HorizontalCollisions(ref velocity);

        if(velocity.y != 0)
            VerticalCollisions(ref velocity);

        transform.Translate(velocity);
    }

    private void HorizontalCollisions(ref Vector2 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? origins.bottomLeft : origins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            //Debug ray display
            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit)
            {
                //if raycast hit, reduce it's length to the difference between actor and obstacle
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
            }
        }
    }

    private void VerticalCollisions(ref Vector2 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? origins.bottomLeft : origins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            //Debug ray display
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
            
            if (hit)
            {
                if (hit.collider.tag == softPlatformTag)
                    if (directionY == 1 || hit.distance == 0)
                        continue;

                //if raycast hit, reduce it's length to the difference between actor and obstacle
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisions.above = directionY == 1;
                collisions.below = directionY == -1;
            }
        }
    }

    private void CalculateRaySpacing()
    {
        Bounds bounds = col2d.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
    private void UpdateRaycastOrigins()
    {
        Bounds bounds = col2d.bounds;
        bounds.Expand(skinWidth * -2);

        origins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        origins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        origins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        origins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    internal class RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    protected class CollisionInfo
    {
        public bool above, below, left, right;
        public void Reset()
        {
            above = below = left = right = false;
        }
    }
}

public enum ActorMovementState
{
    GROUNDED = 0,
    AIRBORNE = 1,
    FORCEDMOVE = 2,
    DASH = 3,
}