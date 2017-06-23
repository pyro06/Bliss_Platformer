using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectables
{
    public override void CollectableEffect()
    {
        base.CollectableEffect();
        GameManager.gameManagerInstance.keysCollected += 1;
    }
}
