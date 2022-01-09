using UnityEngine;

public class Physics : MonoBehaviour
{
    const float skinWidth = 0.15f;
    int horizontalRayCount = 2;
    int verticalRayCount = 2;
    float horizontalRaySpacing;
    float verticalRaySpacing;

    BoxCollider2D boxCollider;
    RaycastOrigins raycastOrigins;
    public CollisionInfo collisionInfo;
    [SerializeField] LayerMask collisionMask;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void Move(Vector2 velocity)
    {
        UpdateRaycastOrigins();

        if (velocity != Vector2.zero)
        {
            collisionInfo.colliding = false;
            collisionInfo.collider = null;
        }
        
        if (velocity.x != 0)
        {
            collisionInfo.ResetHorizontal();
            HorizontalCollision(ref velocity);
        }

        if (velocity.y != 0)
        {
            collisionInfo.ResetVertical();
            VerticalCollision(ref velocity);
        }

        transform.Translate(velocity);
    }

    void HorizontalCollision(ref Vector2 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;
        Vector2 rayOrigin;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            if (directionX == -1) // Set location of ray to be created depending on horizontal direction
            {
                rayOrigin = raycastOrigins.bottomLeft;
            }
            else
            {
                rayOrigin = raycastOrigins.bottomRight;
            }

            rayOrigin += Vector2.up * (i * horizontalRaySpacing); // Set origin
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask); // Create raycast

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.green); // Draw debug rays

            if (hit == true)
            {
                velocity.x = (hit.distance - skinWidth) * directionX; // Subtract skinWidth because we added it to rayLength
                rayLength = hit.distance;

                if (directionX == -1) // Update collisionInfo
                {
                    collisionInfo.left = true;
                }
                else
                {
                    collisionInfo.right = true;
                }

                collisionInfo.colliding = true;
                collisionInfo.collider = hit.collider;
            }
        }
    }

    void VerticalCollision(ref Vector2 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;
        Vector2 rayOrigin;

        for (int i = 0; i < verticalRayCount; i++)
        {
            if (directionY == -1) // Set location of ray to be created depending on vertical direction
            {
                rayOrigin = raycastOrigins.bottomLeft;
            }
            else
            {
                rayOrigin = raycastOrigins.topLeft;
            }

            rayOrigin += Vector2.right * (i * verticalRaySpacing + velocity.x); // Set origin
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask); // Create raycast

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.green); // Draw debug rays

            if (hit == true)
            {
                velocity.y = (hit.distance - skinWidth) * directionY; // Subtract skinWidth because we added it to rayLength
                rayLength = hit.distance;

                if (directionY == -1) // Update collisionInfo
                {
                    collisionInfo.below = true;
                }
                else
                {
                    collisionInfo.above = true;
                }

                collisionInfo.colliding = true;
                collisionInfo.collider = hit.collider;
            }
        }
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = boxCollider.bounds; // Get bounds
        bounds.Expand(skinWidth * -2); // Shrink bounds

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y); // Set raycast origins
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = boxCollider.bounds; // Get bounds
        bounds.Expand(skinWidth * -2); // Shrink bounds

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue); // Make sure ray counts are 2 or greater each
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight, bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below, left, right;
        public bool colliding;
        public Collider2D collider;

        public void ResetHorizontal()
        {
            left = right = false;
        }

        public void ResetVertical()
        {
            above = below = false;
        }
    }
}
