using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    int buttonLevelNo;

    [SerializeField]
    int phaseNo;

    private void Awake()
    {
        PhaseTwo();
    }

    public void PhaseOne()
    {
        phaseNo = 0;
    }

    public void PhaseTwo()
    {
        phaseNo = 15;
    }

    public void PhaseThree()
    {
        phaseNo = 30;
    }

    public void ButtonOne()
    {
        buttonLevelNo = 1 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonTwo()
    {
        buttonLevelNo = 2 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }

    public void ButtonThree()
    {
        buttonLevelNo = 3 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }
    public void ButtonFour()
    {
        buttonLevelNo = 4 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }
    public void Buttonfive()
    {
        buttonLevelNo = 5 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }
    public void ButtonSix()
    {
        buttonLevelNo = 6 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }
    public void ButtonSeven()
    {
        buttonLevelNo = 7 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }
    public void ButtonEight()
    {
        buttonLevelNo = 8 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }
    public void ButtonNine()
    {
        buttonLevelNo = 9 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }
    public void ButtonTen()
    {
        buttonLevelNo = 10 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }
    public void ButtonEleven()
    {
        buttonLevelNo = 11 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }
    public void ButtonTwelve()
    {
        buttonLevelNo = 12 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }
    public void ButtonThirteen()
    {
        buttonLevelNo = 13 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }
    public void ButtonFourteen()
    {
        buttonLevelNo = 14 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;
    }
    public void ButtonFifteen()
    {
        buttonLevelNo = 15 + phaseNo;
        LevelManager.levelMangerInstance.currentLevelNo = buttonLevelNo;

    }
}
