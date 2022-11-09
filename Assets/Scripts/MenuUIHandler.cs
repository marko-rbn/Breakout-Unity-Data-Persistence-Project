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

    private void Start()
    {
        nameWarning.gameObject.SetActive(false);
        //TODO: load high scores file, display maximum score
        //TODO: display current player's top score
        //TODO: display current player's latest score
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
