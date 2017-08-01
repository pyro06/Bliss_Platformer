using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //Static instance of objectpooler for other scripts to use
    public static ObjectPooler sharedInstance;

    public List<GameObject> pooledObjects;
    
    [System.Serializable]
    public class ObjectPoolItem
    {
        public GameObject objectToPool;
        public int numberOfObjectsToPool;
        public bool shouldExpand = true;
    }
    public List<ObjectPoolItem> itemsToPool;

    private void Awake()
    {
        sharedInstance = this;

        //initializing the list
        pooledObjects = new List<GameObject>();
        //added to foreach loop to take in all the items that need to be pooled
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.numberOfObjectsToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }

    }

    private void Start()
    {  
    }
    //getting all pooled objects which are not active in the scene
    public GameObject GetPooledObjects(string tag)
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {   //checking which objects out of the pool are active or inative and return inactive ones
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }//if the pool is nearing to an end and more objects need to be added
        //in the pool, this if checks for that
        foreach(ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
        
    }

    public GameObject PutBackPooledObjects(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {   //checking which objects out of the pool are active or inative and return inactive ones
            if (pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    
}
