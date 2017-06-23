using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractPlayerBehavior : MonoBehaviour
{
    protected InputManager inputManagerInstance;
    protected Container containerInstance;
    protected Rigidbody2D rgbd;
    protected CollisionState collisionStateInstance;

    void Awake()
    {
        inputManagerInstance = GetComponent<InputManager>();
        containerInstance = GetComponent<Container>();
        rgbd = GetComponent<Rigidbody2D>();
        collisionStateInstance = GetComponent<CollisionState>();
    }
}

/*
 protected access modifier - protected can be accessed in the same class and can also be accessed by the 
 derived classes*/
