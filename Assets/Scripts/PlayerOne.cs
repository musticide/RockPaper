using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerOne : MonoBehaviour
{
    bool hasPOnePlayed = false;
    bool hasPTwoPlayed;

    int pOnePlay = 0;

    [Header("Buttons")]
    [SerializeField] Button pOneRockButton;
    [SerializeField] Button pOnePaperButton;
    [SerializeField] Button pOneScissorsButton;

    ScoreTracker scoreTracker;
    PlayerTwo playerTwo;
    GameManager gameManager;
    private void Awake()
    {
        scoreTracker = FindObjectOfType<ScoreTracker>();
        playerTwo = FindObjectOfType<PlayerTwo>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        bool localHasPTwoPlayed = playerTwo.GetHasPTwoPlayed();

    }

//ON Button Clicks
    public void OnPOneRockClick()
    {
        pOnePlay = 1;
        hasPOnePlayed = true;
        SetPOneButtons(false);
        if(hasPOnePlayed && playerTwo.GetHasPTwoPlayed())
        {
            gameManager.RoundWinCheck();
        }
    }

    public void OnPOnePaperClick()
    {
        pOnePlay = 2;
        hasPOnePlayed = true;
        SetPOneButtons(false);
        if (hasPOnePlayed && playerTwo.GetHasPTwoPlayed())
        {
            gameManager.RoundWinCheck();
        }
    }
    public void OnPOneScissorsClick()
    {
        pOnePlay = 3;
        hasPOnePlayed = true;
        SetPOneButtons(false);
        if (hasPOnePlayed && playerTwo.GetHasPTwoPlayed())
        {
            gameManager.RoundWinCheck();
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