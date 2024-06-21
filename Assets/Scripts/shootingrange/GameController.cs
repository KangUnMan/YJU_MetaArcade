using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using System;
using System.Diagnostics;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using Debug = UnityEngine.Debug;

public class GameController : MonoBehaviour
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
    public Text counterText; // UI Text�� ������ ����
    private int count = 0; // ī���� ����
    private int maxCount = 31; // �ִ� ī��
    public bool BtnActive = false;
    private bool isCursorOverObject = false;
    public Text CountDownText;
    private RandomObjectDisplay _randomObjectDisplay;
    private Target _target;
    public TextMeshProUGUI a;
    public GameObject b;
    public Text stopwatchText; // �����ġ �ð��� ǥ���� UI Text
    private float elapsedTime = 0f; // ��� �ð�
    private bool isStopwatchRunning = false; // �����ġ �۵� ����
    public GameObject but;
    private bool originalCursorState;

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

        {
            if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
            {
                if(fix == 0)
                {
                    if (count < maxCount)
                    {
                        UpdateCounterText();
                        count++;
                        UpdateCounterText();
                        PlayShootSound();
                    }
                    //count를 30으로 설정하면 총알이 날아가는 도중에 팝업이 떠서 그 후에 점수 갱신이 됨, count를 31로 하면 30발을 다 쏘고 좌클릭을 한 번 더 해야 하는 불편함이 있지만 점수갱신과 팝업창은 동시에 이루어짐
                    if (count == 31)
                    {
                        
                        count = 0;
                        targetScript.enabled = false;
                        Popup.SetActive(true);
                        StopStopwatch();
                        EndGame();
                        fix = 1;


                    }
                }
                if(fix == 1)
                {
                    score = 0;
                }
               
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
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
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            BtnActive = false;
            b.SetActive(false);
            ResetStopwatch(); // �����ġ �ʱ�ȭ
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
        
        if(count < 31)
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
        scoreText.text = "당신의 점수는:"+ score+"점";
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
        Popup.SetActive(false);
        //score = 0;
        Time.timeScale = 1;
    }

    public void SendMyScore()
    {
        WebServerManager.Instance.SetScore(score);
    }
}