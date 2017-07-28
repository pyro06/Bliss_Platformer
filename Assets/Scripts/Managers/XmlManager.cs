using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class XmlManager : MonoBehaviour
{
    public string path;

    [SerializeField]
    int levelNo;

    public string levelName;

   

    [SerializeField]
    int totalNumberOfLevels;

    [SerializeField]
    List<LevelDetails> leveldetails = new List<LevelDetails>();

    //Array of normal tile gameobjects
    [SerializeField]
    NormalTile[] normalTiles;
    //array normal tile positions
    [SerializeField]
    Vector2[] normalTilePos;
    //maintaining normal tile id for loading
    [SerializeField]
    int[] normalTileIds;

    //Array of poison tile gameobjects
    [SerializeField]
    PoisonTile[] poisonTiles;
    //array of positions of poisonTiles
    [SerializeField]
    Vector2[] poisonTilePos;
    //maintaining the id for loading
    [SerializeField]
    int[] poisonTileIds;

    public GameObject normalTileGameobject;
    public GameObject poisonTileGameObject;

    private void Awake()
    {
        /*
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

        poisonTiles = GameObject.FindObjectsOfType<PoisonTile>();
        poisonTilePos = new Vector2[poisonTiles.Length];
        poisonTileIds = new int[poisonTiles.Length];
        //getting the normal tile gameobject positions
        for (int i = 0; i < poisonTiles.Length; i++)
        {
            poisonTilePos[i] = poisonTiles[i].gameObject.transform.position;
            poisonTileIds[i] = poisonTiles[i].poisonTileId;
        }
        */

        //load on Awake
        for (int j = 1; j <= totalNumberOfLevels; j++)
        {
            levelNo = j;
            levelName = levelNo.ToString();
            path = "/Resources/XMLlevels/" + levelName + ".xml";
            GameObject level = new GameObject(levelName);
            GameObject obj;
            leveldetails = LoadFromXml<List<LevelDetails>>();
            //levelObjects = new GameObject[leveldetails.Count];

            for (int i = 0; i < leveldetails.Count; i++)
            {
                if (leveldetails[i].id == 1)
                {
                    obj = Instantiate(normalTileGameobject, leveldetails[i].pos, Quaternion.identity);
                    obj.transform.parent = level.transform;
                }
                else if (leveldetails[i].id == 2)
                {
                    obj = Instantiate(poisonTileGameObject, leveldetails[i].pos, Quaternion.identity);
                    obj.transform.parent = level.transform;
                }
            }
            LevelManager.levelMangerInstance.levels.Add(level);
        }



        print("Loaded");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            path = "/Resources/XMLlevels/" + levelNo + ".xml";

            for (int i = 0; i < normalTiles.Length + poisonTiles.Length; i++)
            {
                if (i < normalTiles.Length)
                {
                    leveldetails.Add(new LevelDetails());
                    leveldetails[i].pos = normalTilePos[i];
                    leveldetails[i].id = normalTileIds[i];
                }
                else
                {

                    leveldetails.Add(new LevelDetails());
                    leveldetails[i].pos = poisonTilePos[i - normalTiles.Length];
                    leveldetails[i].id = poisonTileIds[i - normalTiles.Length];
                }
            }

            SaveToXml(leveldetails);
            print("Saved");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            /*for(int j = 1; j < totalNumberOfLevels; j++)
            {
                levelNo = j;
                levelName = levelNo.ToString();
                path = "/Resources/XMLlevels/" + levelName + ".xml";
                GameObject level = new GameObject(levelName);
                GameObject obj;
                leveldetails = LoadFromXml<List<LevelDetails>>();
                //levelObjects = new GameObject[leveldetails.Count];

                for (int i = 0; i < leveldetails.Count; i++)
                {
                    if (leveldetails[i].id == 1)
                    {
                        obj = Instantiate(normalTileGameobject, leveldetails[i].pos, Quaternion.identity);
                        obj.transform.parent = level.transform;
                    }
                    else if (leveldetails[i].id == 2)
                    {
                        obj = Instantiate(poisonTileGameObject, leveldetails[i].pos, Quaternion.identity);
                        obj.transform.parent = level.transform;
                    }
                }
                LevelManager.levelMangerInstance.levels.Add(level);
                LevelManager.levelMangerInstance.levels[j].gameObject.SetActive(false);
            }
            


            print("Loaded");*/
        }
    }

    //Saving
    void SaveToXml(object obj)
    {

        Debug.Log(obj.GetType());
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

}

[System.Serializable]
public class LevelDetails
{
    public Vector2 pos;

    public int id;
}
