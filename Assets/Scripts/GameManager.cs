using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool haveBothPlayersPlayed = true;
    bool hasPlayerOnePlayed = false;
    bool hasPlayerTwoPlayed = false;
    PlayMaker playMaker;

        void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void RoundChange()
    {
        if (haveBothPlayersPlayed)
        {
            haveBothPlayersPlayed = false;
            hasPlayerOnePlayed = false;
            hasPlayerTwoPlayed = false;
            playMaker.ResetButtons();
            playMaker.ResetPlays();
        }        
    }
}
