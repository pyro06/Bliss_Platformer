using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTile : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = ObjectPooler.sharedInstance.GetPooledObjects("Bullet");
            print(bullet);
            if (bullet != null)
            {
                bullet.SetActive(true);
                
            }
        }
    }
}
