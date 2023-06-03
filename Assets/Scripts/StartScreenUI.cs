using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScreenUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button quitButton;
    NetworkUI networkUI;

    private void Awake()
    {

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
