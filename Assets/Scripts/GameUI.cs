using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Buttons")]

    [SerializeField] Button[] pOneButtons = new Button[3];
    [SerializeField] Button[] pTwoButtons = new Button[3];

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject playScreen;

    ScoreTracker scoreTracker;
    Player player;

    private void Awake()
    {
        /*for (int i = 0; i < pTwoButtons.Length; i++)
        {
            pTwoButtons[i].onClick.AddListener(() =>
            {
                PlayerTwoTest(i);
            });
        }*/
        scoreTracker = FindObjectOfType<ScoreTracker>();

        pTwoButtons[0].onClick.AddListener(() =>
        {
            OnPTwoClick(0);
        });
        pTwoButtons[1].onClick.AddListener(() =>
        {
            OnPTwoClick(1);
        });
        pTwoButtons[2].onClick.AddListener(() =>
        {
            OnPTwoClick(2);
        });
        pOneButtons[0].onClick.AddListener(() =>
        {
            OnPOneClick(0);
        });
        pOneButtons[1].onClick.AddListener(() =>
        {
            OnPOneClick(1);
        });
        pOneButtons[2].onClick.AddListener(() =>
        {
            OnPOneClick(2);
        });
    }

    

    public void OnPOneClick(int i)
    {
        player = FindObjectOfType<Player>();
        player.SetPlayerChose(i);
    }
    public void OnPTwoClick(int i)
    {
        player = FindObjectOfType<Player>();
        player.SetPlayerChose(i);
    }
    public void SetPauseMenu()
    {

    }

    public void SetPlayerButtons(bool state, bool forPlayerOne)
    {
        for(int i = 0; i < pOneButtons.Length; i++)
        {
            if (forPlayerOne)
            {
                pOneButtons[i].interactable = state;
            }
            else
            {
                pTwoButtons[i].interactable = state;
            }
        }
    }

}
