using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScreenUI : MonoBehaviour
{
    [Header("Buttons")]
    //[SerializeField] Button startButton;
    [SerializeField] Button quitButton;
    NetworkUI networkUI;

    private void Awake()
    {
        /*startButton.onClick.AddListener(() => { StartRockPaperScissors(); });
        networkUI = FindObjectOfType<NetworkUI>();*/
    }

    void StartRockPaperScissors()
    {
        if (networkUI.gameHasClient || networkUI.gameHasHost)
        {
            SceneManager.LoadScene("L_RockPaperScissors");
        }
        else
        {
            Debug.Log("Game has no Host or Client");
        }
    }
}
