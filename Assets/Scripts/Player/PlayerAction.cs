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
    float moveSpeed;
    [SerializeField]
    float tempMoveSpeed;

    float skinWidth = 0.015f;

    SpriteRenderer playerSprite;

    [SerializeField]
    bool isJumping;
    [SerializeField]
    bool fallFromEdge = false;

    //raycasting related variables
    [SerializeField]
    float rayDistance;

    float horizontalRayspacing;
    float verticalRayspacing;

    [SerializeField]
    int horizontalRaycount;
    [SerializeField]
    int verticalRaycount;

    //ground related all variables
    [SerializeField]
    LayerMask groundCollisionLayer;
    [SerializeField]
    bool grounded;
    [SerializeField]
    float distanceFromGround;
    [SerializeField]
    float groundJumpTimer = 0;

    //Wall related all the variables
    [SerializeField]
    float distanceFromWall;
    [SerializeField]
    Vector2 wallLeap;
    [SerializeField]
    bool wallSticking;
    [SerializeField]
    bool canWallJump;
    [SerializeField]
    float wallstickGravity;
    [SerializeField]
    LayerMask wallCollisionLayer;
    RaycastHit2D rightHit;
    RaycastHit2D leftHit;
    [SerializeField]
    int wallDir;
    [SerializeField]
    float wallJumpTimer = 0;
    [SerializeField]
    float delayTimer;
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
        canWallJump = false;
        
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
                GroundJump();   
            }
        }

        if (!grounded)
        {   //gravity
            rgbd.velocity = new Vector2(rgbd.velocity.x, rgbd.velocity.y - gravity.y * Time.deltaTime);
            //adding delay to jump before falling down
            if(rgbd.velocity.y < 0 && !isJumping)
            {
                GroundJumpDelayTimer();
            }
        }
        
        //adding a delay on the directions to wait for the player to jump before dropping off the wall
        if (wallSticking && wallDir == -inputManagerInstance.SetDirection())
        {
            WallJumpDelayTimer();
        }

        if (wallSticking)
        {
            canWallJump = true;
            OnWallStick();
            if (inputManagerInstance.Jump())
            {
                if (canWallJump)
                {   //when direction is opposite to the wall
                    WallJump();
                }
            }
        }
        else
        {
            canWallJump = false;
            wallJumpTimer = 0;
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

        VerticalRayCasting();

        HorizontalRayCasting();
    }

    void GroundJump()
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

    void WallJump()
    {
        if (wallDir == -inputManagerInstance.SetDirection())
        {
            rgbd.velocity = new Vector2(-wallDir * wallLeap.x, wallLeap.y);
        }
    }

    void GroundJumpDelayTimer()
    {
        if (groundJumpTimer < Time.deltaTime)
        {
            //some condition to make him jump from edge
            fallFromEdge = true;
            groundJumpTimer += Time.deltaTime;
        }
        else
        {
            fallFromEdge = false;
            groundJumpTimer = 0;
        }
    }

    void WallJumpDelayTimer()
    {
        if (wallJumpTimer < delayTimer)
        {
            wallJumpTimer += Time.deltaTime;
            rgbd.velocity = new Vector2(0, rgbd.velocity.y);
        }
        else
        {
            wallJumpTimer = 0;
            rgbd.velocity = new Vector2(rgbd.velocity.x, rgbd.velocity.y);
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

        horizontalRayspacing = bounds.size.y / (horizontalRaycount - 1);
        verticalRayspacing = bounds.size.x / (verticalRaycount - 1);
    }

    void Movement()
    {
        float horizontal = inputManagerInstance.SetDirection() * moveSpeed;
        rgbd.velocity = new Vector2(horizontal, rgbd.velocity.y);
        containerInstance.Init(containerInstance._trailLength, containerInstance._spawnRate, playerSprite, containerInstance._effectDuration, containerInstance._desiredColor);
    }

    void VerticalRayCasting()
    {
        for (int i = 0; i < verticalRaycount; i++)
        {
            if (grounded)
            {
                continue;
            }

            Vector2 rayOrigin = raycastOrigins.bottomLeft + (Vector2.right * verticalRayspacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayDistance, groundCollisionLayer);
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
    
    void HorizontalRayCasting()
    {
        for(int i = 0; i < horizontalRaycount; i++)
        {
       
            Vector2 rightRayOrigin = raycastOrigins.topRight + (Vector2.down * horizontalRayspacing * i);
            Vector2 leftRayOrigin = raycastOrigins.topLeft + (Vector2.down * horizontalRayspacing * i);

            rightHit = Physics2D.Raycast(rightRayOrigin, Vector2.right, rayDistance,wallCollisionLayer);
            leftHit = Physics2D.Raycast(leftRayOrigin, Vector2.right * -1, rayDistance, wallCollisionLayer);

            Debug.DrawRay(rightRayOrigin, Vector2.right * rayDistance, Color.red);
            Debug.DrawRay(leftRayOrigin, Vector2.left * rayDistance, Color.red);

            if (((rightHit && rightHit.distance < distanceFromWall) || (leftHit && leftHit.distance < distanceFromWall)) && !grounded && rgbd.velocity.y < 0)
            {
                wallSticking = true;
            }
            else
            {
                wallSticking = false;
            }
        }

        wallDir = (leftHit) ? -1 : 1;
    }

    void OnWallStick()
    {
        rgbd.velocity = new Vector2(rgbd.velocity.x, -wallstickGravity);
    }
}
