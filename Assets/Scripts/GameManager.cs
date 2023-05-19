using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool haveBothPlayersPlayed = true;
    PlayMaker playMaker;
    ScoreTracker scoreTracker;
    int pOnePoints;
    int pTwoPoints;

    private void Awake()
    {
        playMaker = FindObjectOfType<PlayMaker>();
        scoreTracker = FindObjectOfType<ScoreTracker>();
    }
    private void Start()
    {
        
    }

    public void ChangeRound()
    {
        playMaker.ResetPlayerBooleans();
        playMaker.ResetButtons();
        playMaker.ResetPlays();
        pOnePoints = scoreTracker.GetPlayerOnePoints();
        pTwoPoints = scoreTracker.GetPlayerTwoPoints();
        
        if (pOnePoints >= 3)
        {
            Debug.Log("Player One Wins");
            playMaker.SetPOneButtons(false);
            playMaker.SetPTwoButtons(false);

        }
        else if (pTwoPoints >= 3)
        {
            Debug.Log("Player Two Wins");
            playMaker.SetPOneButtons(false);
            playMaker.SetPTwoButtons(false);
        }
    }
}
