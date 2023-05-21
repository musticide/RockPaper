using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerOne : MonoBehaviour
{
    bool hasPOnePlayed = false;

    int pOnePlay = 0;

    [Header("Buttons")]
    Button pOneRockButton;
    Button pOnePaperButton;
    Button pOneScissorsButton;
    GameUI gameUI;

    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameUI = FindObjectOfType<GameUI>();
    }
    private void Start()
    {
        pOneRockButton = gameUI.GetPOneButtonAtIndex(0);
        pOnePaperButton = gameUI.GetPOneButtonAtIndex(1);
        pOneScissorsButton = gameUI.GetPOneButtonAtIndex(2);
        gameUI.OnPOneClick = OnClickFunction;
    }

    //ON Button Clicks
    public void OnPOneRockClick()
    {
        pOnePlay = 1;
        hasPOnePlayed = true;
        SetPOneButtons(false);
        gameManager.RoundWinCheck();
    }

    public void OnPOnePaperClick()
    {
        pOnePlay = 2;
        hasPOnePlayed = true;
        SetPOneButtons(false);
        gameManager.RoundWinCheck();
    }
    public void OnPOneScissorsClick()
    {
        pOnePlay = 3;
        hasPOnePlayed = true;
        SetPOneButtons(false);
        gameManager.RoundWinCheck();
    }

    public void OnClickFunction(int index)
    {
        switch (index)
        {
            case 0:
                OnPOneRockClick();
                break;
            case 1:
                OnPOnePaperClick();
                break;
            case 2:
                OnPOneScissorsClick();
                break;
        }
    }
    
//Set methods
    public void SetPOnePlay(int var)
    {
        pOnePlay = var; 
    }
    public void SetHasPOnePlayed(bool var)
    {
        hasPOnePlayed = var;
    }
    public void SetPOneButtons(bool enable)
    {
        pOneRockButton.interactable = enable;
        pOneScissorsButton.interactable = enable;
        pOnePaperButton.interactable = enable;
    }

//Get methods
    public int GetPOnePlay()
    {
        return pOnePlay;
    }

    public bool GetHasPOnePlayed()
    {
        return hasPOnePlayed;
    }
}