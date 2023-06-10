using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    
    [Header("Image")]
    [SerializeField] Sprite[] rpsSprites = new Sprite[3];
    [SerializeField] Sprite waitSprite;
    [SerializeField] Image playerOneImage;
    [SerializeField] Image playerTwoImage;

    [Header("Canvases")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject playScreen;
    [SerializeField] GameObject endScreen;

    ScoreTracker scoreTracker;    

    private void Awake()
    {
        scoreTracker = FindObjectOfType<ScoreTracker>();
        StartGameUI();
        Initialize();
    }    
    void Initialize()
    {
        playAgainButton.onClick.AddListener(() =>
        {
            scoreTracker.RestartGameMethod();
            StartGameUI();
        });
        restartGameButton.onClick.AddListener(() =>
        {
            scoreTracker.RestartGameMethod();
            StartGameUI();
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

    public void SetPlayerOnesprite(int playerChose)
    {
        playerOneImage.sprite = rpsSprites[playerChose];
    }

    public void SetPlayerTwosprite(int playerChose)
    {
        playerTwoImage.sprite = rpsSprites[playerChose];
    }

    public void ResetPlayerSprites()
    {
        playerOneImage.sprite = waitSprite;
        playerTwoImage.sprite = waitSprite;
    }

    void StartGameUI()
    {
        playScreen.SetActive(true);
        pauseMenu.SetActive(false);
        endScreen.SetActive(false);
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