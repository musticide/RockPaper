using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using UnityEngine.Events;

public class ScoreTracker : NetworkBehaviour
{    
    Player playerOne;
    private Player playerTwo;
    public UnityEvent ResetEvent;
    NetworkVariable<bool> bothHavePlayed = new NetworkVariable<bool>(false);
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
            Debug.Log("Player One: " + playerOne.OwnerClientId);
            playerTwo = playerArray[1];
            Debug.Log("Player Two: " + playerTwo.OwnerClientId);
            playerOne.hasPlayed.OnValueChanged = delegate { BothInputsRecieved();};
            playerTwo.hasPlayed.OnValueChanged = delegate { BothInputsRecieved();};
        }       
    }
    void BothInputsRecieved()
    {
        if (playerOne.hasPlayed.Value && playerTwo.hasPlayed.Value)
        {
            if (!IsClient) { 
            Debug.Log("Both players have played");
            bothHavePlayed.Value = true;
            }           
        }
    }

    void CheckPoints()
    {
        Debug.Log("both play value" + bothHavePlayed.Value);
        ResetEvent.Invoke();
    }
}    