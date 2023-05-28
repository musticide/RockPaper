using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using System;

public class ScoreTracker : MonoBehaviour
{
    int playerOnePoints = 0;
    int playerTwoPoints = 0;
    [SerializeField] TextMeshProUGUI pOnePointsText;
    [SerializeField] TextMeshProUGUI pTwoPointsText;
    GameManager gameManager;
    List <Player> playerList;
    Player[] allPlayers;
    Player playerOne;
    Player playerTwo;
    bool isDraw;
    bool playerOneScored;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnClientJoin()
    {
        allPlayers = FindObjectsOfType<Player>();
        playerList = new List<Player>(allPlayers);
        if(playerList.Count > 1)
        {
            playerOne = playerList[0];
            //Debug.Log("Player One Client ID: " + playerOne.OwnerClientId);
            playerList.Remove(playerOne);
            playerTwo = playerList[0];
            //Debug.Log("Player Two Client ID: " + playerTwo.OwnerClientId);
            playerOne.playerPlayed.OnValueChanged += delegate { IsRoundComplete(); };
            playerTwo.playerPlayed.OnValueChanged += delegate { IsRoundComplete(); };
        }
        else
        {
            return;
        }
    }

    void UpdateScore()
    {
        pOnePointsText.text = "Player One Score: " + playerOnePoints;
        pTwoPointsText.text = "Player Two Score: " + playerTwoPoints;
        //Debug.Log("POne Score" + playerOnePoints);
        //Debug.Log("pTwo Score" + playerTwoPoints);
    }
    public void CalculateScore(bool pointsToPOne, bool isDraw)
    {
        if (pointsToPOne && !isDraw)
        {
            playerOnePoints++;
            Debug.Log(playerOnePoints);
        }
        else if (!pointsToPOne && !isDraw)
        {
            playerTwoPoints++;
            Debug.Log(playerTwoPoints);
        }
        UpdateScore();
        
    }

    public void IsRoundComplete()
    {
        if (playerOne.GetPlayerPlayed() && playerTwo.GetPlayerPlayed())
        {
            NextRound();
        }
    }
    public void RoundWinCheck()
    {
        switch (playerOne.GetPlayerChose())
        {
            case 0:
                if(playerOne.GetPlayerChose() == playerTwo.GetPlayerChose())
                {
                    isDraw = true;
                }
                else if(playerTwo.GetPlayerChose() == 1)
                {
                    Debug.Log("PlayerTwo scored");
                }
                else
                {
                    Debug.Log("Player One Scored");
                }
                break;
            case 1:
                if (playerOne.GetPlayerChose() == playerTwo.GetPlayerChose())
                {
                    isDraw = true;
                }
                else if (playerTwo.GetPlayerChose() == 2)
                {
                    Debug.Log("PlayerTwo scored");
                }
                else
                {
                    Debug.Log("Player One Scored");
                }
                break;
            case 2:
                if (playerOne.GetPlayerChose() == playerTwo.GetPlayerChose())
                {
                    isDraw = true;
                }
                else if (playerTwo.GetPlayerChose() == 0)
                {
                    Debug.Log("PlayerTwo scored");
                }
                else
                {
                    Debug.Log("Player One Scored");
                }
                break;
        }
    }

    private void ResetPlayed()
    {
        playerOne.ResetPlayerPlayed();
        playerTwo.ResetPlayerPlayed();
    }
    void NextRound()
    {
        RoundWinCheck();
        CalculateScore(playerOneScored, isDraw);
        UpdateScore();
        ResetPlayed();
    }

}
