using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] levels;

    [SerializeField]
    int currentLevelNo;

    public Player playerInstance;

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
        currentLevelNo = 0;
        LoadCurrentLevel();
        
    }

    public void FetchLevelInformation()
    {
        LoadNextLevel();
    }

    void LoadCurrentLevel()
    {
        levels[currentLevelNo].gameObject.SetActive(true);

    }

    void LoadNextLevel()
    {
        levels[currentLevelNo].gameObject.SetActive(false);
        currentLevelNo++;
        levels[currentLevelNo].gameObject.SetActive(true);
    }
}
