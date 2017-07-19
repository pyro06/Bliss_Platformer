using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonSetter
{
    public string buttonName;
}

public class InputManager : MonoBehaviour
{
    public ButtonSetter[] allButtonNames;

    [SerializeField]
    float playerHorizontalValue;

    public float PlayerHorizontalValue
    {
        get
        {
            return playerHorizontalValue;
        }

        set
        {
            playerHorizontalValue = value;
        }
    }

    private void Update()
    {
        SetDirection();
    }
    //Taking input for movement left, right or no input
    public void SetDirection()
    {
        if (Input.GetKey(allButtonNames[0].buttonName))
        {
            PlayerHorizontalValue = 1;
        }
        else if (Input.GetKey(allButtonNames[1].buttonName))
        {
            PlayerHorizontalValue = -1;
        }
        else
        {
            PlayerHorizontalValue = 0;
        }
    }

    public void LeftDirection(float value)
    {
        PlayerHorizontalValue = value;
    }

    public void RightDirectiom(float value)
    {
        PlayerHorizontalValue = value;
    }
    //Taking input for jumping
    public bool Jump()
    {
        if (Input.GetKeyDown(allButtonNames[2].buttonName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
