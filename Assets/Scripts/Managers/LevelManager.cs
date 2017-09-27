using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public int currentLevelNo;

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

    private void Start()
    {
        //LoadCurrentLevel();
    }

    public void FetchLevelInformation()
    {
        LoadNextLevel();
    }

    void LoadCurrentLevel()
    {
        XmlManager.xmlManagerInstance.LoadLevel(currentLevelNo);
    }

    void LoadNextLevel()
    {
        /*if (currentLevelNo == 1)
        {
            XmlManager.xmlManagerInstance.LoadLevel(currentLevelNo);
        }*/
        //else
        //{
            XmlManager.xmlManagerInstance.DeactivateCurrentLevel();
            //currentLevelNo++;
            //XmlManager.xmlManagerInstance.LoadLevel(currentLevelNo);
       // }
    }

    public void ReSpawnPlayerSameLevel()
    {
        GameManager.gameManagerInstance.playerInstance.transform.position = playerPosition;
    }

}
