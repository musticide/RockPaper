using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using System;

public class ScoreTracker : NetworkBehaviour
{
    NetworkVariable<int> playerOnePoints = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);
    NetworkVariable<int> playerTwoPoints = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);
    //int playerOnePoints = 0;
    //int playerTwoPoints = 0;
    [SerializeField] TextMeshProUGUI pOnePointsText;
    [SerializeField] TextMeshProUGUI pTwoPointsText;
    GameManager gameManager;
    GameUI gameUI;
    List <Player> playerList;
    Player[] allPlayers;
    Player playerOne;
    Player playerTwo;
    NetworkVariable<bool> isDraw = new NetworkVariable<bool>(false);
    NetworkVariable<bool> playerOneScored = new NetworkVariable<bool>(false);
    //bool isDraw;
    //bool playerOneScored;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameUI = FindObjectOfType<GameUI>();
    }

    public void OnClientJoin()
    {
        allPlayers = FindObjectsOfType<Player>();
        playerList = new List<Player>(allPlayers);
        if(playerList.Count > 1)
        {
            playerOne = playerList[0];
            Debug.Log("Player One Client ID: " + playerOne.OwnerClientId);
            playerList.Remove(playerOne);
            playerTwo = playerList[0];
            Debug.Log("Player Two Client ID: " + playerTwo.OwnerClientId);
            playerOne.playerPlayed.OnValueChanged += delegate { RoundComplete(); };
            playerTwo.playerPlayed.OnValueChanged += delegate { RoundComplete(); };
        }
        else
        {
            return;
        }
    }


    public void RoundComplete()
    {
        if(IsOwner)gameUI.SetPlayerButtons(false);
        if (playerOne.GetPlayerPlayed() && playerTwo.GetPlayerPlayed())
        {
            Debug.Log("Player 1 has played: " + playerOne.GetPlayerPlayed());
            Debug.Log("Player 2 has played: " + playerTwo.GetPlayerPlayed());
            NextRound();
        }
    }
    void NextRound()
    {
        RoundWinCheck();
        CalculateScore(playerOneScored.Value, isDraw.Value);
        UpdateScore();
        ResetPlayed();
    }
    public void RoundWinCheck()
    {
        switch (playerOne.GetPlayerChose())
        {
            case 0:
                if(playerOne.GetPlayerChose() == playerTwo.GetPlayerChose())
                {
                    isDraw.Value = true;
                }
                else if(playerTwo.GetPlayerChose() == 1)
                {
                    playerOneScored.Value = false;
                }
                else
                {
                    playerOneScored.Value = true;
                }
                break;
            case 1:
                if (playerOne.GetPlayerChose() == playerTwo.GetPlayerChose())
                {
                    isDraw.Value = true;
                }
                else if (playerTwo.GetPlayerChose() == 2)
                {
                    playerOneScored.Value = false;
                }
                else
                {
                    playerOneScored.Value = true;
                }
                break;
            case 2:
                if (playerOne.GetPlayerChose() == playerTwo.GetPlayerChose())
                {
                    isDraw.Value = true;
                }
                else if (playerTwo.GetPlayerChose() == 0)
                {
                    playerOneScored.Value = false;
                }
                else
                {
                    playerOneScored.Value = true;
                }
                break;
        }
    }

    private void ResetPlayed()
    {
        playerOne.ResetPlayerPlayed();
        playerTwo.ResetPlayerPlayed();
        gameUI.SetPlayerButtons(true);
        isDraw.Value = false;
        playerOneScored.Value = false;
        Debug.Log("ResetPlayed was Called");
    }

    public void CalculateScore(bool pointsToPOne, bool isDraw)
    {
        if (pointsToPOne && !isDraw)
        {
            playerOnePoints.Value++;
            Debug.Log(playerOnePoints);
        }
        else if (!pointsToPOne && !isDraw)
        {
            playerTwoPoints.Value++;
            Debug.Log(playerTwoPoints);
        }
        UpdateScore();

    }
    void UpdateScore()
    {
        pOnePointsText.text = "Player One Score: " + playerOnePoints.Value;
        pTwoPointsText.text = "Player Two Score: " + playerTwoPoints.Value;
        //Debug.Log("POne Score" + playerOnePoints);
        //Debug.Log("pTwo Score" + playerTwoPoints);
    }

}
