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

    public float radiusA;
	
	void FixedUpdate ()
    {
        //pos represents the position of the gizmo. bottomposition is the offset
        Vector2 pos = bottomPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        standing = Physics2D.OverlapCircle(pos, radiusA, collisionLayer);
	}

    private void OnDrawGizmos()
    {
        Gizmos.color =  Color.red;

        //pos represents the position of the gizmo. bottomposition is the offset
        Vector2 pos = bottomPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        Gizmos.DrawWireSphere(pos, radiusA);
    }
}
