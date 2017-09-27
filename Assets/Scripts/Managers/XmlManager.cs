using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

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

    public GameObject normalTileGameobject;
    public GameObject goalTileGameObject;
    public GameObject energyTileGameObject;
    public GameObject gemTileGameObject;
    public GameObject spawnTileGameObject;

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
            normalTilePos[i] = normalTiles[i].gameObject.transform.position;
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
        //load on Awake
        /*float b = (float)2.3566666;
        float a = RoundOff(b,2);
        print(a);*/
        float mult = Mathf.Pow((float)10.0, 2);
        print(mult);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {
            path = "/Resources/XMLlevels/" + levelNo + ".xml";
            //please add the new gameobject length here or else it wont work
            //
            //
            //
            //
            for (int i = 0; i < normalTiles.Length + goalTiles.Length + energyTiles.Length + gemTiles.Length + spawnTiles.Length; i++)
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
                else
                {
                    leveldetails.Add(new LevelDetails());
                    leveldetails[i].pos = spawnTilePos[i - (normalTiles.Length + goalTiles.Length + energyTiles.Length + gemTiles.Length)];
                    leveldetails[i].id = spawnTileId[i - (normalTiles.Length + goalTiles.Length + energyTiles.Length + gemTiles.Length)];
                }
            }

            SaveToXml(leveldetails);
            print("Saved");
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
        path = "/Resources/XMLlevels/" + levelName + ".xml";
        GameObject obj;
        leveldetails = LoadFromXml<List<LevelDetails>>();
        //levelObjects = new GameObject[leveldetails.Count];
        //Loading of the levels
        for (int i = 0; i < leveldetails.Count; i++)
        {
            if (leveldetails[i].id == 0)
            {
                obj = ObjectPooler.sharedInstance.GetPooledObjects("SpawnPoint");
                obj.gameObject.SetActive(true);
                obj.transform.position = leveldetails[i].pos;
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
        }
        print("Loaded");
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
                obj.gameObject.SetActive(false);
                obj.transform.position = Vector2.zero;
            }
            else if (leveldetails[i].id == 4)
            {
                obj = ObjectPooler.sharedInstance.PutBackPooledObjects("Gem");
                obj.gameObject.SetActive(false);
                obj.transform.position = Vector2.zero;
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

        using (var stream = new StreamReader(Application.dataPath + path))
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
