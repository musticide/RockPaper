using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class PlayerOne : NetworkBehaviour
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
    }
    private void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
    }

    //ON Button Clicks
    public void OnPOneRockClick()
    {
        pOnePlay = 1;
        hasPOnePlayed = true;
        gameUI.SetPOneButtons(false);
        gameManager.RoundWinCheck();
    }

    public void OnPOnePaperClick()
    {
        pOnePlay = 2;
        hasPOnePlayed = true;
        gameUI.SetPOneButtons(false);
        gameManager.RoundWinCheck();
    }
    public void OnPOneScissorsClick()
    {
        pOnePlay = 3;
        hasPOnePlayed = true;
        gameUI.SetPOneButtons(false);
        gameManager.RoundWinCheck();
    }

    public void OnPOneClickFunction(int index)
    {
        Debug.Log("OnclickfunctionWasCalled");
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