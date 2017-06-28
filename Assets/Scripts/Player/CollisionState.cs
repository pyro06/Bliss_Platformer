using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionState : AbstractPlayerBehavior
{
    [SerializeField]
    LayerMask collisionLayer;

    public bool standing;

    //Vector2 bottomPosition;

    //Vector2 collisionSize;

    float skinWidth = 0.015f;

    [SerializeField]
    int horizontalRaycount;
    [SerializeField]
    int verticalRaycount;

    [SerializeField]
    float distanceFromGround;

    [SerializeField]
    float rayDistance;

    float horizontalRayspacing;
    float verticalRayspacing;

    SpriteRenderer sprite;

    bool groundHit = false;

    //CircleCollider2D col2D;

    struct RayCastOrigins
    {
        public Vector2 bottomLeft, bottomRight;
        public Vector2 topLeft, topRight;
    }

    RayCastOrigins raycastOrigins;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        //col2D = GetComponent<CircleCollider2D>();
        CalculateRaySpacing();
       
    }

    private void Update()
    {
        standing = false;
        UpdateRaycastOrigins();

        for (int i = 0; i < verticalRaycount;i++)
        {
            if(standing)
            {
                continue;
            }

            Vector2 rayOrigin = raycastOrigins.bottomLeft + (Vector2.right * verticalRayspacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin,Vector2.down,rayDistance,collisionLayer);
            
            Debug.DrawRay(rayOrigin, Vector2.down * rayDistance, Color.red);
            
           
            if (hit && hit.distance < distanceFromGround)
            {
                
                standing = true;
            }
            else
            {
                standing = false;
               //StartCoroutine(JumpDelay());
            }
        }
        
      

    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = sprite.bounds;
        bounds.Expand (skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = sprite.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRaycount = Mathf.Clamp(horizontalRaycount, 2, int.MaxValue);
        verticalRaycount = Mathf.Clamp(verticalRaycount, 2, int.MaxValue);

        horizontalRayspacing = bounds.size.y / (horizontalRaycount - 1);
        verticalRayspacing = bounds.size.x / (verticalRaycount - 1);
    }

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(0.3f);
        standing = false;
        StopCoroutine(JumpDelay());
    }
}
