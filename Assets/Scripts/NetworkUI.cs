using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [Header ("Buttons")]
    [SerializeField] Button hostButton;
    [SerializeField] Button clientButton;
    [SerializeField] Button joinPageButton;
    [SerializeField] Button startGameButton;
    [SerializeField] Button hostMainMenuButton;
    [SerializeField] Button clientMainMenuButton;
    [SerializeField] Button exitGameButton;

    [Header("Text")]
    [SerializeField] TMP_InputField joinCodeField;
    [SerializeField] TextMeshProUGUI joinCodeText;
    [SerializeField] TextMeshProUGUI clientHasJoinedMsg;

    [Header("Canvases")]
    [SerializeField] Canvas mainMenuCanvas;
    [SerializeField] Canvas joinGameCanvas;
    [SerializeField] Canvas startGameCanvas;

    bool gameIsHosted = false;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn) 
        {            
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        MainMenu();

        //on Click listeners
        hostButton.onClick.AddListener(() =>
        {
            StartHostRelay();
        });

        clientButton.onClick.AddListener(() =>
        {
            StartClientRelay(joinCodeField.text);
        });

        joinPageButton.onClick.AddListener(() =>
        {
            ClientPage();
        });

        startGameButton.onClick.AddListener(() => 
        {
            Player[] playerArray = FindObjectsOfType<Player>();
            if(playerArray.Length > 1)
            {
                NetworkManager.Singleton.SceneManager.LoadScene("L_RockPaperScissors", UnityEngine.SceneManagement.LoadSceneMode.Single);
                startGameButton.interactable = false;
            }  
        });

        hostMainMenuButton.onClick.AddListener(() =>
        {
            MainMenu();
        });
        
        clientMainMenuButton.onClick.AddListener(() =>
        {
            MainMenu();
        });
        exitGameButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        
        NetworkManager.Singleton.OnClientConnectedCallback += delegate { ClientConnected(); } ;
        NetworkManager.Singleton.OnClientDisconnectCallback += delegate { ClientDisconnected(); };
    }



    async void StartHostRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);
            joinCodeText.text = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartHost();
            HostPage();
        } catch(RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    async void StartClientRelay(string joinCode)
    {
        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);            
            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartClient();            
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    void HostPage()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        startGameCanvas.gameObject.SetActive(true);
        clientHasJoinedMsg.alpha = 0;
        startGameButton.interactable = false;
    }

    void ClientPage()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        joinGameCanvas.gameObject.SetActive(true);
    }

    void MainMenu()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        joinGameCanvas.gameObject.SetActive(false);
        startGameCanvas.gameObject.SetActive(false);
        if (!NetworkManager.Singleton.isActiveAndEnabled) return;
        NetworkManager.Singleton.Shutdown();
        gameIsHosted = false;
    }
    void ClientConnected()
    {
        Debug.Log("Client Connected");
        if (!gameIsHosted)
        {
            gameIsHosted = true;
        }
        else
        {
            startGameButton.interactable = true;
            clientHasJoinedMsg.text = "Client Joined!";
            clientHasJoinedMsg.alpha = 1;
        }
    }

    void ClientDisconnected()
    {
        clientHasJoinedMsg.text = "Client Left";
    }
}