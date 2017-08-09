using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //making an array of all the UI pages
    //0. Phases Ui page
    //1. Levels Ui Page
    [SerializeField]
    UIManager[] uiPages;

    [SerializeField]
    Button[] allLevelButtons;

    //Protected access modifiers are used so that the derived classes can use the variables without making them public
    [SerializeField]
    protected Text phaseText;

    [SerializeField]
    protected int phaseNo;

    [SerializeField]
    protected int totalNumberOfPhases;

    [SerializeField]
    protected int phaseToLevelCounter;

    [SerializeField]
    protected int buttonLevelNo;


    private void Awake()
    {
        phaseNo = 1;
        phaseText.text = "Phase " + phaseNo.ToString();
        phaseToLevelCounter = 0;

        //PhaseOne();
        for (int i = 0; i<allLevelButtons.Length; i++)
        {
            allLevelButtons[i].interactable = false;
        }
    }

    private void Update()
    {
        for (int i = 0; i < allLevelButtons.Length; i++)
        {
            if (LevelManager.levelMangerInstance.currentLevelNo == i)
            {
                allLevelButtons[i-1].interactable = true;
            }
        }
    }

    public void PhaseButton()
    {
        uiPages[0].gameObject.SetActive(false);
        uiPages[1].gameObject.SetActive(true);
    }

    public void BackToPhasePage()
    {
        uiPages[1].gameObject.SetActive(false);
        uiPages[0].gameObject.SetActive(true);
    }

    public void LeftButton()
    {
        if (phaseNo <= 1)
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
