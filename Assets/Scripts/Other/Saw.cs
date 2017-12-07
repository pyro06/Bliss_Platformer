using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public int sawTileId;

    public float rotationSpeed;

    public float movementSpeed;

    public float direction;

    public bool upDown;

    public bool rightLeft;

    public Vector2 from;

    public Vector2 to;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (upDown)
        {
            
            if (transform.position.x <= from.x)
            {
                direction = 1;
            }
            else if (transform.position.x >= to.x)
            {
                direction = -1;
            }
            transform.Rotate(new Vector3(0, 0, 30 * rotationSpeed * Time.deltaTime), Space.Self);

            transform.Translate(Vector2.up * movementSpeed * direction * Time.deltaTime, Space.World);
        }
        if (rightLeft)
        {
            
     
            if (transform.position.x <= from.x)
            {
                direction = 1;
            }
            else if (transform.position.x >= to.x)
            {
                direction = -1;
            }
            transform.Rotate(new Vector3(0, 0, 30 * rotationSpeed * -direction * Time.deltaTime), Space.Self);
            transform.Translate(Vector2.right * movementSpeed * direction * Time.deltaTime, Space.World);
        }
	}

}
