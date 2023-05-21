using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool haveBothPlayersPlayed = true;
    PlayerOne playerOne;
    PlayerTwo playerTwo;
    ScoreTracker scoreTracker;
    int pOnePoints;
    int pTwoPoints;
    bool isDraw;
    bool hasPOneScored;

    private void Awake()
    {
        playerOne = FindObjectOfType<PlayerOne>();
        playerTwo = FindObjectOfType<PlayerTwo>();
        scoreTracker = FindObjectOfType<ScoreTracker>();
    }
    private void Start()
    {
        
    }

    public void ChangeRound()
    {        
        SetPlayerInputs(true);
        playerOne.SetPOnePlay(0);
        playerTwo.SetPTwoPlay(0);
        playerOne.SetHasPOnePlayed(false);
        playerTwo.SetHasPTwoPlayed(false);
        pOnePoints = scoreTracker.GetPlayerOnePoints();
        pTwoPoints = scoreTracker.GetPlayerTwoPoints();
        
        if (pOnePoints >= 3)
        {
            Debug.Log("Player One Wins");
            SetPlayerInputs(false);

        }
        else if (pTwoPoints >= 3)
        {
            Debug.Log("Player Two Wins");
            SetPlayerInputs(false);
        }
    }

    void SetPlayerInputs(bool var)
    {
        playerOne.SetPOneButtons(var);
        playerTwo.SetPTwoButtons(var);
    }
    public  void RoundWinCheck()
    {
        //Debug.Log("RoundWinCheck Was called");
        int pOnePlay = playerOne.GetPOnePlay();
        int pTwoPlay = playerTwo.GetPTwoPlay();
        if (pOnePlay == 1)
        {
            if (pOnePlay == pTwoPlay)
            {
                isDraw = true;
            }
            else if (pTwoPlay == 2)
            {
                hasPOneScored = false;
            }
            else
            {
                hasPOneScored = true;
            }
        }

        else if (pOnePlay == 2)
        {
            if (pOnePlay == pTwoPlay)
            {
                isDraw = true;
            }
            else if (pTwoPlay == 3)
            {
                hasPOneScored = false;
            }
            else
            {
                hasPOneScored = true;
            }
        }
        else if (pOnePlay == 3)
        {
            if (pOnePlay == pTwoPlay)
            {
                isDraw = true;
            }
            else if (pTwoPlay == 1)
            {
                hasPOneScored = false;
            }
            else
            {
                hasPOneScored = true;
            }
        }

        scoreTracker.CalculateScore(hasPOneScored, isDraw);
        ChangeRound();
    }
}
