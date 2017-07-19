using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{
    //Player components
    Rigidbody2D rgbd;
    EdgeCollider2D playerCollider;
    Container containerInstance;
    SpriteRenderer playerSprite;
    [SerializeField]
    Event eventSystem;

    //Player Movement
    [SerializeField]
    Vector2 movement;
    [SerializeField]
    float horizontal;
    float movementDirection;
    [SerializeField]
    float moveSpeed;
    float xVal;

    //Ground Jump
    [SerializeField]
    float gravity;
    [SerializeField]
    bool isGrounded;
    [SerializeField]
    float jumpHeight;
    [SerializeField]
    bool canJump;
    [SerializeField]
    bool inAir;
    [SerializeField]
    bool isJumpInput;
    [SerializeField]
    float tempWallGravity;
    [SerializeField]
    float timer = 0;
    [SerializeField]
    float groundTimer;
    [SerializeField]
    bool fallFromEdge;

    //Wall
    [SerializeField]
    bool wallSticking;
    [SerializeField]
    float wallStickGravity;
    [SerializeField]
    float thresholdFromWall;
    Vector2 raySizeHorizontalRight,raySizeHorizontalLeft;
    [SerializeField]
    int playerDir;
    [SerializeField]
    bool jump;
    [SerializeField]
    float wallJumptimer = 0;
    [SerializeField]
    float wallJumpDelayTimer;


    //RayCasting
    [SerializeField]
    LayerMask groundCollisionLayer;
    [SerializeField]
    Ray2D ray;
    [SerializeField]
    float rayDistance;
    [SerializeField]
    float thresholdFromGround;
    [SerializeField]
    RaycastHit2D hit, hitRight,hitLeft;
    [SerializeField]
    float spacingOffsetX;
    [SerializeField]
    float spacingOffsetY;
    Vector2 raySizeVertical;
    [SerializeField]
    float amountOfEnergy;

    private void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        containerInstance = GetComponent<Container>();
        playerCollider = GetComponent<EdgeCollider2D>();
        wallStickGravity = (gravity / 8);
        amountOfEnergy = 100;
        GameManager.gameManagerInstance.PlayerEnergyBar.CurrentVal = amountOfEnergy;
    }

    private void Update()
    {
        //Only inputs like movemnet and jump
        //Movement();
        horizontal = movementDirection;

        //Check for jump input
        //JumpInput();
        
        
        EnergyUsage();

        containerInstance.Init(containerInstance._trailLength, containerInstance._spawnRate, playerSprite, containerInstance._effectDuration, containerInstance._desiredColor);
    }

    private void FixedUpdate()
    {
        isGrounded = false;

        //Vertically checking for ground layer
        VerticalRayCasting();

        //Horizontal checking for wall as ground layer
        HorizontalRayCasting();

        //moveing left or right
        movement = new Vector2(horizontal * moveSpeed, movement.y);


        //checking which side of the wall the player is sticking
        HorizontalRayCastStatus();

        //Ground Jumping
        if (isJumpInput && isGrounded && canJump && !inAir)
        {
            Jump();
        }
        else if (isJumpInput && fallFromEdge)
        {
            Jump();
        }

        //wall Jumping
        if (isJumpInput && wallSticking && canJump && !inAir)
        {
            if (horizontal == -playerDir)
            {
                Jump();
            }
        }

        if (!isGrounded)
        {
            //applying gravity to the player
            Gravity();                      //last added
            if (movement.y < 0 && !inAir && !wallSticking)
            {
                GroundJumpTimer();
            }
            else
            {
                fallFromEdge = false;
            }
        }

        //setting of player movement to the rigidbody velocity        
        rgbd.velocity = movement;
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = -1;
        }
        else
        {
            horizontal = 0;
        }
    }

    void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumpInput)
            {
                isJumpInput = true;
            }
        }
    }

    public void LeftButtonDirection(float direction)
    {
        movementDirection = direction;
    }

    public void RightButtonDirection(float direction)
    {
        movementDirection = direction;
    }

    public void JumpButton(bool value)
    {
        if (!isJumpInput)
        {
            isJumpInput = value;
        }
    }

    void VerticalRayCasting()
    {
        for (int i = -1; i < 2; i++)
        {
            //continuing so that all the rays check for ground check
            if (isGrounded)
            {
                continue;
            }
            raySizeVertical = new Vector2(transform.position.x - (playerCollider.bounds.size.x / 2 - spacingOffsetX) * i,
                                  transform.position.y - ((playerCollider.bounds.size.y) / 2));
            Debug.DrawRay(raySizeVertical, Vector2.down * rayDistance, Color.red);

            hit = Physics2D.Raycast(raySizeVertical, Vector2.down, rayDistance, groundCollisionLayer);

            //Hit ground
            if (hit && hit.distance < thresholdFromGround)
            {
                Debug.DrawRay(raySizeVertical, Vector2.down * rayDistance, Color.green);
                isGrounded = true;
                //making vertical velocity 0 when on ground
                ResetVerticalVelocity();
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
            //horizontal Right ray casting for wall detection
            raySizeHorizontalRight = new Vector2(transform.position.x + ((playerCollider.bounds.size.x ) / 2),
                                  transform.position.y - (playerCollider.bounds.size.y / 2 - spacingOffsetY) * i);

            hitRight = Physics2D.Raycast(raySizeHorizontalRight, Vector2.right, rayDistance, groundCollisionLayer);
            Debug.DrawRay(raySizeHorizontalRight, Vector2.right* rayDistance, Color.red);

            raySizeHorizontalLeft = new Vector2(transform.position.x - ((playerCollider.bounds.size.x) / 2),
                                  transform.position.y - (playerCollider.bounds.size.y / 2 - spacingOffsetY) * i);

            hitLeft = Physics2D.Raycast(raySizeHorizontalLeft, Vector2.left, rayDistance, groundCollisionLayer);
            Debug.DrawRay(raySizeHorizontalLeft, Vector2.left* rayDistance, Color.red);

            if ((hitRight && hitRight.distance < thresholdFromWall) || (hitLeft && hitLeft.distance < thresholdFromWall))
            {
                wallSticking = true;
            }
            else
            {
                wallSticking = false;
            }
        }
    }

    //when gravity is being aplied
    void Gravity()
    {
        if (wallSticking)
        {
            if (isJumpInput)
            {
                WallJumpDelay();
            }
            inAir = false;
            canJump = true;
            
            
            //making normal gravity act till velocity in less than 0 otherwise use wallstick gravity
            if (movement.y < 0)
            {
                tempWallGravity -= wallStickGravity * Time.deltaTime;
                movement.y = tempWallGravity;
            }
            else
            {
                tempWallGravity = 0;
                movement.y -= gravity * Time.deltaTime;

            }
        }
        else
        {
            tempWallGravity = 0;
            movement.y -= gravity * Time.deltaTime;
            //isJumpInput = false;
            canJump = false;
            if (isJumpInput)
            {
                WallJumpDelay();
            }

        }
    }

    //when on ground
    void ResetVerticalVelocity()
    {
        movement.y = 0;
        inAir = false;
        canJump = true;
        timer = 0;
    }

    void Jump()
    {
        inAir = true;
        isJumpInput = false;
        wallSticking = false;
        movement = new Vector2(movement.x, jumpHeight);
        wallJumptimer = 0;
        timer = 0;
    }

    void HorizontalRayCastStatus()
    {
        if (hitRight)
        {
            playerDir = 1;
        }
        else if (hitLeft)
        {
            playerDir = -1;
        }
        else
        {
            playerDir = 0;
        }
    }

    void WallJumpDelay()
    {
        if (wallJumptimer < wallJumpDelayTimer)
        {
            wallJumptimer += Time.deltaTime;
            isJumpInput = true;
        }
        else
        {
            wallJumptimer = 0;
            isJumpInput = false;
        }
    }

    //added last delete from here if u want
    void GroundJumpTimer()
    {
        if (timer < groundTimer)
        {
            timer += Time.deltaTime;
            fallFromEdge = true;
        }
        else
        {
            timer = groundTimer;
            fallFromEdge = false;
        }
    }

    void EnergyUsage()
    {
        if ((movement.x != 0 || movement.y != 0))
        {
            if (wallSticking && isGrounded)
            {
                amountOfEnergy = GameManager.gameManagerInstance.PlayerEnergyBar.CurrentVal * 100;
            }
            else
            {
                amountOfEnergy -= 0.01f;
                GameManager.gameManagerInstance.PlayerEnergyBar.CurrentVal = amountOfEnergy;
            }
            
        }
    }
}
