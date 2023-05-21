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

    public void OnPOneButtonClick(int index)
    {
        OnPOneClick(index);
    }

    public void OnPTwoButtonClick(int index)
    {
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
}
