using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level",menuName = "Saw Details",order = 1) ]
public class SawDetailsScriptableObjects : ScriptableObject
{
    public List<SawDetails> sawDetails = new List<SawDetails>();
}

[System.Serializable]
public class SawDetails
{
    public Vector2 fromPos;

    public Vector2 toPos;

    public float rotationSpeed;

    public float movementSpeed;

    public int direction;

    public bool upDownMovement;

    public bool rightLeftMovement;
}
