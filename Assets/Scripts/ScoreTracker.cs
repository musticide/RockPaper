using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class ScoreTracker : NetworkBehaviour
{    
    //references
    private Player playerOne;
    private Player playerTwo;
    [SerializeField] TextMeshProUGUI clientIDText;
    GameUI gameUI;

    public UnityEvent ResetEvent;
    public UnityEvent GameOverEvent;
    public UnityEvent GameRestartEvent;

    //server variables
    NetworkVariable<bool> bothHavePlayed = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    NetworkVariable<bool> isDraw = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    NetworkVariable<bool> didPlayerOneWin = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    NetworkVariable<bool> gameOver = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    NetworkVariable<bool> restartGame = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    private void Awake()
    {
        Initialize();
        gameUI = FindObjectOfType<GameUI>();

    }

    void Initialize()
    {
        bothHavePlayed.OnValueChanged = delegate { Invoke("CheckPoints", 0.3f); Invoke("UpdatePlayerSprites", 0f); };
        isDraw.OnValueChanged = delegate { ResetParameters(); };
        gameOver.OnValueChanged += delegate { GameOverEvent.Invoke(); };
        gameOver.OnValueChanged += delegate { gameUI.SetWinnerText(didPlayerOneWin.Value); };
        restartGame.OnValueChanged += delegate { ResetAll(); };
    }
    public void OnClientJoin()
    {        
        Player[] playerArray;
        playerArray = FindObjectsOfType<Player>();        
        if (playerArray.Length < 2)
        {
            return;
        }
        else if (playerArray.Length == 2)
        {
            playerOne = playerArray[0];            
            playerTwo = playerArray[1];            
            playerOne.hasPlayed.OnValueChanged = delegate { Invoke("PlayerInputRecieved", 0.3f);};
            playerOne.points.OnValueChanged = delegate { UpdateScore(); };
            playerTwo.hasPlayed.OnValueChanged = delegate { Invoke("PlayerInputRecieved", 0.3f); };
            playerTwo.points.OnValueChanged = delegate { UpdateScore(); };
            if (!IsServer) return;
            playerOne.points.Value = 0;
            playerTwo.points.Value = 0;
        }       
    }
    void PlayerInputRecieved()
    {        
        if (playerOne.hasPlayed.Value && playerTwo.hasPlayed.Value)
        {
            if (IsServer) { 
            //Debug.Log("Both players have played");
            bothHavePlayed.Value = true;
            }           
        }
    }

    void CheckPoints()
    {
        if (!bothHavePlayed.Value) return;
        Debug.Log("Check Points Was Called");
        switch (playerOne.playerChose.Value)
        {
            case 0:
                switch (playerTwo.playerChose.Value)
                {
                    case 0:
                        if (!IsServer) return;                        
                        isDraw.Value = true;
                        break;
                    case 1:
                        IncrementPlayerTwoPoints();
                        break;
                    case 2:
                        IncrementPlayerOnePoints();
                        break;
                }
                break;
            case 1:
                switch (playerTwo.playerChose.Value)
                {
                    case 0:
                        IncrementPlayerOnePoints();
                        break;
                    case 1:
                        if (!IsServer) return;
                        isDraw.Value = true;
                        break;
                    case 2:
                        IncrementPlayerTwoPoints();
                        break;
                }
                break;
            case 2:
                switch (playerTwo.playerChose.Value)
                {
                    case 0:
                        IncrementPlayerTwoPoints();
                        break;
                    case 1:
                        IncrementPlayerOnePoints();
                        break;
                    case 2:
                        if (!IsServer) return;
                        isDraw.Value = true;
                        break;
                }
                break;
        }
        Invoke("ResetParameters", 2f);
    }

    void UpdatePlayerSprites()
    {
        if (bothHavePlayed.Value)
        {
            gameUI.SetPlayerOnesprite(playerOne.playerChose.Value);
            gameUI.SetPlayerTwosprite(playerTwo.playerChose.Value);
        }
        else
        {
            gameUI.ResetPlayerSprites();
        }
    }
    void IncrementPlayerOnePoints()
    {
        Debug.Log("1 points was called");
        if (!IsServer) return;
        playerOne.points.Value++;
        Debug.Log("Player One Points: " + playerOne.points.Value);
    }
    void IncrementPlayerTwoPoints()
    {
        Debug.Log("2 Points was Called");
        if (!IsServer) return;
        playerTwo.points.Value++;
        Debug.Log("Player Two Points: " + playerTwo.points.Value);
    }

    void UpdateScore()
    {
        gameUI.UpdateScoreText(playerOne.points.Value, playerTwo.points.Value);
        GameOverCheck();
    }
    void ResetParameters()
    {
        ResetEvent.Invoke();
        if (!IsServer) return;
        bothHavePlayed.Value = false;
        isDraw.Value = false;        
    }

    void GameOverCheck()
    {
        if (!IsServer) return;
        if(playerOne.points.Value >= 3)
        {
            didPlayerOneWin.Value = true;
            gameOver.Value = true;  
        }
        else if (playerTwo.points.Value >= 3)
        {
            didPlayerOneWin.Value = false;
            gameOver.Value = true;
        }
    }
    
    public void ResetAll()
    {        
        if (IsServer)
        {
            isDraw.Value = false;
            bothHavePlayed.Value = false;
            playerOne.points.Value = 0;
            playerTwo.points.Value = 0;
            restartGame.Value = false;
        }
        else
        {
            if (!IsOwner) return;
            playerOne.hasPlayed.Value = false;
            playerTwo.hasPlayed.Value = false;
        }
    }
     public void RestartGameMethod()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("L_RockPaperScissors", UnityEngine.SceneManagement.LoadSceneMode.Single);
        ResetAll();
    }
}    