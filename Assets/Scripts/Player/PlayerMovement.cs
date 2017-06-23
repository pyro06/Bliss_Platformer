using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AbstractPlayerBehavior
{
    [SerializeField]
    float speed;

    [SerializeField]
    float tempSpeed;

    SpriteRenderer playerSprite;

	// Use this for initialization
	void Start ()
    {
        playerSprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float horizontal = inputManagerInstance.SetDirection() * speed;
        rgbd.velocity = new Vector2(horizontal, rgbd.velocity.y);
        containerInstance.Init(containerInstance._trailLength, containerInstance._spawnRate, playerSprite, containerInstance._effectDuration, containerInstance._desiredColor);
    }

    private void Update()
    {
        if (!GameManager.gameManagerInstance.playerAlive)
        {
            speed = 0;
        }
        else
        {
            speed = tempSpeed;
        }
    }
}
