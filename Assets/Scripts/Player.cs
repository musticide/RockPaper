using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public NetworkVariable<int> playerChose = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<bool> hasPlayed = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> points = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    GameUI gameUI;
    ScoreTracker scoreTracker;

    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
        //SceneManager.sceneLoaded += OnSceneLoaded;        
        NetworkManager.Singleton.SceneManager.OnLoadComplete += delegate { OnSceneLoaded(); };        
    }

    void OnSceneLoaded()
    {        
        if (!IsOwner) return;
        if (FindObjectOfType<GameUI>() == null) return;
        Initialize();
        points.Value = 0;
        hasPlayed.Value = false;
    }

    private void Initialize()
    {
        Debug.Log("Player Initialize was Called");
        gameUI = FindObjectOfType<GameUI>();
        scoreTracker = FindObjectOfType<ScoreTracker>();
        gameUI.GetButton(0).onClick.AddListener(() =>
        {
            SetPlayerChose(0);
        });
        gameUI.GetButton(1).onClick.AddListener(() =>
        {
            SetPlayerChose(1);
        });
        gameUI.GetButton(2).onClick.AddListener(() =>
        {
            SetPlayerChose(2);
        });
        scoreTracker.OnClientJoin();
        scoreTracker.ResetEvent.AddListener(() =>
        {
            ResetHasPlayed();
        });

    }
 
    private void SetPlayerChose(int i)
    {
        if (!IsOwner) return;
        playerChose.Value = i;
        hasPlayed.Value = true;
        gameUI.SetButtonState(false);
        //Debug.Log("player: " + OwnerClientId + "Chose: " + playerChose.Value);
    }

    void ResetHasPlayed()
    {
        if (!IsOwner) return;
        hasPlayed.Value = false;
        gameUI.SetButtonState(true);
        //Debug.Log("Resetted id: " + OwnerClientId + " : " + hasPlayed.Value);
    }
}