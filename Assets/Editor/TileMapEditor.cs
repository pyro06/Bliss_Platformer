﻿using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TileMap))]
public class TileMapEditor : Editor
{
    public TileMap map;

    private Object[] spriteReferences;

    private TileBrush brush;

    private Vector3 mouseHitPos;

    public override void OnInspectorGUI() 
    {
        GUILayout.BeginVertical();
        map.mapSize = EditorGUILayout.Vector2Field("Map Size: ", map.mapSize); 
        GUILayout.EndVertical();

        GUILayout.BeginHorizontal(); 
        GUILayout.Label("Texture2D "); 
        map.texture2D = (Texture2D)EditorGUILayout.ObjectField(map.texture2D, typeof(Texture2D), false); 
        GUILayout.EndHorizontal();

        if (map.texture2D != null) 
        { 
            GUILayout.BeginHorizontal(); 
            EditorGUILayout.LabelField("Tile Size:", map.tileSize.x + "x" + map.tileSize.y); 
            GUILayout.EndHorizontal();

            map.boxCollider = EditorGUILayout.Toggle("BoxCollider2D", map.boxCollider);
            map.addNormalTileScript = EditorGUILayout.Toggle("NormalTileScript", map.addNormalTileScript);
            map.addJumpTileScript = EditorGUILayout.Toggle("JumpTileScript", map.addJumpTileScript);
            map.addPoisonTileScript = EditorGUILayout.Toggle("PoisonTileScript", map.addPoisonTileScript);
            map.addAnimatorController = EditorGUILayout.Toggle("AnimatorController", map.addAnimatorController);
            map.addSpikeScript = EditorGUILayout.Toggle("SpikeScript", map.addSpikeScript);
            map.addLayer = EditorGUILayout.Toggle("Add Layer", map.addLayer);
            map.addCoinScript = EditorGUILayout.Toggle("CoinScript", map.addCoinScript);

        }

        if(map.texture2D)
        { 
            spriteReferences = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(map.texture2D)); 
            var sprite = (Sprite)spriteReferences[1];
            var width = sprite.textureRect.width; 
            var height = sprite.textureRect.height;
            map.tileSize = new Vector2(width, height);

            map.gridSize = new Vector2((width / 100) * map.mapSize.x, (height / 100) * map.mapSize.y);

            UpdateBrush(map.currentTileBrush);

        }
        GUILayout.BeginHorizontal(); 
        EditorGUILayout.LabelField("Grid Size In Units:", map.gridSize.x + "x" + map.gridSize.y);
        GUILayout.EndHorizontal();


        

    }

    void OnEnable() 
    { 
        map = target as TileMap; 
        Tools.current = Tool.View;

        if (map.tiles == null) 
        {
            GameObject go = new GameObject("Tiles"); 
            go.transform.parent = map.transform;
            go.transform.position = new Vector3(0, 0, 0); 
            map.tiles = go;
        }

    }

    void CreateBrush()
    { 
        if (brush == null) 
            brush = TileBrush.CreateBrush(map); 
    }
    void DestroyBrush() 
    { 
        if (brush != null) brush.Destroy();
    }

    void OnSceneGUI() 
    {
        if (Tools.current != Tool.View) 
        {
            DestroyBrush(); 
            Selection.activeGameObject = map.gameObject; 
            return; 
        } 
        else
        { 
            CreateBrush();
            UpdateHitPosition();
            MoveBrush();

        }
        if (map.texture2D != null && MouseOnMap()) 
        { 
            Event current = Event.current; 
            if (current.shift)
            { 
                Draw(); 
            }
            if (current.alt)
            { 
                RemoveTile(); 
            }
        }
        else 
        {
            DestroyBrush(); 
        }


}

    void OnDisable() 
    { 
        DestroyBrush(); 
    }
    public void UpdateBrush(Sprite sprite)
    { 
        if (brush != null)
            brush.UpdateBrush(sprite); 
    }

    void MoveBrush()
    { 
        Vector2 tileSize = new Vector2( map.tileSize.x / 100, map.tileSize.y / 100);
        var x = (float)(System.Math.Floor( (mouseHitPos.x) / tileSize.x) * tileSize.x) + map.transform.position.x;
        var y = (float)(System.Math.Floor((mouseHitPos.y) / tileSize.y) * tileSize.y) + map.transform.position.y;
        var row = (x - map.transform.position.x )/tileSize.x; var column = System.Math.Abs( (y - map.transform.position.y)) / tileSize.y - 1;
        var id = ((column * map.mapSize.x) + row);
        if (MouseOnMap()) 
        { 
            brush.tileId = (int)System.Math.Round(id); brush.transform.position = new Vector3(x + (tileSize.x * .5f), y + (tileSize.y * .5f), map.transform.position.z - 0.05f); 
        }

    }

    void UpdateHitPosition()
    {
        Plane p = new Plane((this.target as MonoBehaviour).transform.TransformDirection(Vector3.forward), new Vector3());
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        Vector3 hit = new Vector3(); float dist; if (p.Raycast(ray, out dist)) hit = ray.origin + ray.direction.normalized * dist; mouseHitPos = (this.target as MonoBehaviour).transform.InverseTransformPoint(hit);
    }

    bool MouseOnMap() 
    { 
        bool a = false;
        if (mouseHitPos.x > 0 && mouseHitPos.x < map.gridSize.x && mouseHitPos.y < 0 && mouseHitPos.y > -map.gridSize.y) 
        { 
            a = true; 
        } 
        return a;
    }

    void Draw()
    { 
        var id = map.texture2D.name + brush.tileId.ToString();
        float posX = brush.transform.position.x - 0.5f;
        float posY = brush.transform.position.y - 0.5f;
        GameObject tile = GameObject.Find( map.name + "/Tiles/tile_" + id);
        if (tile == null)
        {
            tile = new GameObject("tile_" + id);
            tile.transform.parent = map.tiles.transform;
            tile.transform.position = new Vector3( posX + 0.5f,posY + 0.5f,0);
            tile.AddComponent<SpriteRenderer>();
        }
        tile.GetComponent<SpriteRenderer>().sprite = brush.GetComponent<SpriteRenderer>().sprite; 

        if (map.boxCollider)
        { 
            if (tile.GetComponent<BoxCollider2D>() == null) tile.AddComponent<BoxCollider2D>();
            tile.GetComponent<BoxCollider2D>().size = new Vector2(map.tileSize.x / 100, map.tileSize.y / 100); 
        } 
        else
        { 
            if (tile.GetComponent<BoxCollider2D>() != null) DestroyImmediate(tile.GetComponent<BoxCollider2D>()); 
        }

        if (map.addNormalTileScript)
        {
            if (tile.GetComponent<NormalTile>() == null)
                tile.AddComponent<NormalTile>();
        }
        else
        {
            if (tile.GetComponents<NormalTile>() != null)
                DestroyImmediate(tile.GetComponent<NormalTile>());
        }

        if (map.addJumpTileScript)
        {
            if (tile.GetComponent<JumpTile>() == null)
                tile.AddComponent<JumpTile>();
        }
        else
        {
            if (tile.GetComponents<JumpTile>() != null)
                DestroyImmediate(tile.GetComponent<JumpTile>());
        }
        if (map.addPoisonTileScript)
        {
            if (tile.GetComponent<PoisonTile>() == null)
                tile.AddComponent<PoisonTile>();
        }
        else
        {
            if (tile.GetComponents<PoisonTile>() != null)
                DestroyImmediate(tile.GetComponent<PoisonTile>());
        }
        if (map.addAnimatorController)
        {
            if (tile.GetComponent<Animator>() == null)
                tile.AddComponent<Animator>();
        }
        else
        {
            if (tile.GetComponents<Animator>() != null)
                DestroyImmediate(tile.GetComponent<Animator>());
        }
        if (map.addSpikeScript)
        {
            if (tile.GetComponent<Spike>() == null)
                tile.AddComponent<Spike>();
        }
        else
        {
            if (tile.GetComponents<Spike>() != null)
                DestroyImmediate(tile.GetComponent<Spike>());
        }
        if (map.addLayer)
        {
            tile.layer = 8;
        }
        else
        {
            tile.layer = 0;
        }
        if (map.addCoinScript)
        {
            if (tile.GetComponent<Coin>() == null)
                tile.AddComponent<Coin>();
        }
        else
        {
            if (tile.GetComponents<Coin>() != null)
                DestroyImmediate(tile.GetComponent<Coin>());
        }
    }

    void RemoveTile()
    {
        var id = map.texture2D.name + brush.tileId.ToString();
        GameObject tile = GameObject.Find(map.name + "/Tiles/tile_" + id);
        if (tile != null) DestroyImmediate(tile);
    }


}
