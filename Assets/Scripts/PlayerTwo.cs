using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerTwo : MonoBehaviour
{
    bool hasPTwoPlayed = false;

    int pTwoPlay = 0;

    [Header("Buttons")]
    Button pTwoRockButton;
    Button pTwoPaperButton;
    Button pTwoScissorsButton;
    GameUI gameUI;

    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameUI = FindObjectOfType<GameUI>();
    }
    private void Start()
    {
        pTwoRockButton = gameUI.GetPTwoButtonAtIndex(0);
        pTwoPaperButton = gameUI.GetPTwoButtonAtIndex(1);
        pTwoScissorsButton = gameUI.GetPTwoButtonAtIndex(2);
        gameUI.OnPTwoClick = OnClickFunction; 
    }

    //ON Button Clicks
    public void OnPTwoRockClick()
    {
        pTwoPlay = 1;
        hasPTwoPlayed = true;
        SetPTwoButtons(false);
        gameManager.RoundWinCheck();
    }

    public void OnPTwoPaperClick()
    {
        pTwoPlay = 2;
        hasPTwoPlayed = true;
        SetPTwoButtons(false);
        gameManager.RoundWinCheck();
    }
    public void OnPTwoScissorsClick()
    {
        pTwoPlay = 3;
        hasPTwoPlayed = true;
        SetPTwoButtons(false);
        gameManager.RoundWinCheck();
    }

    public void OnClickFunction(int index)
    {
        switch (index)
        {
            case 0:
                OnPTwoRockClick();
                break;
            case 1:
                OnPTwoPaperClick();
                break;
            case 2:
                OnPTwoScissorsClick();
                break;
        }
    }
    //Set methods
    public void SetPTwoPlay(int var)
    {
        pTwoPlay = var;
    }

    public void SetHasPTwoPlayed(bool var)
    {
        hasPTwoPlayed = var;
    }
    public void SetPTwoButtons(bool enable)
    {
        pTwoRockButton.interactable = enable;
        pTwoScissorsButton.interactable = enable;
        pTwoPaperButton.interactable = enable;
    }

    //Get methods
    public int GetPTwoPlay()
    {
        return pTwoPlay;
    }

    public bool GetHasPTwoPlayed()
    {
        return hasPTwoPlayed;
    }
}