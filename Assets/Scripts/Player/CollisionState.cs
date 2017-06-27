using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionState : MonoBehaviour
{
    [SerializeField]
    LayerMask collisionLayer;

    public bool standing;

    [SerializeField]
    Vector2 bottomPosition;

    [SerializeField]
    Vector2 collisionSize;

    float skinWidth = 0.015f;

    [SerializeField]
    int horizontalRaycount;
    [SerializeField]
    int verticalRaycount;

    float horizontalRayspacing;
    float verticalRayspacing;

    SpriteRenderer sprite;

    struct RayCastOrigins
    {
        public Vector2 bottomLeft, bottomRight;
        public Vector2 topLeft, topRight;
    }

    RayCastOrigins raycastOrigins;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        CalculateRaySpacing();
    }

    private void Update()
    {
        UpdateRaycastOrigins();

        for(int i =0; i<verticalRaycount;i++)
        {
            Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right * verticalRayspacing * i, Vector2.up * -2, Color.red);
        }
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = sprite.bounds;
        bounds.Expand (skinWidth * 2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = sprite.bounds;
        bounds.Expand(skinWidth * 2);

        horizontalRaycount = Mathf.Clamp(horizontalRaycount, 2, int.MaxValue);
        verticalRaycount = Mathf.Clamp(verticalRaycount, 2, int.MaxValue);

        horizontalRayspacing = bounds.size.y / (horizontalRaycount - 1);
        verticalRayspacing = bounds.size.x / (verticalRaycount - 1);
    }
	
	/*void FixedUpdate ()
    {
        //pos represents the position of the gizmo. bottomposition is the offset
        Vector2 pos = bottomPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        standing = Physics2D.OverlapBox(pos, collisionSize, 90, collisionLayer);
	}*/

    /*private void OnDrawGizmos()
    {
        Gizmos.color =  Color.red;

        //pos represents the position of the gizmo. bottomposition is the offset
        Vector2 pos = bottomPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        Gizmos.DrawWireCube(pos, collisionSize);
    }*/


}
