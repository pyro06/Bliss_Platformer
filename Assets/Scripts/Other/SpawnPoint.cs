using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        ResetPlayerSpawnPerLevel();
	}
	
	void ResetPlayerSpawnPerLevel()
    {
        LevelManager.levelMangerInstance.playerInstance.transform.position = transform.position;
        LevelManager.levelMangerInstance.playerPosition = transform.position;
    }
}
