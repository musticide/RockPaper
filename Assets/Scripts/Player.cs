using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    EventManager eventManager;
    //private int pOneChose;
    NetworkVariable<int> pOneChose = new NetworkVariable<int>() ;
    private void Start()
    {
        eventManager = FindObjectOfType<EventManager>();
        eventManager.OnPOneClick.AddListener(SetPOneChose);
    }

    private void SetPOneChose(int i)
    {
        pOneChose.Value = i;
        Debug.Log("PlayerOne Presed" + pOneChose.Value);
    }

    public int GetPOneChose()
    {
        return pOneChose.Value;
    }
}
