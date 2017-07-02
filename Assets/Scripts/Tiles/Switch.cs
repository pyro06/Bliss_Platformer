using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : TileCollisionWithPlayer
{
    Door doorTile;

	// Use this for initialization
	void Start ()
    {
        doorTile = GetComponentInChildren<Door>();
        doorTile.gameObject.SetActive(false);
	}

    public override void Objective()
    {
        doorTile.gameObject.SetActive(true);
    }
}
