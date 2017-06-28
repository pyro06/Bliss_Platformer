using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : AbstractPlayerBehavior
{

    [SerializeField]
    float jumpHeight;

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

    private void Start()
    {
        tempJump = jumpHeight;
    }

    
    

    void DelayTimer()
    {
        print("p");
        if (timer < delayTimer)
        {
            collisionStateInstance.standing = true;
            timer += Time.deltaTime;
           
        }
        else
        {
            timer = 0;
            collisionStateInstance.standing = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        
        if (inputManagerInstance.Jump() && collisionStateInstance.standing && !GameManager.gameManagerInstance.jumpColliding)
        {
            
            jumpHeight = tempJump;
            rgbd.velocity = new Vector2(rgbd.velocity.x, jumpHeight);
        }
        else if (inputManagerInstance.Jump() && collisionStateInstance.standing && GameManager.gameManagerInstance.jumpColliding)
        {
            jumpHeight = jumpHeightOnJumpTile;
            rgbd.velocity = new Vector2(rgbd.velocity.x, jumpHeight);
        }
       

        if (!GameManager.gameManagerInstance.playerAlive)
        {
            rgbd.simulated = false;
        }
        else
        {
            rgbd.simulated = true;
        }


        if (!collisionStateInstance.standing)
        {
            rgbd.velocity = new Vector2(rgbd.velocity.x, rgbd.velocity.y - gravity.y * Time.deltaTime);
            if (rgbd.velocity.y < 0)
            {
                DelayTimer();
            }
        }

    }
}
