using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseUIPage : MonoBehaviour
{
    [SerializeField]
    Text phaseText;

    [SerializeField]
    int phaseNo;

    [SerializeField]
    int totalNumberOfPhases;

    [SerializeField]
    int phaseToLevelCounter;

    [SerializeField]
    int buttonLevelNo;

    private void Awake()
    {
        phaseNo = 1;
        phaseText.text = "Phase " + phaseNo.ToString();
        phaseToLevelCounter = 0;
    }

    public void LeftButton()
    {
        if(phaseNo <= 1)
        {
            phaseNo = 1;
            phaseText.text = "Phase " + phaseNo.ToString();
            phaseToLevelCounter = 0;
        }
        else
        {
            phaseNo -= 1;
            phaseText.text = "Phase " + phaseNo.ToString();
            phaseToLevelCounter -= 15;
        }
    }

    public void RightButton()
    {
        if (phaseNo >= totalNumberOfPhases)
        {
            phaseNo = totalNumberOfPhases;
            phaseText.text = "Phase " + phaseNo.ToString();
        }
        else
        {
            phaseToLevelCounter = phaseNo * 15;
            phaseNo += 1;
            phaseText.text = "Phase " + phaseNo.ToString();
        }
    }

    public void ButtonOne()
    {
        buttonLevelNo = 1 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonTwo()
    {
        buttonLevelNo = 2 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonThree()
    {
        buttonLevelNo = 3 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonFour()
    {
        buttonLevelNo = 4 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void Buttonfive()
    {
        buttonLevelNo = 5 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonSix()
    {
        buttonLevelNo = 6 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonSeven()
    {
        buttonLevelNo = 7 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonEight()
    {
        buttonLevelNo = 8 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonNine()
    {
        buttonLevelNo = 9 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonTen()
    {
        buttonLevelNo = 10 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonEleven()
    {
        buttonLevelNo = 11 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonTwelve()
    {
        buttonLevelNo = 12 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonThirteen()
    {
        buttonLevelNo = 13 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonFourteen()
    {
        buttonLevelNo = 14 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonFifteen()
    {
        buttonLevelNo = 15 + phaseToLevelCounter;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }
}
