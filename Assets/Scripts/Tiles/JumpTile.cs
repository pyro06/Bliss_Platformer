using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTile : TileCollisionWithPlayer
{
    Animator jumpTileAnimator;

    private void Start()
    {
        jumpTileAnimator = GetComponent<Animator>();
    }

    public override void Objective()
    {
        GameManager.gameManagerInstance.jumpColliding = true;
        PlayerTouchingJumpTile();
    }

    public override void ObjectiveCompleted()
    {
        GameManager.gameManagerInstance.jumpColliding = false;
        PlayerNotTouchingJumpTile();
    }

    //Animations
    void PlayerTouchingJumpTile()
    {
        jumpTileAnimator.SetBool("JumpTileTouching", true);
    }

    void PlayerNotTouchingJumpTile()
    {
        jumpTileAnimator.SetBool("JumpTileTouching", false);
    }
}
