using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject enemyposition;

    public GameObject playerPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = playerPosition.transform.position.x - enemyposition.transform.position.x;
        float direction = Mathf.Sign(enemyposition.transform.right.x);
        Debug.Log(direction);
	}
}
