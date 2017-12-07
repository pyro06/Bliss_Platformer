using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

public class XmlManager : MonoBehaviour
{
    public static XmlManager xmlManagerInstance;

    public string path;

    //the actual level number you want tp play with
    [SerializeField]
    int levelNo;

    public string levelName;

    [SerializeField]
    List<LevelDetails> leveldetails = new List<LevelDetails>();
    //a list is made because these details are present for each and every saw and they are not collectively used
    [SerializeField]
    List<SawDetails> sawDetails = new List<SawDetails>();


    /* ids for gameobjects
     * 0. spawn point
     * 1. Normal tile (based on the phase)
     * 2. goal
     * 3. Energy
     * 4. gem
     * 5. saw
     */

    //Array of normal tile gameobjects
    [SerializeField]
    NormalTile[] normalTiles;
    //array normal tile positions
    [SerializeField]
    Vector2[] normalTilePos;
    //maintaining normal tile id for loading
    [SerializeField]
    int[] normalTileIds;

    //Array of goal gameobjects
    [SerializeField]
    Goal[] goalTiles;
    [SerializeField]
    Vector2[] goalPos;
    [SerializeField]
    int[] goalId;

    //Array of gem gameobjects
    [SerializeField]
    Gem[] gemTiles;
    [SerializeField]
    Vector2[] gemPos;
    [SerializeField]
    int[] gemId;

    //Array of energy tile gameobjects
    [SerializeField]
    EnergyPickUp[] energyTiles;
    //array of positions of poisonTiles
    [SerializeField]
    Vector2[] energyTilePos;
    //maintaining the id for loading
    [SerializeField]
    int[] energyTileIds;

    //Array of spawn points
    [SerializeField]
    SpawnPoint[] spawnTiles;
    [SerializeField]
    Vector2[] spawnTilePos;
    [SerializeField]
    int[] spawnTileId;

    //Array of saw gameobjects
    [SerializeField]
    Saw[] sawTiles;
    [SerializeField]
    Vector2[] sawTilePos;
    [SerializeField]
    int[] sawTileId;

    //Array of properties related to saw
    [SerializeField]
    float[] sawRotationSpeed;


    public GameObject normalTileGameobject;
    public GameObject goalTileGameObject;
    public GameObject energyTileGameObject;
    public GameObject gemTileGameObject;
    public GameObject spawnTileGameObject;
    public GameObject sawTileGameObject;

