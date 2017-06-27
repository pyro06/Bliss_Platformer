using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTile : TileCollisionWithPlayer
{
    public override void Objective()
    {
        GameManager.gameManagerInstance.jumpColliding = true;
    }

    public override void ObjectiveCompleted()
    {
        GameManager.gameManagerInstance.jumpColliding = false;
    }
}
