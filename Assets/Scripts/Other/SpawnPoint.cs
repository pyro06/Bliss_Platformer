using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int spawnTileId;

	// Use this for initialization
	void Start ()
    {
        ResetPlayerSpawnPerLevel();
	}
	
	void ResetPlayerSpawnPerLevel()
    {
        GameManager.gameManagerInstance.playerInstance.transform.position = transform.position;
        LevelManager.levelMangerInstance.playerPosition = transform.position;
    }
}
