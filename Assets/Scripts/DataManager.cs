using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager Instance;

    public string playerName;  //preserve player's name and score between scenes

    private static string filePath;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        filePath = Application.persistentDataPath + "/HighScores.json";

        //TEST
        //SaveScores();
        //LoadScores();
    }

    [System.Serializable]
    class SaveData
    {
        public List<PlayerData> scoreList;
    }

    [System.Serializable]
    class PlayerData
    {
        public string name;
        public int score;
    }

    public void SaveScores()
    {
        SaveData data = new SaveData();
        data.scoreList = new() {new PlayerData { name="test name", score=10 }};  //init with dummy data
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }

    public void LoadScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
        }
    }
}
