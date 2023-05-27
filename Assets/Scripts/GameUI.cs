using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Buttons")]

    [SerializeField] Button[] pOneButtons = new Button[3];
    [SerializeField] Button[] pTwoButtons = new Button[3];

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject playScreen;
    
    public void SetPauseMenu()
    {

    }

    public void SetPlayerButtons(bool state, bool forPlayerOne)
    {
        for(int i = 0; i < pOneButtons.Length; i++)
        {
            if (forPlayerOne)
            {
                pOneButtons[i].interactable = state;
            }
            else
            {
                pTwoButtons[i].interactable = state;
            }
        }
    }

    public void OnPOneClick(int buttonIndex)
    {
        WinCheck(buttonIndex);
    }

    private void WinCheck(int buttonIndex)
    {

    }
}
