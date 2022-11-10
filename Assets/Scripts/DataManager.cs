using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager Instance;

    public string playerName;  //preserve player's name
    public int lastScore = 0;  //preserve player's last score

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
    }

    public void SaveScore(int score)
    {
        // load scores
        SaveData data = LoadScores();
        // overwrite or add score for current player name
        data.AddOrUpdate(playerName, score);
        // save data
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }

    public SaveData LoadScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            return data;
        } else
        {
            //return an empty SaveData object if no file exists
            return new SaveData();
        }
    }

}


[System.Serializable]
public class SaveData
{
    public List<PlayerData> scores;

    // add or update a score associated with name
    public void AddOrUpdate(string name, int score)
    {
        int index = scores.FindIndex(x => x.name == name);
        if (index == -1)
        {
            // name not found - add a new item to the list
            scores.Add(new PlayerData() { name = name, score = score });
        }
        else
        {
            // name found - add new score, only if score is better than recorded
            if (scores[index].score < score) { scores[index].score = score; }
        }
    }

    public int GetScoreForPlayer(string name)
    {
        int index = scores.FindIndex(x => x.name == name);
        if (index == -1)
        {
            return 0;
        } else
        {
            return scores[index].score;
        }
    }

    //returns top score player data record or null if no scores yet recorded
    public PlayerData GetTopScore()
    {
        PlayerData top = null;
        foreach (PlayerData pd in scores)
        {
            if (top == null || pd.score > top.score) top = pd;
        }
        return top;
    }
}

[System.Serializable]
public class PlayerData
{
    public string name;
    public int score;
}
