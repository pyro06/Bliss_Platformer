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

    //key variables
    public int keysCollected;
    public Key[] totalNoOfKeys;

    //door variables
    public bool unLockDoor;

    private void Start()
    {
        playerAlive = true;
        timeManagerInstance = GetComponent<TimeManager>();
        totalNumberOfCoins = GameObject.FindObjectsOfType<Coin>();
        totalNoOfKeys = GameObject.FindObjectsOfType<Key>();
    }

    private void Update()
    {
        /*
        //calculating the total coins collected
        coinPercent = (coinsCollected / totalNumberOfCoins.Length) * 100;
        print(coinPercent);

        //checking to see if all the keys are collected
        if (keysCollected == totalNoOfKeys.Length)
        {
            print("Door opens");
        }
        */
    }

}
