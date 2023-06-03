using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("Buttons")]

    [SerializeField] Button[] playerButtons = new Button[3];
    [SerializeField] TextMeshProUGUI pOnePointsText;
    [SerializeField] TextMeshProUGUI pTwoPointsText;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject playScreen;

    ScoreTracker scoreTracker;
    Player player;

    private void Awake()
    {
        scoreTracker = FindObjectOfType<ScoreTracker>();
        //Initialize();
    }

    public void SetPauseMenu()
    {

    }
    /*void Initialize()
    {
        
    }*/

    public Button GetButton(int i)
    {
        return playerButtons[i];
    }
 
    public void SetButtonState(bool state)
    {
        foreach(Button button in playerButtons)
        {
            button.interactable = state;
        }
    }

    public void UpdateScoreText(int playerOnePoints, int playerTwoPoints)
    {
        pOnePointsText.text = "Player One: " + playerOnePoints;
        pTwoPointsText.text = "Player Two: " + playerTwoPoints;
    }
}
