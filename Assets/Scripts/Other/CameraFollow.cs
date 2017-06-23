using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    float xMax;

    [SerializeField]
    float xMin;

    [SerializeField]
    float yMax;

    [SerializeField]
    float yMin;

    public float cameraSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        Vector3 pos = player.transform.position;
        pos.z = transform.position.z;

        //transform.position = pos;

        //transform.position = Vector3.Lerp(transform.position, pos, cameraSpeed * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(pos.x,xMin,xMax),Mathf.Clamp(pos.y,yMin,yMax),pos.z);
	}
}
