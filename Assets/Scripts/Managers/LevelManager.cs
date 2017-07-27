using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public List<GameObject> levels;

    [SerializeField]
    int currentLevelNo;

    public Vector2 playerPosition;

    private static LevelManager levelManager;

    public static LevelManager levelMangerInstance
    {
        get
        {
            return levelManager;
        }
    }

    private void Awake()
    {
        levelManager = this;
        currentLevelNo = 1;
        
    }

    /*private void Start()
    {
        for(int i = 0; i<6; i++)
        {
            levels[i].gameObject.SetActive(false);
        }
        LoadCurrentLevel();
    }*/

    public void FetchLevelInformation()
    {
        LoadNextLevel();
    }

    void LoadCurrentLevel()
    {
        levels[currentLevelNo - 1].gameObject.SetActive(true);
    }

    void LoadNextLevel()
    {
        levels[currentLevelNo].gameObject.SetActive(false);
        currentLevelNo++;
        levels[currentLevelNo].gameObject.SetActive(true);
    }

    public void ReSpawnPlayerSameLevel()
    {
        GameManager.gameManagerInstance.playerInstance.transform.position = playerPosition;
    }
}
