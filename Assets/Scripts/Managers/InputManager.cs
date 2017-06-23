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

    //Taking input for movement left, right or no input
    public float SetDirection()
    {
        if (Input.GetKey(allButtonNames[0].buttonName))
        {
            return 1;
        }
        else if (Input.GetKey(allButtonNames[1].buttonName))
        {
            return -1;
        }
        else
        {
            return 0;
        }
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
