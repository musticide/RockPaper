using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using UnityEngine.Events;
using TMPro;

public class ScoreTracker : NetworkBehaviour
{    
    private Player playerOne;
    private Player playerTwo;
    public UnityEvent ResetEvent;
    [SerializeField] TextMeshProUGUI clientIDText;
    NetworkVariable<bool> bothHavePlayed = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    NetworkVariable<bool> isDraw = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);    
    private void Update()
    {
        
    }
    private void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        bothHavePlayed.OnValueChanged = delegate { CheckPoints() ; };
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
            //Debug.Log("Player One: " + playerOne.OwnerClientId);
            playerTwo = playerArray[1];
            //Debug.Log("Player Two: " + playerTwo.OwnerClientId);
            playerOne.hasPlayed.OnValueChanged = delegate { BothInputsRecieved();};
            playerTwo.hasPlayed.OnValueChanged = delegate { BothInputsRecieved();};                       
        }       
    }
    void BothInputsRecieved()
    {
        //Debug.Log("Player " + playerOne.OwnerClientId + "has Played = " + playerOne.hasPlayed.Value);
        //Debug.Log("Player " + playerTwo.OwnerClientId + "has Played = " + playerTwo.hasPlayed.Value);
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
        Debug.Log("player: " + playerOne.OwnerClientId + "Chose: " + playerOne.playerChose.Value);
        Debug.Log("player: " + playerTwo.OwnerClientId + "Chose: " + playerTwo.playerChose.Value);
        switch (playerOne.playerChose.Value)
        {
            case 0:
                Debug.Log("Case Zero");
                switch (playerTwo.playerChose.Value)
                {
                    case 0:
                        if (!IsServer) return;
                        Debug.Log("Case Draw");
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
        //ResetEvent.Invoke();
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
}    