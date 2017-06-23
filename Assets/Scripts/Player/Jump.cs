using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : AbstractPlayerBehavior
{

    [SerializeField]
    float jumpHeight;

    [SerializeField]
    float jumpHeightOnJumpTile;

    // Update is called once per frame
    void Update ()
    {
		if (inputManagerInstance.Jump() && collisionStateInstance.standing && !GameManager.gameManagerInstance.jumpColliding)
        {
           rgbd.velocity = new Vector2(rgbd.velocity.x, jumpHeight);
        }
        else if (inputManagerInstance.Jump() && collisionStateInstance.standing && GameManager.gameManagerInstance.jumpColliding)
        {
            rgbd.velocity = new Vector2(rgbd.velocity.x, jumpHeightOnJumpTile);
        }
        else
        {
            rgbd.velocity = new Vector2(rgbd.velocity.x, rgbd.velocity.y);
        }

        if (!GameManager.gameManagerInstance.playerAlive)
        {
            rgbd.simulated = false;
        }
        else
        {
            rgbd.simulated = true;
        }
    }
}
