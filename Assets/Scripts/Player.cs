using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;
using System;

public class Player : NetworkBehaviour
{
    NetworkVariable<int> playerChose = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<bool> playerPlayed = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
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

    }

    public void SetPlayerChose(int i)
    {
        if (!IsOwner) return;
        playerChose.Value = i;
        playerPlayed.Value = true;
    }

    public int GetPlayerChose()
    {
        return playerChose.Value;
    }
    public bool GetPlayerPlayed()
    {
        return playerPlayed.Value;
    }

    public void ResetPlayerPlayed()
    {
        if (!IsOwner) return;
        playerPlayed.Value = false;
    }

}
