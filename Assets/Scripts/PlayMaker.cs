using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayMaker : MonoBehaviour
{
    bool hasPOnePlayed = false;
    bool hasPTwoPlayed = false;
    
    bool hasPlayerOneScored = false;
    bool isDraw = false;
    int pOnePlay = 0;
    int pTwoPlay = 0;

    [Header("Buttons")]
    [SerializeField] Button pOneRockButton;
    [SerializeField] Button pOnePaperButton;
    [SerializeField] Button pOneScissorsButton;
    [SerializeField] Button pTwoRockButton;
    [SerializeField] Button pTwoPaperButton;
    [SerializeField] Button pTwoScissorsButton;

    ScoreTracker scoreTracker;

    private void Awake()
    {
        scoreTracker = FindObjectOfType<ScoreTracker>();
    }

    public void OnPOneRockClick()
    {
        pOnePlay = 1;
        hasPOnePlayed = true;
        SetPOneButtons(false);
        if(hasPOnePlayed && hasPTwoPlayed)
        {
            RoundWinCheck();
        }
    }

    public void OnPOnePaperClick()
    {
        pOnePlay = 2;
        hasPOnePlayed = true;
        SetPOneButtons(false);
        if (hasPOnePlayed && hasPTwoPlayed)
        {
            RoundWinCheck();
        }
    }
    public void OnPOneScissorsClick()
    {
        pOnePlay = 3;
        hasPOnePlayed = true;
        SetPOneButtons(false);
        if (hasPOnePlayed && hasPTwoPlayed)
        {
            RoundWinCheck();
        }
    }
    public void OnPTwoRockClick()
    {
        pTwoPlay = 1;
        hasPTwoPlayed = true;
        SetPTwoButtons(false);
        if (hasPOnePlayed && hasPTwoPlayed)
        {
            RoundWinCheck();
        }
    }
    public void OnPTwoPaperClick()
    {
        pTwoPlay = 2;
        hasPTwoPlayed = true;
        SetPTwoButtons(false);
        if (hasPOnePlayed && hasPTwoPlayed)
        {
            RoundWinCheck();
        }
    }
    public void OnPTwoScissorsClick()
    {
        pTwoPlay = 3;
        hasPTwoPlayed = true;
        SetPTwoButtons(false);
        if (hasPOnePlayed && hasPTwoPlayed)
        {
            RoundWinCheck();
        }
    }

    public void ResetPlays()
    {
        pOnePlay = 0;
        pTwoPlay = 0;
    }
    void RoundWinCheck()
    {
        //Debug.Log("RoundWinCheck Was called");
        if (pOnePlay == 1)
        {
            if(pOnePlay == pTwoPlay)
            {
                isDraw = true;
            }
            else if (pTwoPlay == 2)
            {
                hasPlayerOneScored = false;
            }
            else
            {
                hasPlayerOneScored = true;
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
                hasPlayerOneScored = false;
            }
            else
            {
                hasPlayerOneScored = true;
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
                hasPlayerOneScored = false;
            }
            else
            {
                hasPlayerOneScored = true;
            }
        }

        scoreTracker.CalculateScore(hasPlayerOneScored, isDraw);
    }

    public void ResetButtons()
    {
        SetPOneButtons(true);
        SetPTwoButtons(true);
    }
    public void SetPOneButtons(bool enable)
    {
        pOneRockButton.interactable = enable;
        pOneScissorsButton.interactable = enable;
        pOnePaperButton.interactable = enable;
    }

    public void SetPTwoButtons(bool enable)
    {
        pTwoRockButton.interactable = enable;
        pTwoPaperButton.interactable = enable;
        pTwoScissorsButton.interactable = enable;
    }

    public void ResetPlayerBooleans()
    {
        hasPOnePlayed = false;
        hasPTwoPlayed = false;
        isDraw = false;
    }
}