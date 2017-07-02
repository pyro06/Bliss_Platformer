using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    Rigidbody2D rgbd;

    InputManager inputManagerInstance;

    Container containerInstance;

    [SerializeField]
    float normalJumpHeight;

    [SerializeField]
    float jumpHeightOnJumpTile;

    [SerializeField]
    float tempJump;

    [SerializeField]
    Vector2 gravity;

    [SerializeField]
    float timer = 0;

    [SerializeField]
    float delayTimer;

    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float tempMoveSpeed;

    [SerializeField]
    LayerMask collisionLayer;

    public bool grounded;

    float skinWidth = 0.015f;

    [SerializeField]
    int horizontalRaycount;
    [SerializeField]
    int verticalRaycount;

    [SerializeField]
    float distanceFromGround;

    [SerializeField]
    float rayDistance;

    //float horizontalRayspacing;
    float verticalRayspacing;

    SpriteRenderer playerSprite;

    [SerializeField]
    bool isJumping;

    [SerializeField]
    bool fallFromEdge= false;

    struct RayCastOrigins
    {
        public Vector2 bottomLeft, bottomRight;
        public Vector2 topLeft, topRight;
    }

    RayCastOrigins raycastOrigins;

    // Use this for initialization
    void Start ()
    {
        rgbd = GetComponent<Rigidbody2D>();
        inputManagerInstance = GetComponent<InputManager>();
        containerInstance = GetComponent<Container>();
        playerSprite = GetComponent<SpriteRenderer>();
        tempJump = normalJumpHeight;
        tempMoveSpeed = moveSpeed;
        
        CalculateRaySpacing();
    }

    private void FixedUpdate()
    {
        //Movement
        Movement();


        //Jumping
        if (inputManagerInstance.Jump())
        {
            if ((grounded || fallFromEdge) && !isJumping )
            {
                Jump();   
            }
        }

        
        if (!grounded)
        {   //gravity
            rgbd.velocity = new Vector2(rgbd.velocity.x, rgbd.velocity.y - gravity.y * Time.deltaTime);
            //adding delay to jump before falling down
            if(rgbd.velocity.y < 0 && !isJumping)
            {
                DelayTimer();
            }
        }
    }
    void Jump()
    {
        isJumping = true;
        grounded = false;
        fallFromEdge = false;
            if (!GameManager.gameManagerInstance.jumpColliding)
            {
                normalJumpHeight = tempJump;
                rgbd.velocity = new Vector2(rgbd.velocity.x, normalJumpHeight);
            }
            else
            {
                normalJumpHeight = jumpHeightOnJumpTile;
                rgbd.velocity = new Vector2(rgbd.velocity.x, normalJumpHeight);
            }
    }

    // Update is called once per frame
    void Update ()
    {
        if (!GameManager.gameManagerInstance.playerAlive)
        {
            rgbd.simulated = false;
            moveSpeed = 0;
        }
        else
        {
            rgbd.simulated = true;
            moveSpeed = tempMoveSpeed;
        }

        grounded = false;

        UpdateRaycastOrigins();

        RayCasting();
    }


    void DelayTimer()
    {
        if (timer < Time.deltaTime)
        {
            //some condition to make him jump from edge
            fallFromEdge = true;
            timer += Time.deltaTime;
        }
        else
        {
            fallFromEdge = false;
            timer = 0;
        }
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = playerSprite.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = playerSprite.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRaycount = Mathf.Clamp(horizontalRaycount, 2, int.MaxValue);
        verticalRaycount = Mathf.Clamp(verticalRaycount, 2, int.MaxValue);

        //horizontalRayspacing = bounds.size.y / (horizontalRaycount - 1);
        verticalRayspacing = bounds.size.x / (verticalRaycount - 1);
    }

    void Movement()
    {
        float horizontal = inputManagerInstance.SetDirection() * moveSpeed;
        rgbd.velocity = new Vector2(horizontal, rgbd.velocity.y);
        containerInstance.Init(containerInstance._trailLength, containerInstance._spawnRate, playerSprite, containerInstance._effectDuration, containerInstance._desiredColor);
    }

    void RayCasting()
    {
        for (int i = 0; i < verticalRaycount; i++)
        {
            if (grounded)
            {
                continue;
            }

            Vector2 rayOrigin = raycastOrigins.bottomLeft + (Vector2.right * verticalRayspacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayDistance, collisionLayer);
            Debug.DrawRay(rayOrigin, Vector2.down * rayDistance, Color.red);

            if (hit && hit.distance < distanceFromGround)
            {
                grounded = true;
                fallFromEdge = false;
                //jump ending
                if (isJumping)
                {
                    isJumping = false;
                }
            }
            else
            {
                grounded = false;

            }
        }
    }
}
