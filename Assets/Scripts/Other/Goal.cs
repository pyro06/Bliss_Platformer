using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : TileCollisionWithPlayer
{
    public int goalTileId;

    private void Update()
    {
        Rotate();
    }

    public override void Objective()
    {
        LevelManager.levelMangerInstance.LoadNextLevel();
    }


    void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, -30) * Time.deltaTime, Space.Self);
    }
}
