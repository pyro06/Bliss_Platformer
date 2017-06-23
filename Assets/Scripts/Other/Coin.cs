using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectables
{
    public override void CollectableEffect()
    {
        base.CollectableEffect();
        GameManager.gameManagerInstance.coinsCollected += 1;
    }
}
