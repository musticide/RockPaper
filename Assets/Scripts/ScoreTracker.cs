using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    int playerOnePoints = 0;
    int playerTwoPoints = 0;
    [SerializeField] TextMeshProUGUI pOnePointsText;
    [SerializeField] TextMeshProUGUI pTwoPointsText;
    PlayMaker playMaker;
    GameManager gameManager;

    private void Awake()
    {
        playMaker = FindObjectOfType<PlayMaker>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void UpdateScore()
    {
        pOnePointsText.text = "POne Score: " + playerOnePoints;
        pTwoPointsText.text = "PTwo Score: " + playerTwoPoints;
        //Debug.Log("POne Score" + playerOnePoints);
        //Debug.Log("pTwo Score" + playerTwoPoints);
    }
    public void CalculateScore(bool pointsToPOne, bool isDraw)
    {
        if (pointsToPOne && !isDraw)
        {
            playerOnePoints++;
        }
        else if (!pointsToPOne && !isDraw)
        {
            playerTwoPoints++;
        }
        UpdateScore();
        //Debug.Log("calculateScoreWasCalled");
        gameManager.ChangeRound();
    }

    public int GetPlayerOnePoints()
    {
        return playerOnePoints;
    }

    public int GetPlayerTwoPoints()
    {
        return playerTwoPoints;
    }

    /*void ChangeRound() NOW HAPPENS IN GAME MANAGER
    {
        playMaker.ResetPlayerBooleans();
        playMaker.ResetButtons();
        playMaker.ResetPlays();
    }*/
}
