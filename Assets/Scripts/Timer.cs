using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeRemaining = 600f; // 10 minutes in seconds
    private bool timerIsRunning = false;

    void Start()
    {
        timerIsRunning = true;
        UpdateTimerText();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                OnTimerEnd();
            }
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerEnd()
    {
        // 타이머가 끝났을 때 수행할 작업을 여기에 추가
        Debug.Log("Timer has ended!");
    }
}