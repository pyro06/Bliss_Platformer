using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickUp : TileCollisionWithPlayer
{
    public int energyPickupId;

    

    public override void Objective()
    {

        gameObject.SetActive(false);
        gameObject.transform.position = Vector2.zero;
    }
}
