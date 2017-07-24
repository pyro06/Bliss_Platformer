using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : TileCollisionWithPlayer
{
    public override void Objective()
    {
        LevelManager.levelMangerInstance.FetchLevelInformation();
    }
}
