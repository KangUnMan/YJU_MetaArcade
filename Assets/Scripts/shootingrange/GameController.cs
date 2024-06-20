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
    public GameObject Gun;
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
        /*if (isMouseLocked == false)
        {
            if (Input.GetMouseButtonDown(0)) // 좌클릭 입력
            {
                // 여기에 좌클릭 입력을 처리하는 코드를 추가합니다.
            }

            if (Input.GetMouseButtonDown(1)) // 우클릭 입력
            {
                // 여기에 우클릭 입력을 처리하는 코드를 추가합니다.
            }
        }*/
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
                        //targetScript2.enabled = false;
                        targetScript.enabled = false;
                        Gun.SetActive(false);
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
            scoretext();
            Reset();
            UpdateCounterText();
            Gun.SetActive(true);
            StartCoroutine(StartGameAfterDelay(5.0f));
            BtnActive = true;
            b.SetActive(true);
            StartStopwatch();
            fix = 0;
            score = 0;
            
            
            //but.SetActive(false);


        }


    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Gun.SetActive(false);
            BtnActive = false;
            b.SetActive(false);
            ResetStopwatch(); // �����ġ �ʱ�ȭ
            StopStopwatch();
            Reset();
            targetScript.enabled = true;
            targetScript2.enabled = true;
            //bScript.ResetB();
            Debug.Log("Resetting score...");
            score = 0;
            Debug.Log("Score after reset: " + score);
            count = 0;
            

        }
    }

    private IEnumerator StartGameAfterDelay(float delay)
    {
        //float remainingTime = delay;
        //while (remainingTime > 0)
        //{
        //    CountDownText.text = remainingTime.ToString("F1"); // �Ҽ��� �� �ڸ����� ǥ��
        //    yield return new WaitForSeconds(0.1f); // 0.1�ʸ��� ������Ʈ
        //    remainingTime -= 0.1f;
        //}

        //CountDownText.text = "0";
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
    /* void OnMouseEnter()
     {
         isCursorOverObject = true;
     }

     // Ŀ���� ������Ʈ�� ��� �� ȣ��Ǵ� �Լ�
     void OnMouseExit()
     {
         isCursorOverObject = false;
     }
     /*void OnGUI()
     {
         Event e = Event.current;
         if (isCursorOverObject)
         {
             b.SetActive(true);
             if (isCursorOverObject && e.isKey && e.keyCode == KeyCode.F)
             {
                 isFKeyPressed = true;
             }
         }
         else
         {
             b.SetActive(false);
         }
     }*/

    public void SendMyScore()
    {
        WebServerManager.Instance.SetScore(score);
    }
}