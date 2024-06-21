using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class GameController : MonoBehaviourPunCallbacks
{
    //public Target bScript;
    public AudioSource audioSource; // AudioSource를 연결할 변수
    public AudioClip shootSound;
    public ScoreManager scoreManager;
    public Leaderboard leaderboard;
    public int fix = 0;
    public FirstPlayerController targetScript2;
    public PlayershootManager targetScript;
    public static GameController Instance; // 싱글톤 인스턴스
    private bool isMouseLocked = false;
    public int score; // 현재 점수
    public Text scoreText;
    public GameObject Popup;
    public Text counterText; // UI Text
    private int count = 0; // 카운터 변수
    private int maxCount = 31; // 최대 카운트
    public bool BtnActive = false;
    private bool isCursorOverObject = false;
    public Text CountDownText;
    private RandomObjectDisplay _randomObjectDisplay;
    private Target _target;
    public TextMeshProUGUI a;
    public GameObject b;
    public Text stopwatchText; // 스톱워치 시간 표시용 UI Text
    private float elapsedTime = 0f; // 경과 시간
    private bool isStopwatchRunning = false; // 스톱워치 실행 여부
    public GameObject but;
    private bool originalCursorState;
    private bool isPopupActive = false;

    [SerializeField] Button _saveBtn;

    // 게임 시작 시 인스턴스를 설정합니다.
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _randomObjectDisplay = GetComponent<RandomObjectDisplay>();
        _randomObjectDisplay.enabled = false;
        _target = GetComponent<Target>();
        audioSource = GetComponent<AudioSource>();

        _saveBtn.onClick.AddListener(SendMyScore);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            count = 30;
        }
        if (isStopwatchRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateStopwatchText();
        }

        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
            if (fix == 0)
            {
                if (count < maxCount)
                {
                    UpdateCounterText();
                    count++;
                    UpdateCounterText();
                    PlayShootSound();
                }
                if (count == 31)
                {
                    count = 0;
                    targetScript.enabled = false;
                    ShowPopup();
                    StopStopwatch();
                    EndGame();
                    fix = 1;
                }
            }
            if (fix == 1)
            {
                score = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (photonView.IsMine) // 로컬 플레이어인지 확인
            {
                PlayershootManager playerShootManager = other.GetComponent<PlayershootManager>();
                targetScript = playerShootManager;
                scoretext();
                Reset();
                UpdateCounterText();
                StartCoroutine(StartGameAfterDelay(5.0f));
                BtnActive = true;
                b.SetActive(true);
                StartStopwatch();
                fix = 0;
                score = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (photonView.IsMine) // 로컬 플레이어인지 확인
            {
                BtnActive = false;
                b.SetActive(false);
                ResetStopwatch();
                StopStopwatch();
                Reset();
                targetScript.enabled = true;
                targetScript2.enabled = true;
                Debug.Log("Resetting score...");
                score = 0;
                Debug.Log("Score after reset: " + score);
                count = 0;
            }
        }
    }

    private IEnumerator StartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartGame();
    }

    public void RecordPlayerScore(string playerName, int score)
    {
        scoreManager.AddScore(playerName, score);
        leaderboard.UpdateLeaderboard();
    }

    public void pausegame()
    {
        Time.timeScale = 0;
    }

    public void resumegame()
    {
        Time.timeScale = 1;
    }

    private void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    public void IncreaseScore(int value)
    {
        score += value;
        if (count < 31)
        {
            scoretext();
        }
        Debug.Log(count);
        Debug.Log(score);
    }

    private void StartGame()
    {
        _randomObjectDisplay.enabled = true;
    }

    private void EndGame()
    {
        _randomObjectDisplay.enabled = false;
    }

    void StartStopwatch()
    {
        isStopwatchRunning = true;
    }

    void StopStopwatch()
    {
        isStopwatchRunning = false;
    }

    void ResetStopwatch()
    {
        elapsedTime = 0f;
        UpdateStopwatchText();
    }

    void scoretext()
    {
        scoreText.text = "당신의 점수는: " + score + "점";
    }

    void UpdateStopwatchText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100F) % 100F);
        stopwatchText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    void UpdateCounterText()
    {
        counterText.text = count + "/30";
        if (count >= 30)
        {
            counterText.text = "30/30";
        }
    }

    private void Reset()
    {
        count = 0;
    }

    public void popupExit()
    {
        if (photonView.IsMine) // 로컬 플레이어인지 확인
        {
            Popup.SetActive(false);
            isPopupActive = false;
            Time.timeScale = 1;
        }
    }

    private void ShowPopup()
    {
        if (photonView.IsMine) // 로컬 플레이어인지 확인
        {
            Popup.SetActive(true);
            isPopupActive = true;
        }
    }

    public void SendMyScore()
    {
        WebServerManager.Instance.SetScore(score);
    }
}
