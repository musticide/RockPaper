using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    EventManager eventManager;
    //private int pOneChose;
    NetworkVariable<int> playerChose = new NetworkVariable<int>();
    int playerID;
    private void Start()
    {

        if (IsServer)
        {
            playerID = 0;
        }
        else
        {
            playerID = 1;
        }
    }

    public void SetPlayerChose(int i)
    {
        playerChose.Value = i;
        if (playerID == 0)
        {
            Debug.Log("PlayerOne Presed" + playerChose.Value);
        }
        else
        {
            Debug.Log("PlayerTwo Presed" + playerChose.Value);
        }
    }

    public int GetPlayerChose()
    {
        return playerChose.Value;
    }

    public int GetPlayerID()
    {
        return playerID;
    }
}
