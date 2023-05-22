using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Buttons")]

    [SerializeField] Button[] pOneButtons = new Button[3];
    [SerializeField] Button[] pTwoButtons = new Button[3];

    public delegate void OnClickDelegate(int index);
    public OnClickDelegate OnPOneClick;
    public OnClickDelegate OnPTwoClick;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject playScreen;

    PlayerOne playerOne;
    PlayerTwo playerTwo;

    public void OnPOneButtonClick(int index)
    {
        playerOne = FindObjectOfType<PlayerOne>();
        OnPOneClick = playerOne.OnPOneClickFunction;
        OnPOneClick(index);
    }

    public void OnPTwoButtonClick(int index)
    {
        playerTwo = FindObjectOfType<PlayerTwo>();
        OnPTwoClick = playerTwo.OnPTwoClickFunction;
        OnPTwoClick(index);
    }

    public Button GetPOneButtonAtIndex(int index)
    {
        return pOneButtons[index];        
    }

    public Button GetPTwoButtonAtIndex(int index)
    {
        return pTwoButtons[index];
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        playScreen.SetActive(false);
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        playScreen.SetActive(true);
    }

    public void SetPOneButtons(bool state)
    {
        for (int i=0; i < 3; i++) 
        { 
            pOneButtons[i].interactable = state;
        }
    }

    public void SetPTwoButtons(bool state)
    {
        for (int i =0; i<3; i++)
        {
            pTwoButtons[i].interactable = state;
        }
    }
}