    private void Awake()
    {
        xmlManagerInstance = this;
        
        //getting the normal tile gameobjects
        normalTiles = GameObject.FindObjectsOfType<NormalTile>();
        normalTilePos = new Vector2[normalTiles.Length];
        normalTileIds = new int[normalTiles.Length];
        //getting the normal tile gameobject positions
        for(int i = 0; i < normalTiles.Length; i++)
        {
            normalTilePos[i] = new Vector2(RoundOff( normalTiles[i].gameObject.transform.position.x,2),RoundOff( normalTiles[i].gameObject.transform.position.y,2));
            normalTileIds[i] = normalTiles[i].normalTileId;
        }

        energyTiles = GameObject.FindObjectsOfType<EnergyPickUp>();
        energyTilePos = new Vector2[energyTiles.Length];
        energyTileIds = new int[energyTiles.Length];
        //getting the poison tile gameobject positions
        for (int i = 0; i < energyTiles.Length; i++)
        {
            energyTilePos[i] = energyTiles[i].gameObject.transform.position;
            energyTileIds[i] = energyTiles[i].energyPickupId;
        }

        goalTiles = GameObject.FindObjectsOfType<Goal>();
        goalPos = new Vector2[goalTiles.Length];
        goalId = new int[goalTiles.Length];
        for (int i = 0; i < goalTiles.Length; i++)
        {
            goalPos[i] = goalTiles[i].gameObject.transform.position;
            goalId[i] = goalTiles[i].goalTileId;
        }

        gemTiles = GameObject.FindObjectsOfType<Gem>();
        gemPos = new Vector2[gemTiles.Length];
        gemId = new int[gemTiles.Length];
        for (int i = 0; i < gemTiles.Length; i++)
        {
            gemPos[i] = gemTiles[i].gameObject.transform.position;
            gemId[i] = gemTiles[i].gemTileId;
        }

        spawnTiles = GameObject.FindObjectsOfType<SpawnPoint>();
        spawnTilePos = new Vector2[spawnTiles.Length];
        spawnTileId = new int[spawnTiles.Length];
        for (int i = 0; i < spawnTiles.Length; i++)
        {
            spawnTilePos[i] = spawnTiles[i].gameObject.transform.position;
            spawnTileId[i] = spawnTiles[i].spawnTileId;
        }

        sawTiles = GameObject.FindObjectsOfType<Saw>();
        sawTilePos = new Vector2[sawTiles.Length];
        sawTileId = new int[sawTiles.Length];

        sawRotationSpeed = new float[sawTiles.Length];
        for(int i = 0; i < sawTiles.Length; i++)
        {
            sawTilePos[i] = sawTiles[i].gameObject.transform.position;
            sawTileId[i] = sawTiles[i].sawTileId;

            sawRotationSpeed[i] = sawTiles[i].rotationSpeed;
        }

        
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            path = "/StreamingAssets/" + levelNo + ".xml";
            //please add the new gameobject length here or else it wont work
            //
            //
            //
            //
            for (int i = 0; i < normalTiles.Length + goalTiles.Length + energyTiles.Length + gemTiles.Length + spawnTiles.Length + sawTiles.Length; i++)
            {
               
                if (i < normalTiles.Length)
                {
                    leveldetails.Add(new LevelDetails());
                    leveldetails[i].pos = normalTilePos[i];
                    leveldetails[i].id = normalTileIds[i];
                }
                else if (i >= normalTiles.Length && i < normalTiles.Length + goalTiles.Length)
                {
                    leveldetails.Add(new LevelDetails());
                    leveldetails[i].pos = goalPos[i - normalTiles.Length];
                    leveldetails[i].id = goalId[i - normalTiles.Length];
                }
                else if (i >= normalTiles.Length + goalTiles.Length && i < normalTiles.Length + goalTiles.Length + energyTiles.Length)
                {
                    leveldetails.Add(new LevelDetails());
                    leveldetails[i].pos = energyTilePos[i - (normalTiles.Length + goalTiles.Length)];
                    leveldetails[i].id = energyTileIds[i - (normalTiles.Length + goalTiles.Length)];
                }
                else if (i >= normalTiles.Length + goalTiles.Length + energyTiles.Length && i < normalTiles.Length + goalTiles.Length + energyTiles.Length + gemTiles.Length)
                {
                    leveldetails.Add(new LevelDetails());
                    leveldetails[i].pos = gemPos[i - (normalTiles.Length + goalTiles.Length + energyTiles.Length)];
                    leveldetails[i].id = gemId[i - (normalTiles.Length + goalTiles.Length + energyTiles.Length)];
                }
                else if (i >= normalTiles.Length + goalTiles.Length + energyTiles.Length + gemTiles.Length && i < normalTiles.Length + goalTiles.Length + energyTiles.Length + gemTiles.Length + spawnTiles.Length)
                {
                    leveldetails.Add(new LevelDetails());
                    leveldetails[i].pos = spawnTilePos[i - (normalTiles.Length + goalTiles.Length + energyTiles.Length + gemTiles.Length)];
                    leveldetails[i].id = spawnTileId[i - (normalTiles.Length + goalTiles.Length + energyTiles.Length + gemTiles.Length)];
                }
                else
                {
                    leveldetails.Add(new LevelDetails());
                    leveldetails[i].pos = sawTilePos[i - (normalTiles.Length + goalTiles.Length + energyTiles.Length + gemTiles.Length + spawnTiles.Length)];
                    leveldetails[i].id = sawTileId[i - (normalTiles.Length + goalTiles.Length + energyTiles.Length + gemTiles.Length + spawnTiles.Length)];
                }
            }

            SaveToXml(leveldetails);
            print("Saved level");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            path = "/StreamingAssets/" + "SawProperties" + ".xml";
            for (int i = 0; i < sawTiles.Length; i++)
            {
                sawDetails.Add(new SawDetails());
                sawDetails[i].rotationSpeed = sawRotationSpeed[i];
            }

            SaveToXml(sawDetails);
            print("Saved saw details");
        }

