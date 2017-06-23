using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            CollectableEffect();
        }
    }

    public virtual void CollectableEffect()
    {
        gameObject.SetActive(false);
    }
}

/*virtual functions are always public*/
