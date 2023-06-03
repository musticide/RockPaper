using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] Button hostButton;
    [SerializeField] Button clientButton;
    [SerializeField] Button startGameButton;
    public bool gameHasHost = false;
    public bool gameHasClient = false;

    private void Awake()
    {
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            gameHasHost = true;
        });

        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            gameHasClient = true;
        });
        startGameButton.onClick.AddListener(() => 
        {
            if(gameHasHost || gameHasClient)
            {
                NetworkManager.Singleton.SceneManager.LoadScene("L_RockPaperScissors", UnityEngine.SceneManagement.LoadSceneMode.Single);
            }
        });
    }
}
