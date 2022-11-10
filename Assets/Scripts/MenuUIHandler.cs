using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    //public ColorPicker ColorPicker;
    public TMP_Text nameWarning;
    public TMP_InputField nameInput;
    public TMP_Text bestestScore;  //top score of any player
    public TMP_Text bestScore;  // best score for player
    public TMP_Text lastScore;  // last play score


    private void Start()
    {
        nameWarning.gameObject.SetActive(false);

        // set name if already known
        string playerName = "";
        playerName = DataManager.Instance.playerName ?? "";
        nameInput.text = playerName;

        // load high scores file, display maximum score
        SaveData scores = DataManager.Instance.LoadScores();
        PlayerData top = scores.GetTopScore();
        if (top == null)
        {
            bestestScore.gameObject.SetActive(false);
        }
        else
        {
            bestestScore.text = "Bestest High Score: " + top.name + " : " + top.score;
        }

        // display current player's top score
        int playerTopScore = scores.GetScoreForPlayer(playerName);
        if (playerTopScore == 0)
        {
            bestScore.gameObject.SetActive(false);
        } else
        {
            bestScore.text = "Your Best Score: " + playerTopScore;
        }

        //TODO: display current player's latest score
        if (DataManager.Instance.lastScore == 0)
        {
            lastScore.gameObject.SetActive(false);
        }
        else
        {
            lastScore.text = "Your Last Score: " + DataManager.Instance.lastScore;
        }

    }

    public void StartNew()
    {
        // if name is entered, store it in DataManager, otherwise don't proceed
        if (nameInput.text == "")
        {
            nameWarning.gameObject.SetActive(true);
        } else
        {
            // store name in DataManager and load game scene
            nameWarning.gameObject.SetActive(false);
            DataManager.Instance.playerName = nameInput.text;
            SceneManager.LoadScene(1);
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
