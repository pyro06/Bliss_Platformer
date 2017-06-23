using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollisionWithPlayer : MonoBehaviour
{
    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Objective();
        }
    }

    public virtual void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ObjectiveCompleted();
        }
    }

    public virtual void Objective()
    {
        print("Objective");
    }

    public virtual void ObjectiveCompleted()
    {
        print("ObjectiveCompleted");
    }
}
