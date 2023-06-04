using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [Header("Buttons")]

    [SerializeField] Button[] playerButtons = new Button[3];    
    [SerializeField] Button playAgainButton;
    [SerializeField] Button pauseButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button restartGameButton;
    [SerializeField] Button exitToMainMenuButton;
    [SerializeField] Button leaveGameButton;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI pOnePointsText;
    [SerializeField] TextMeshProUGUI pTwoPointsText;
    [SerializeField] TextMeshProUGUI winnerText;

    [Header("Canvases")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject playScreen;
    [SerializeField] GameObject endScreen;

    ScoreTracker scoreTracker;
    Player player;

    private void Awake()
    {
        scoreTracker = FindObjectOfType<ScoreTracker>();
        playScreen.SetActive(true);
        pauseMenu.SetActive(false);
        endScreen.SetActive(false);
        Initialize();
    }    
    void Initialize()
    {
        playAgainButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.SceneManager.LoadScene("L_RockPaperScissors", UnityEngine.SceneManagement.LoadSceneMode.Single);
        });
        restartGameButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.SceneManager.LoadScene("L_RockPaperScissors", UnityEngine.SceneManagement.LoadSceneMode.Single);
        });
        pauseButton.onClick.AddListener(() => 
        {
            PauseGame();
        });
        resumeButton.onClick.AddListener(() =>
        {
            ResumeGame();
        });
        scoreTracker.GameOverEvent.AddListener(() =>
        {
            EndGame();
        });
        leaveGameButton.onClick.AddListener(() =>
        {
            LeaveGame();
        });
        exitToMainMenuButton.onClick.AddListener(() =>
        {
            LeaveGame();
        });
    }

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

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        playScreen.SetActive(false);
        if (!NetworkManager.Singleton.IsHost)
        {
            restartGameButton.interactable = false;
        }
    }
    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        playScreen.SetActive(true);
    }
    public void EndGame()
    {
        pauseMenu.SetActive(false);
        playScreen.SetActive(false);
        endScreen.SetActive(true);
        if (!NetworkManager.Singleton.IsHost)
        {
            playAgainButton.interactable = false;
        }
    }

    public void SetWinnerText(bool didPlayerOneWin)
    {
        if (didPlayerOneWin)
        {
            winnerText.text = "Player One Wins";
        }
        else
        {
            winnerText.text = "Player Two Wins";
        }
    }

    void LeaveGame()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("L_StartScreen", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
        else
        {
            NetworkManager.Singleton.Shutdown();
            SceneManager.LoadScene("L_StartScreen");
        }
    }
}