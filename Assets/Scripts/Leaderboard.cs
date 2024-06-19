using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public ScoreManager scoreManager;
    public GameObject scoreEntryPrefab;
    public Transform scoreContainer;

    private void Start()
    {
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        // Clear existing entries
        foreach (Transform child in scoreContainer)
        {
            Destroy(child.gameObject);
        }

        // Get and sort scores
        List<ScoreManager.ScoreEntry> scores = scoreManager.GetScores();
        scores.Sort((a, b) => b.score.CompareTo(a.score));

        // Display sorted scores
        foreach (ScoreManager.ScoreEntry scoreEntry in scores)
        {
            GameObject scoreObj = Instantiate(scoreEntryPrefab, scoreContainer);
            scoreObj.GetComponent<Text>().text = $"{scoreEntry.playerName}: {scoreEntry.score}";
        }
    }
}
