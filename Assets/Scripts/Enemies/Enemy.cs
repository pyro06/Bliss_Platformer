using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject enemyposition;

    public GameObject playerPosition;

    [SerializeField]
    int direction;

    [SerializeField]
    float speed;

    [SerializeField]
    Vector2 position;

    [SerializeField]
    Vector2 distance;

    [SerializeField]
    bool run;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        distance = playerPosition.transform.position - enemyposition.transform.position;
        Debug.Log(distance);
        EnemyPlayerDistanceCheck();
        EnemyMovement();

	}

    void EnemyMovement()
    {
       if (run)
        {
            speed = 5;
        }
       else if ((!run && position.x <= 18.99f && position.x >= -18.99f))
        {
            speed = 5;
        }
       else
        {
            speed = 0;
        }

            transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
            position = transform.position;
            if ((position.x > 19) || (position.x <= -19))
            {
                direction = -direction;
            }
       
        
    }

    void EnemyPlayerDistanceCheck()
    {
        Vector2 thresholdDistance = new Vector2(38, 0);
        if ((distance.y >= -0.2f && distance.y < 2f && distance.x < thresholdDistance.x) )
        {
            run = true;
        }
        else
        {
            run = false;
        }
    }
}
