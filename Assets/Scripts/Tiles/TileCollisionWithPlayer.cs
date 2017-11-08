using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollisionWithPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Objective();
        }
    }

    /*private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ObjectiveCompleted();
        }
    }*/

    public virtual void Objective()
    {
        print("Objective");
    }

    public virtual void ObjectiveCompleted()
    {
        print("ObjectiveCompleted");
    }
}
