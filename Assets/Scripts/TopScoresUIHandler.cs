using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text;

public class TopScoresUIHandler : MonoBehaviour
{
    public TMP_Text scoresListText;

    // Start is called before the first frame update
    void Start()
    {
        // render list of top scores
        SaveData data = DataManager.Instance.LoadScores();
        data.scores.Sort((x, y) => x.score.CompareTo(y.score));
        data.scores.Reverse();
        int count = 0;
        scoresListText.text = "";
        foreach (PlayerData pd in data.scores)
        {
            scoresListText.text += pd.name + " : " + pd.score + "\n";
            count++;
            if (count >= 5) break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
