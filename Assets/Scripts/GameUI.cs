using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class GameUI : MonoBehaviour
{
    [Header("Buttons")]

    [SerializeField] Button[] playerButtons = new Button[3];

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject playScreen;

    ScoreTracker scoreTracker;
    Player player;

    private void Awake()
    {
        scoreTracker = FindObjectOfType<ScoreTracker>();
    }

    public void SetPauseMenu()
    {

    }

    public Button GetButton(int i)
    {
        return playerButtons[i];
    }
    public void SetPlayerButtons(bool state)
    {
        for(int i = 0; i < playerButtons.Length; i++)
        {
            playerButtons[i].interactable = state;
            /*if (forPlayerOne)
            {
                playerButtons[i].interactable = state;
            }
            else
            {
                //pTwoButtons[i].interactable = state;
            }*/
        }
    }

}
