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
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void UpdateScore()
    {
        pOnePointsText.text = "Player One Score: " + playerOnePoints;
        pTwoPointsText.text = "Player Two Score: " + playerTwoPoints;
        //Debug.Log("POne Score" + playerOnePoints);
        //Debug.Log("pTwo Score" + playerTwoPoints);
    }
    public void CalculateScore(bool pointsToPOne, bool isDraw)
    {
        if (pointsToPOne && !isDraw)
        {
            playerOnePoints++;
            Debug.Log(playerOnePoints);
        }
        else if (!pointsToPOne && !isDraw)
        {
            playerTwoPoints++;
            Debug.Log(playerTwoPoints);
        }
        UpdateScore();
    }

    public int GetPlayerOnePoints()
    {
        return playerOnePoints;
    }

    public int GetPlayerTwoPoints()
    {
        return playerTwoPoints;
    }


}
