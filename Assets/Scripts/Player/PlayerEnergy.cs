using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : Collectables
{
    public override void CollectableEffect()
    {
        base.CollectableEffect();
        GameManager.gameManagerInstance.playerInstance.amountOfEnergy += 15;
    }
}
