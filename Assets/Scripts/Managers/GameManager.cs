﻿using System.Collections;
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
    [SerializeField]
    PlayerEnergyUI playerEnergyBar;
    
    public PlayerEnergyUI PlayerEnergyBar
    {
        get
        {
            return playerEnergyBar;
        }
    }

    public bool playerAlive;

    //JumpTile related variables
    public bool jumpColliding;

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
        totalNumberOfCoins = GameObject.FindObjectsOfType<Coin>();
    }


}
