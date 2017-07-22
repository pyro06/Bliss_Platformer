using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laserBeam;
    [SerializeField]
    BoxCollider2D laserCollider;
    [SerializeField]
    float rayDistance;
    [SerializeField]
    LayerMask collisionLayer;
    [SerializeField]
    Vector2 laserPosition;

    private void Start()
    {
        laserCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //SetBeamLength();
        RotateBeam();
    }

    void SetBeamLength()
    {
        laserBeam.transform.localScale = new Vector2(rayDistance * 5, transform.localScale.y);
        Vector2 rayOrigin = new Vector2(transform.position.x + (laserCollider.bounds.size.x / 2), transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, rayDistance, collisionLayer);
        Debug.DrawRay(rayOrigin, Vector2.right * rayDistance, Color.green);

        if (hit)
        {
            rayDistance = hit.distance;
            laserBeam.transform.localScale = new Vector2(rayDistance * 5, transform.localScale.y);
        }
        else
        {
            rayDistance = 10;
            laserBeam.transform.localScale = new Vector2(rayDistance * 5, transform.localScale.y);
        }
    }

    void RotateBeam()
    {
        transform.Rotate(new Vector3(0, 0, -30) * Time.deltaTime,Space.Self);
    }
}
