using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public Transform bullet;
    public List<Transform> allBullets;
    public static ObjectPooling o;
    int j = 0;

    private void Awake()
    {
        o = this;
    }

    // Use this for initialization
    void Start ()
    {
      
		for (int i = 0;i < 40;i++)
        {
            Transform g = Instantiate(bullet,new Vector2(0,0),Quaternion.identity) as Transform;
            allBullets.Add(g);
            allBullets[i].gameObject.SetActive(false);
            
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        

    }

    public void CallForBullets()
    {
        for (int i = 0; i < allBullets.Count; i++)
        {
            if (!allBullets[i].gameObject.activeSelf)
            {
                allBullets[i].gameObject.SetActive(true);
                break;
            }
        }
    }

}
