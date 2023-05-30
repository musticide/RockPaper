using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class GameUI : NetworkBehaviour
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
        scoreTracker.playerOnePoints.OnValueChanged += delegate { pOnePointsText.text = "Player One Score: " + scoreTracker.playerOnePoints.Value; };
        scoreTracker.playerTwoPoints.OnValueChanged += delegate { pTwoPointsText.text = "Player Two Score: " + scoreTracker.playerTwoPoints.Value; };
    }*/

    public Button GetButton(int i)
    {
        return playerButtons[i];
    }
    public void SetPlayerButtons(bool state)
    {
        for(int i = 0; i < playerButtons.Length; i++)
        {
            playerButtons[i].interactable = state;
        }
    }
}
