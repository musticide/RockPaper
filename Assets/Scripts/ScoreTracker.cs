using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using UnityEngine.Events;
using TMPro;

public class ScoreTracker : NetworkBehaviour
{    
    //references
    private Player playerOne;
    private Player playerTwo;
    [SerializeField] TextMeshProUGUI clientIDText;
    GameUI gameUI;

    public UnityEvent ResetEvent;    
    NetworkVariable<bool> bothHavePlayed = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    NetworkVariable<bool> isDraw = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);    
    private void Update()
    {
        
    }
    private void Awake()
    {
        Initialize();
        gameUI = FindObjectOfType<GameUI>();
    }

    void Initialize()
    {
        bothHavePlayed.OnValueChanged = delegate { Invoke("CheckPoints", 0.3f); };
        isDraw.OnValueChanged = delegate { ResetParameters(); };
    }
    public void OnClientJoin()
    {        
        Player[] playerArray;
        playerArray = FindObjectsOfType<Player>();        
        if (playerArray.Length < 2)
        {
            return;
        }
        else if (playerArray.Length == 2)
        {
            playerOne = playerArray[0];            
            playerTwo = playerArray[1];            
            playerOne.hasPlayed.OnValueChanged = delegate { Invoke("PlayerInputRecieved", 0.3f);};
            playerOne.points.OnValueChanged = delegate { UpdateScore(); };
            playerTwo.hasPlayed.OnValueChanged = delegate { Invoke("PlayerInputRecieved", 0.3f); };
            playerTwo.points.OnValueChanged = delegate { UpdateScore(); };

        }       
    }
    void PlayerInputRecieved()
    {        
        if (playerOne.hasPlayed.Value && playerTwo.hasPlayed.Value)
        {
            if (IsServer) { 
            //Debug.Log("Both players have played");
            bothHavePlayed.Value = true;
            }           
        }
    }

    void CheckPoints()
    {
        if (!bothHavePlayed.Value) return;
        Debug.Log("Check Points Was Called");
        switch (playerOne.playerChose.Value)
        {
            case 0:
                switch (playerTwo.playerChose.Value)
                {
                    case 0:
                        if (!IsServer) return;                        
                        isDraw.Value = true;
                        break;
                    case 1:
                        IncrementPlayerTwoPoints();
                        break;
                    case 2:
                        IncrementPlayerOnePoints();
                        break;
                }
                break;
            case 1:
                switch (playerTwo.playerChose.Value)
                {
                    case 0:
                        IncrementPlayerOnePoints();
                        break;
                    case 1:
                        if (!IsServer) return;
                        isDraw.Value = true;
                        break;
                    case 2:
                        IncrementPlayerTwoPoints();
                        break;
                }
                break;
            case 2:
                switch (playerTwo.playerChose.Value)
                {
                    case 0:
                        IncrementPlayerTwoPoints();
                        break;
                    case 1:
                        IncrementPlayerOnePoints();
                        break;
                    case 2:
                        if (!IsServer) return;
                        isDraw.Value = true;
                        break;
                }
                break;
        }
        ResetParameters();
    }

    void IncrementPlayerOnePoints()
    {
        Debug.Log("1 points was called");
        if (!IsServer) return;
        playerOne.points.Value++;
        Debug.Log("Player One Points: " + playerOne.points.Value);
    }
    void IncrementPlayerTwoPoints()
    {
        Debug.Log("2 Points was Called");
        if (!IsServer) return;
        playerTwo.points.Value++;
        Debug.Log("Player Two Points: " + playerTwo.points.Value);
    }

    void UpdateScore()
    {
        gameUI.UpdateScoreText(playerOne.points.Value, playerTwo.points.Value);
    }
    void ResetParameters()
    {
        ResetEvent.Invoke();
        if (!IsServer) return;
        bothHavePlayed.Value = false;
        isDraw.Value = false;
    }
}    