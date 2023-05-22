using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool haveBothPlayersPlayed = true;
    PlayerOne playerOne;
    PlayerTwo playerTwo;
    ScoreTracker scoreTracker;
    GameUI gameUI;
    int pOnePoints;
    int pTwoPoints;
    bool isDraw;
    bool hasPOneScored;
    [SerializeField] GameObject playScreen;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] TextMeshProUGUI winnerText;

    private void Awake()
    {
        playerOne = FindObjectOfType<PlayerOne>();
        playerTwo = FindObjectOfType<PlayerTwo>();
        scoreTracker = FindObjectOfType<ScoreTracker>();
        gameUI = FindObjectOfType<GameUI>();
    }
    private void Start()
    {
        playScreen.SetActive(true);
        pauseMenu.SetActive(false);
        endScreen.SetActive(false);
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

        WinCheck();
    }

    void SetPlayerInputs(bool var)
    {
        gameUI.SetPOneButtons(var);
        gameUI.SetPTwoButtons(var);
    }
    public void RoundWinCheck()
    {
        if(playerOne.GetHasPOnePlayed() && playerTwo.GetHasPTwoPlayed())
        {        
            Points();
            scoreTracker.CalculateScore(hasPOneScored, isDraw);
            ChangeRound();
        }
    }

    void Points()
    {
        int pOnePlay = playerOne.GetPOnePlay();
        int pTwoPlay = playerTwo.GetPTwoPlay();
        switch (pOnePlay)
        {
            case 1:
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
                break;
            
            case 2:
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
                break;

            case 3:
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
                break;
        }
            
    }

    void WinCheck()
    {
        if (pOnePoints >= 3)
        {
            Debug.Log("Player One Wins!");
            endScreen.SetActive(true);
            playScreen.SetActive(false);
            SetPlayerInputs(false);
            winnerText.text = "Player One Wins!";
        }
        else if (pTwoPoints >= 3)
        {
            Debug.Log("Player Two Wins!");
            endScreen.SetActive(true);
            playScreen.SetActive(false);
            SetPlayerInputs(false);
            winnerText.text = "Player Two Wins!"; 
        }
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
