using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : TileCollisionWithPlayer
{

    public override void Objective()
    {
        GameManager.gameManagerInstance.gotoNextLevel = true;
    }
    
    
}
