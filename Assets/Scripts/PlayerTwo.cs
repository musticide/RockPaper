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
    [SerializeField] Button pTwoRockButton;
    [SerializeField] Button pTwoPaperButton;
    [SerializeField] Button pTwoScissorsButton;

    ScoreTracker scoreTracker;
    PlayerOne playerOne;
    GameManager gameManager;
    private void Awake()
    {
        scoreTracker = FindObjectOfType<ScoreTracker>();
        playerOne = FindObjectOfType<PlayerOne>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        
    }

    //ON Button Clicks
    public void OnPTwoRockClick()
    {
        pTwoPlay = 1;
        hasPTwoPlayed = true;
        SetPTwoButtons(false);
        if (playerOne.GetHasPOnePlayed() && hasPTwoPlayed)
        {
            gameManager.RoundWinCheck();
        }
    }

    public void OnPTwoPaperClick()
    {
        pTwoPlay = 2;
        hasPTwoPlayed = true;
        SetPTwoButtons(false);
        if (playerOne.GetHasPOnePlayed() && hasPTwoPlayed)
        {
            gameManager.RoundWinCheck();
        }
    }
    public void OnPTwoScissorsClick()
    {
        pTwoPlay = 3;
        hasPTwoPlayed = true;
        SetPTwoButtons(false);
        if (playerOne.GetHasPOnePlayed() && hasPTwoPlayed)
        {
            gameManager.RoundWinCheck();
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