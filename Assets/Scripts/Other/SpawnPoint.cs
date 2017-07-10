using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        LevelManager.levelMangerInstance.playerInstance.transform.position = transform.position;
        print("spawn");
	}
	
	
}
