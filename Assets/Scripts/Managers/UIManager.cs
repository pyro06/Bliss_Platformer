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
    Text phaseText;

    [SerializeField]
    int phaseNo;

    [SerializeField]
    int totalNumberOfPhases;

    [SerializeField]
    int totalNumberOfLevelsUnlocked;

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

    public void PhaseButton()
    {
        uiPages[0].gameObject.SetActive(false);

        //setting the interactability off of all the buttons on every phase start
        for (int i = 0; i < allLevelButtons.Length; i++)
        {
            allLevelButtons[i].interactable = false;
        }

        uiPages[1].gameObject.SetActive(true);

        UnLockLevels(phaseNo);
    }

    void UnLockLevels(int currentPhaseNo)
    {
        print(currentPhaseNo);
        if (currentPhaseNo == 1)
        {
            if (totalNumberOfLevelsUnlocked < 15)
            {
                for (int i = 0; i < 15 - ((15 * currentPhaseNo) - totalNumberOfLevelsUnlocked); i++)
                {
                    allLevelButtons[i].interactable = true;
                }
            }
            else
            {   //change this when you do work next time
                for (int i = 0; i < 15; i++)
                {
                    allLevelButtons[i].interactable = true;
                }
            }
        }
        if (currentPhaseNo == 2)
        {
            if (totalNumberOfLevelsUnlocked < 15 * currentPhaseNo)
            {
                print("top");
                for (int i = 0; i < 15 - ((15 * currentPhaseNo) - totalNumberOfLevelsUnlocked); i++)
                {
                    allLevelButtons[i].interactable = true;
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    allLevelButtons[i].interactable = true;
                }
            }
        }
        if (currentPhaseNo == 3)
        {
            if (totalNumberOfLevelsUnlocked < 15 * currentPhaseNo)
            {
                print("top");
                for (int i = 0; i < 15 - ((15 * currentPhaseNo) - totalNumberOfLevelsUnlocked); i++)
                {
                    allLevelButtons[i].interactable = true;
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    allLevelButtons[i].interactable = true;
                }
            }
        }
        

    }

    public void BackToPhasePage()
    {
        uiPages[1].gameObject.SetActive(false);
        uiPages[0].gameObject.SetActive(true);
    }

    public virtual void LeftButton()
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

    public virtual void RightButton()
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
