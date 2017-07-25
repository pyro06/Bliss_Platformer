using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class XmlManager : MonoBehaviour
{
    public string path;

    [SerializeField]
    List<LevelDetails> leveldetails = new List<LevelDetails>();

    //Array of normal tile gameobjects
    [SerializeField]
    NormalTile[] normalTiles;
    //array normal tile positions
    [SerializeField]
    Vector2[] normalTilePos;

    [SerializeField]
    PoisonTile[] poisonTiles;
    [SerializeField]
    Vector2[] poisonTilePos;

    [SerializeField]
    GameObject[] levelObjects;

    public GameObject normalTile;

    private void Awake()
    {
        
        //getting the normal tile gameobjects
        normalTiles = GameObject.FindObjectsOfType<NormalTile>();
        normalTilePos = new Vector2[normalTiles.Length];
        //getting the normal tile gameobject positions
        for(int i = 0; i < normalTiles.Length; i++)
        {
            normalTilePos[i] = normalTiles[i].gameObject.transform.position;
        }

        poisonTiles = GameObject.FindObjectsOfType<PoisonTile>();
        poisonTilePos = new Vector2[poisonTiles.Length];
        for (int j = 0; j < poisonTiles.Length; j++)
        {
            poisonTilePos[j] = poisonTiles[j].gameObject.transform.position;
        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            for(int i = 0; i < normalTiles.Length + poisonTiles.Length; i++)
            {
                leveldetails.Add(new LevelDetails());
                if (i == normalTiles.Length - 1)
                {
                    leveldetails[i].pos = normalTilePos[i];
                }
                else
                {
                    leveldetails[i].pos = poisonTilePos[i];
                }
            }

            SaveToXml(leveldetails);
            print("Saved");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            leveldetails = LoadFromXml<List<LevelDetails>>();
            //levelObjects = new GameObject[leveldetails.Count];

            for (int i = 0; i < leveldetails.Count; i++)
            {
                Instantiate(normalTile, leveldetails[i].pos, Quaternion.identity);
            }

            print("Loaded");
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
}
