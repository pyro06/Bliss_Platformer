using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //player
    InputManager inputManagerInstance;
    Rigidbody2D rgbd;
    Vector2 axisMovement;
    SpriteRenderer playerSprite;
    Container containerInstance;
    [SerializeField]
    Vector2 playerSpriteSize;
    [SerializeField]
    EdgeCollider2D playerCollider;
    PlayerAnimation playerAnimationInstance;
    [SerializeField]
    float energy;

    //movement
    float horizontal;
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    float playerFacingDirection;

    //jumping
    [SerializeField]
    float gravity;
    [SerializeField]
    float jumpHeight;
    [SerializeField]
    bool isGrounded;
    [SerializeField]
    bool canJump;
    [SerializeField]
    bool isJumping;
    [SerializeField]
    bool isJumpInput;
    [SerializeField]
    bool jumpLand;
    [SerializeField]
    bool fallFromEdge;
    [SerializeField]
    float groundJumpDelayTimer;

    //Raycasting
    [SerializeField]
    LayerMask groundCollisionLayer;
    [SerializeField]
    Ray2D ray;
    [SerializeField]
    float rayDistance;
    [SerializeField]
    float thresholdFromGround;
    [SerializeField]
    RaycastHit2D hit, hitHorizontal;
    [SerializeField]
    float spacingOffsetX;
    [SerializeField]
    float spacingOffsetY;
    [SerializeField]
    float skinWidthX;
    [SerializeField]
    float skinWidthY;

    //Wall related variables
    [SerializeField]
    bool wallSticking;
    [SerializeField]
    bool wallDetected;
    [SerializeField]
    float wallStickGravity;
    [SerializeField]
    float thresholdFromWall;
    [SerializeField]
    LayerMask wallCollisionLayer;

    [SerializeField]
    Vector2 raySize, raySizeHorizontal;
    int temp = 0;

    [SerializeField]
    int playerDir;
    [SerializeField]
    float wallJumpHeight;
    [SerializeField]
    float timer = 0;
    [SerializeField]
    float wallJumpDelayTimer;
    

    private void Start()
    {
        inputManagerInstance = GetComponent<InputManager>();
        rgbd = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        containerInstance = GetComponent<Container>();
        playerCollider = GetComponent<EdgeCollider2D>();
        playerAnimationInstance = GetComponent<PlayerAnimation>();
        playerFacingDirection = 1;
        energy = 100;
    }


    // Update is called once per frame
    void Update ()
    {
        //Only inputs like movemnet and jump
        horizontal = inputManagerInstance.SetDirection();
        

        if (!isJumpInput)
        {
            isJumpInput = inputManagerInstance.Jump();
        }

        //which direction is the player facing
        if (horizontal > 0)
        {
            playerFacingDirection = 1;
        }
        else if (horizontal < 0)
        {
            playerFacingDirection = -1;
        }
        transform.localScale = new Vector2(playerFacingDirection, transform.localScale.y);

        EnergyUsage();

#if UNITY_EDITOR
        DrawingOfRays();
#endif
    }

    private void FixedUpdate()
    {

        isGrounded = false;

        //ground check
        VerticalRayCasting();

        

        //movement
        axisMovement = new Vector2(horizontal * movementSpeed, axisMovement.y);
        
       
        //jumping
        if (isJumpInput && (isGrounded || fallFromEdge) && !isJumping && canJump)
        {
            Jump();
        }

        //Walljumping 
        if(isJumpInput && wallSticking && !isJumping && canJump)
        {
            //Apply Wallhit velocity
            WallJumping();
        }

        if (wallSticking && horizontal != 0 && playerDir == -GetSign(horizontal))
        {
            WallJumpDelay();
        }
        
        //Gravity
        if(!isGrounded)
        {
            Gravity();
            //doing wallsticking only when negative gravity is affecting
            if (!wallSticking && jumpLand)
            {
                //wall check
                HorizontalRayCasting();
            }
            GravityIsActing();
        }
        /*else
        {
            if (horizontal != 0)
            {
                playerSprite.flipX = false;
            }
        }*/

        //setting of player movement to the rigidbody velocity        
        rgbd.velocity = axisMovement;
        
    }

  

    //ground check function
    void VerticalRayCasting()
    {
        for(int i = -1 ; i < 2 ; i++)
        {
            //continuing so that all the rays check for ground check
            if(isGrounded)
            {
                continue;
            }
            raySize = new Vector2(transform.position.x - (playerCollider.bounds.size.x / 2 - spacingOffsetX) * i,
                                  transform.position.y - ((playerCollider.bounds.size.y - skinWidthY) / 2));

            hit = Physics2D.Raycast(raySize, Vector2.down, rayDistance, groundCollisionLayer);
          
            //Hit ground
            if (hit && hit.distance < thresholdFromGround)
            {
               
                isGrounded = true;
                wallSticking = false;
                fallFromEdge = false;
                timer = 0;
                //animations being called
                playerAnimationInstance.PlayAnimations(horizontal, isJumping, jumpLand,wallSticking);
                //resetting jump and gravity when vely is decraesing
                if (axisMovement.y < 0)
                {
                    JumpReset();
                    GravityReset();
                }
            }
            else
            {
                
                isGrounded = false;
                playerAnimationInstance.PlayAnimations(0, isJumping, jumpLand,wallSticking);
            }
        }
    }

    void HorizontalRayCasting()
    {
        for (int i = -1; i < 2; i++)
        {
            //horizontal ray casting for wall detection
            raySizeHorizontal = new Vector2(transform.position.x + ((playerCollider.bounds.size.x - skinWidthX) / 2) * GetSign(horizontal),
                                  transform.position.y - (playerCollider.bounds.size.y / 2 - spacingOffsetY) * i);

            hitHorizontal = Physics2D.Raycast(raySizeHorizontal, Vector2.right * GetSign(horizontal), rayDistance, wallCollisionLayer);
            Debug.DrawRay(raySizeHorizontal, Vector2.right * GetSign(horizontal) * rayDistance ,Color.red);
            playerDir = GetSign(horizontal);

            

            if (hitHorizontal && hitHorizontal.distance < thresholdFromWall)
            {
                //jumpreseting when wallsticking
                wallSticking = true;
                jumpLand = false;
                //making axis movement y to 0 so that the player doesnt move upward when touching the wall also
                //resetting the jump
                if(isJumping)
                {
                    axisMovement.y = 0;
                    JumpReset();
                }
            }
            else
            {
                wallSticking = false;
            }
          
        }
    }

    //drawing vertical rays in update
    void DrawingOfRays()
    {
        for (int i = -1; i < 2; i++)
        {
            raySize = new Vector2(transform.position.x - (playerCollider.bounds.size.x / 2 - spacingOffsetX) * i,
                                 transform.position.y - playerCollider.bounds.size.y / 2);

            if (hit && hit.distance < thresholdFromGround)
            {
                Debug.DrawRay(raySize, Vector2.down * rayDistance, Color.green);
            }
            else
            {
                Debug.DrawRay(raySize, Vector2.down * rayDistance, Color.red);
            }
            
        }

        containerInstance.Init(containerInstance._trailLength, containerInstance._spawnRate, playerSprite, containerInstance._effectDuration, containerInstance._desiredColor);
    }

    //set the player gravity
    void Gravity()
    {
        //wallstick gravity when hit the walls
        if (wallSticking)
        {
            axisMovement.y -= wallStickGravity * Time.deltaTime;
        }//normal gravity when inair
        else
        {
            axisMovement.y -= gravity * Time.deltaTime;
        }
    }

    void GravityIsActing()
    {
        if (axisMovement.y < 0)
        {
            jumpLand = true;
            isJumpInput = false;
            if (!isJumping && !wallSticking)
            {
                GroundJumpDelay();
            }
            else
            {
                fallFromEdge = false;
            }
        }
    }

    //resetting player gravity when on ground
    void GravityReset()
    {
        axisMovement.y = 0;
        jumpLand = false;
    }

    //player jumping
    void Jump()
    {
        
        isJumping = true;
        canJump = false;
        isGrounded = false;
        jumpLand = false;
        //wall jump timer
        timer = 0;

        axisMovement = new Vector2(axisMovement.x, jumpHeight);
        print(jumpHeight);
    }

    //player jump reset values when ater jumping
    void JumpReset()
    {
        canJump = true;
        isJumping = false;
    }

    //manually changing the direction of the ray with respect to the sign set
    int GetSign(float value)
    {
       if(value == 0)
        {
            return temp;
        }

       else
        {
            int sign = (value > 0) ? 1 : -1;

            if (temp != sign)
            {
                temp = sign;
            }

            return temp;
        }

      
    }

    //wall jumping function to check different inputs
    void WallJumping()
    {   //jump opposite/ same player direction to the wall
        if (playerDir == -GetSign(horizontal))
        {
            Jump();
            wallSticking = false;
        }
        else
        {
            isJumpInput = false;
        }
        //Jump same direction / player direction is same
    }

    //Delay added so that the player should not fall off the wall
    void WallJumpDelay()
    {
        if (timer < wallJumpDelayTimer)
        {
            timer += Time.deltaTime;
            //to keep the wall sticking
            wallSticking = true;
        }
        else
        {
            timer = 0;
            wallSticking = false;
        }
    }

    void GroundJumpDelay()
    {
        if (timer < groundJumpDelayTimer)
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

    void EnergyUsage()
    {
        if (axisMovement.x != 0 || axisMovement.y != 0)
        {
            energy -= 0.01f;
        }
    }
}
