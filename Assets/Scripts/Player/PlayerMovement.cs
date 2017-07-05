using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //player
    InputManager inputManagerInstance;
    Rigidbody2D rgbd;
    [SerializeField]
    Vector2 axisMovement;
    SpriteRenderer playerSprite;
    Container containerInstance;

    //movement
    float horizontal;
    [SerializeField]
    float movementSpeed;

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
    float offset;

    //Wall related variables
    [SerializeField]
    bool wallSticking, wallHit;
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

    private void Start()
    {
        inputManagerInstance = GetComponent<InputManager>();
        rgbd = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        containerInstance = GetComponent<Container>();

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
        if (isJumpInput && isGrounded && !isJumping && canJump)
        {
            Jump();
        }

        //Walljumping 
        if(isJumpInput && wallHit && !isJumping && canJump)
        {
            wallSticking = true;
        }

        //wallsticking to keep it running per frame for smoothness
        if(wallSticking)
        {
            //Apply Wallhit velocity
            WallJumping();
        }
        
        

        //WallSticking
        
        //Gravity
        if(!isGrounded)
        {
            Gravity();
            //wall check
            HorizontalRayCasting();
        }

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
       

            raySize = new Vector2(transform.position.x - (playerSprite.bounds.size.x / 2 - offset) * i,
                                  transform.position.y - playerSprite.bounds.size.y / 2);

            

            hit = Physics2D.Raycast(raySize, Vector2.down, rayDistance, groundCollisionLayer);
           

            //Hit ground
            if (hit && hit.distance < thresholdFromGround)
            {
               
                isGrounded = true;
                wallSticking = false;
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
            }
        }

      

    }

    void HorizontalRayCasting()
    {
        for (int i = -1; i < 2; i++)
        {
            //horizontal ray casting for wall detection
            raySizeHorizontal = new Vector2(transform.position.x + (playerSprite.bounds.size.x / 2) * GetSign(horizontal),
                                  transform.position.y - (playerSprite.bounds.size.y / 2 - offset) * i);

            hitHorizontal = Physics2D.Raycast(raySizeHorizontal, Vector2.right * GetSign(horizontal), rayDistance, wallCollisionLayer);
            Debug.DrawRay(raySizeHorizontal, Vector2.right * GetSign(horizontal) * rayDistance ,Color.red);
            playerDir = GetSign(horizontal);
            if (hitHorizontal && hitHorizontal.distance < thresholdFromWall)
            {
                //jumpreseting when wallsticking
                //wallSticking = true;
                wallHit = true;
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
                wallHit = false;
                //wallSticking = false;
            }
          
        }
    }

    //drawing vertical rays in update
    void DrawingOfRays()
    {
        for (int i = -1; i < 2; i++)
        {
            raySize = new Vector2(transform.position.x - (playerSprite.bounds.size.x / 2 - offset) * i,
                                 transform.position.y - playerSprite.bounds.size.y / 2);

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
        if (wallHit)
        {
            axisMovement.y -= wallStickGravity * Time.deltaTime;
        }//normal gravity when inair
        else
        {
            axisMovement.y -= gravity * Time.deltaTime;
        }
    }
    //resetting player gravity when on ground
    void GravityReset()
    {
        axisMovement.y = 0;
    }

    //player jumping
    void Jump()
    {
        
        isJumping = true;
        canJump = false;
        isGrounded = false;

        axisMovement = new Vector2(axisMovement.x, jumpHeight);
    }
    //player jump reset values when ater jumping
    void JumpReset()
    {
        canJump = true;
        isJumping = false;
        isJumpInput = false;
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
    {
        if (horizontal == 0)
        {
            axisMovement.x = playerDir * -1 * 3;
        }
    }
}
