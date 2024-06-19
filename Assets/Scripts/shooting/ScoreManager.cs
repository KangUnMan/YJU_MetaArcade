using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    private const string fileName = "scores.json";

    [System.Serializable]
    public class ScoreEntry
    {
        public string playerName;
        public int score;

        public ScoreEntry(string playerName, int score)
        {
            this.playerName = playerName;
            this.score = score;
        }
    }

    [System.Serializable]
    public class ScoreList
    {
        public List<ScoreEntry> scores = new List<ScoreEntry>();
    }

    private ScoreList scoreList = new ScoreList();

    private void Start()
    {
        LoadScores();
    }

    public void AddScore(string playerName, int score)
    {
        scoreList.scores.Add(new ScoreEntry(playerName, score));
        SaveScores();
    }

    private void SaveScores()
    {
        string json = JsonUtility.ToJson(scoreList, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, fileName), json);
    }

    private void LoadScores()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            scoreList = JsonUtility.FromJson<ScoreList>(json);
        }
    }

    public List<ScoreEntry> GetScores()
    {
        return scoreList.scores;
    }
}
