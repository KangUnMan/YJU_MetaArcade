using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class ScoreBoardController : MonoBehaviourPunCallbacks
{
    public AudioSource audioSource; // AudioSource�� ������ ����
    public AudioClip shootSound;
    public ScoreManager scoreManager;
    public Leaderboard leaderboard;
    public Text scoreText;
    public GameObject Popup;
    public Text counterText; // UI Text
    public Text CountDownText;
    public TextMeshProUGUI a;
    public GameObject b;
    public Text stopwatchText; // �����ġ �ð� ǥ�ÿ� UI Text
    public GameObject but;
    private int score; // ���� ����
    public RandomObjectDisplay randomObjectDisplay; // RandomObjectDisplay ����

    [SerializeField] Button _saveBtn;
    [SerializeField] Button _exitBtn;

    private int _score;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        _saveBtn.onClick.AddListener(SendMyScore);
        _saveBtn.onClick.AddListener(popupExit);
        _exitBtn.onClick.AddListener(popupExit);
    }

    public void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    public void ShowPopup()
    {
        if (photonView.IsMine) // ���� �÷��̾����� Ȯ��
        {
            Popup.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ResetScore()
    {
        UpdateScoreText();
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    public void IncreaseScore(int value)
    {
        score += value;
        UpdateScoreText(); // ���� ���� �� UI ������Ʈ
        Debug.Log("Score: " + score);
    }

    public void UpdateCounterText(int count)
    {
        counterText.text = count + "/30";
        if (count >= 30)
        {
            counterText.text = "30/30";
        }
    }

    public void UpdateScoreText()
    {
        _score = score;
        scoreText.text = "����� ������: " + score + "��";
        Debug.Log("UpdateScoreText called. Score: " + score);
    }

    public void UpdateStopwatchText(float elapsedTime)
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100F) % 100F);
        stopwatchText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public void ActivateButton(bool isActive)
    {
        b.SetActive(isActive);
    }

    public void StartGame()
    {
        if (randomObjectDisplay != null)
        {
            randomObjectDisplay.SetActive(true);
        }
        else
        {
            Debug.LogError("RandomObjectDisplay ������Ʈ�� ã�� �� �����ϴ�!");
        }
    }

    public void EndGame()
    {
        if (randomObjectDisplay != null)
        {
            randomObjectDisplay.SetActive(false);
        }
        else
        {
            Debug.LogError("RandomObjectDisplay ������Ʈ�� ã�� �� �����ϴ�!");
        }
    }

    public void ResetScoreText()
    {
        scoreText.text = "����� ������: 0��";
    }

    public void popupExit()
    {
        if (photonView.IsMine) // ���� �÷��̾����� Ȯ��
        {
            Popup.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void pausegame()
    {
        Time.timeScale = 0;
    }

    public void resumegame()
    {
        Time.timeScale = 1;
    }

    public void SendMyScore()
    {
        WebServerManager.Instance.SetScore(_score);
    }
}
