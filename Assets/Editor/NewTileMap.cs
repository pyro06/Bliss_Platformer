using UnityEngine;
using System.Collections;
using UnityEditor;

public class NewTileMap : EditorWindow
{
    [MenuItem("TileMap/CreateTileMap")]
    public static void Init()
    {
        GameObject go = new GameObject("TileMap");
        go.AddComponent<TileMap>();
    }
    
}
