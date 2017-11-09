using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : TileCollisionWithPlayer
{
    public int gemTileId;

    public override void Objective()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = Vector2.zero;
    }
}
