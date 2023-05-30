using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;
using System;

public class Player : NetworkBehaviour
{
    public NetworkVariable<int> playerChose = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<bool> hasPlayed = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    GameUI gameUI;
    ScoreTracker scoreTracker;
    UnityEvent testEvent;
    private void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
        scoreTracker = FindObjectOfType<ScoreTracker>();
        gameUI.GetButton(0).onClick.AddListener(() =>
        {
            SetPlayerChose(0);
        });
        gameUI.GetButton(1).onClick.AddListener(() =>
        {
            SetPlayerChose(1);
        });
        gameUI.GetButton(2).onClick.AddListener(() =>
        {
            SetPlayerChose(2);
        });
        scoreTracker.OnClientJoin();
        scoreTracker.ResetEvent.AddListener(() =>
        {
            ResetHasPlayed();
        });

    }
    private void Update()
    {
        //Debug.Log(OwnerClientId + "; " + "Has played: " + hasPlayed.Value + "int: " + playerChose.Value);
    }

    private void SetPlayerChose(int i)
    {
        if (!IsOwner) return;
        playerChose.Value = i;
        hasPlayed.Value = true;       
    }

    void ResetHasPlayed()
    {
        if (!IsOwner) return;
        hasPlayed.Value = false;
        Debug.Log("id: " + OwnerClientId + " : " + hasPlayed.Value);
    }

}
