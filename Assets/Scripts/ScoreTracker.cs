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

    public void UpdateScore()
    {
        pOnePointsText.text = "POne Score: " + playerOnePoints;
        pTwoPointsText.text = "PTwo Score: " + playerTwoPoints;
    }
    public void CalculateScore(bool isPlayerOne, bool isDraw)
    {
        if (isPlayerOne && !isDraw)
        {
            playerOnePoints++;
        }
        else if (!isPlayerOne && !isDraw)
        {
            playerTwoPoints++;
        }
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
