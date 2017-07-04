using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;

    public static GameManager gameManagerInstance
    {
        get
        {
            return gameManager;
        }
    }

    private void Awake()
    {
        gameManager = this;
    }

    //Player related variables
    public Player playerInstance;
    public Vector2 spawnPoint;
    public bool playerAlive;

    //JumpTile related variables
    public bool jumpColliding;

    //time related variables
    public TimeManager timeManagerInstance;

    //collectable variables
    public float coinsCollected;
    public Coin[] totalNumberOfCoins;
    public float coinPercent;

    //Level variables
    public bool gotoNextLevel;

    private void Start()
    {
        playerAlive = true;
        gotoNextLevel = false;
        timeManagerInstance = GetComponent<TimeManager>();
        totalNumberOfCoins = GameObject.FindObjectsOfType<Coin>();
    }


}