        //only for trial to test if the levels are loading properly
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadLevel(levelNo);
        }
    }

    public void LoadLevel(int levelNo)
    {
        levelName = levelNo.ToString();
        path = "/" + levelName + ".xml";
        GameObject obj;
        
        leveldetails = LoadFromXml<List<LevelDetails>>();
        //saw properties need to loaded as the game starts to use it when required
        sawDetails = LoadFromXml<List<SawDetails>>();
        //levelObjects = new GameObject[leveldetails.Count];
        //Loading of the levels
        for (int i = 0; i < leveldetails.Count; i++)
        {
            if (leveldetails[i].id == 0)
            {
                obj = ObjectPooler.sharedInstance.GetPooledObjects("SpawnPoint");
                obj.transform.position = leveldetails[i].pos;
                obj.gameObject.SetActive(true);
            }
            else if (leveldetails[i].id == 1)
            {
                obj = ObjectPooler.sharedInstance.GetPooledObjects("NTile");
                obj.gameObject.SetActive(true);
                obj.transform.position = leveldetails[i].pos;
            }
            else if (leveldetails[i].id == 2)
            {
                obj = ObjectPooler.sharedInstance.GetPooledObjects("Goal");
                obj.gameObject.SetActive(true);
                obj.transform.position = leveldetails[i].pos;
            }
            else if (leveldetails[i].id == 3)
            {
                obj = ObjectPooler.sharedInstance.GetPooledObjects("Energy");
                obj.gameObject.SetActive(true);
                obj.transform.position = leveldetails[i].pos;
            }
            else if (leveldetails[i].id == 4)
            {
                obj = ObjectPooler.sharedInstance.GetPooledObjects("Gem");
                obj.gameObject.SetActive(true);
                obj.transform.position = leveldetails[i].pos;
            }
            else if (leveldetails[i].id == 5)
            {
                obj = ObjectPooler.sharedInstance.GetPooledObjects("Saw");
                obj.gameObject.SetActive(true);
                obj.transform.position = leveldetails[i].pos;
            }
        }
        print("Loaded level " + levelNo);
    }

    //working fine
    public void DeactivateCurrentLevel()
    {
        GameObject obj;
        for (int i = 0; i < leveldetails.Count; i++)
        {
            if (leveldetails[i].id == 0)
            {
                obj = ObjectPooler.sharedInstance.PutBackPooledObjects("SpawnPoint");
                obj.gameObject.SetActive(false);
                obj.transform.position = Vector2.zero;
            }
            else if (leveldetails[i].id == 1)
            {
                obj = ObjectPooler.sharedInstance.PutBackPooledObjects("NTile");
                obj.gameObject.SetActive(false);
                obj.transform.position = Vector2.zero;
            }
            else if (leveldetails[i].id == 2)
            {
                obj = ObjectPooler.sharedInstance.PutBackPooledObjects("Goal");
                obj.gameObject.SetActive(false);
                obj.transform.position = Vector2.zero;
            }
            else if (leveldetails[i].id == 3)
            {
                obj = ObjectPooler.sharedInstance.PutBackPooledObjects("Energy");
                if (obj != null)
                {
                    obj.gameObject.SetActive(false);
                    obj.transform.position = Vector2.zero;
                }
                
            }
            else if (leveldetails[i].id == 4)
            {
                obj = ObjectPooler.sharedInstance.PutBackPooledObjects("Gem");
                if (obj != null)
                {
                    obj.gameObject.SetActive(false);
                    obj.transform.position = Vector2.zero;
                }
            }
            else if (leveldetails[i].id == 5)
            {
                obj = ObjectPooler.sharedInstance.PutBackPooledObjects("Saw");
                if (obj != null)
                {
                    obj.gameObject.SetActive(false);
                    obj.transform.position = Vector2.zero;
                }
            }
        }
    }

    //Saving
    void SaveToXml(object obj)
    {
        XmlSerializer serializer = new XmlSerializer(obj.GetType());

        using (var stream = new StreamWriter(Application.dataPath + path))
        {
            serializer.Serialize(stream, obj);
        }
    }

    //Loading
    T LoadFromXml<T>()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        //changes the path

        //For android
        /*www class is been used 
         * because we are lolading the levels in android
         * in android the .apk is compressed and needs www class for the reader
         * */
        string streamingAssetPath = Application.streamingAssetsPath + path;

        WWW reader = new WWW(streamingAssetPath);

        while(!reader.isDone)
        {

        }

        using(var stream = new MemoryStream(reader.bytes))
        {
            object obj = serializer.Deserialize(stream);
            return (T)obj;
        }

    }

    float RoundOff(float value, int digits)
    {
        float mult = Mathf.Pow((float)10.0, digits); // just does 10 to the power 2 that is 100
        return Mathf.Round(value * mult) / mult; // 
    }

}

[System.Serializable]
public class LevelDetails
{
    public Vector2 pos;

    public int id;
}

[System.Serializable]
public class SawDetails
{
    //all the variables for saw
    public Vector2 fromPos;

    public Vector2 toPos;

    public float rotationSpeed;

    public float movementSpeed;

    public int direction;

    public bool upDownMovement;

    public bool rightLeftMovement;
}
