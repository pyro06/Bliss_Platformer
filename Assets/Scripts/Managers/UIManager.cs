using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Button[] allLevelButtons;

    [SerializeField]
    int buttonLevelNo;

    [SerializeField]
    int phaseNo;

    [SerializeField]
    int phaseAddedCounter;


    private void Awake()
    {
        //PhaseOne();
        for(int i = 0; i<allLevelButtons.Length; i++)
        {
            allLevelButtons[i].interactable = false;
        }
    }

    private void Update()
    {
        for(int i = 0; i < allLevelButtons.Length; i++)
        {
            if (LevelManager.levelMangerInstance.currentLevelNo == i + 1)
            {
                allLevelButtons[i].interactable = true;
            }
        }
    }
    
}
