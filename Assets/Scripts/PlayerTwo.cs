using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

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

    //ON Button Clicks
    public void OnPTwoRockClick()
    {
        pTwoPlay = 1;
        hasPTwoPlayed = true;
        gameUI.SetPTwoButtons(false);
        gameManager.RoundWinCheck();
    }

    public void OnPTwoPaperClick()
    {
        pTwoPlay = 2;
        hasPTwoPlayed = true;
        gameUI.SetPTwoButtons(false);
        gameManager.RoundWinCheck();
    }
    public void OnPTwoScissorsClick()
    {
        pTwoPlay = 3;
        hasPTwoPlayed = true;
        gameUI.SetPTwoButtons(false);
        gameManager.RoundWinCheck();
    }

    public void OnPTwoClickFunction(int index)
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