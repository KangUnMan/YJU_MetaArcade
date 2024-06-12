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

public class GameController : MonoBehaviour
{
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
                if (count < maxCount)
                {
                    UpdateCounterText();
                    count++;
                    UpdateCounterText();
                    
                }
                if (count == 31)
                {
                    
                    
                    targetScript.enabled = false;
                    Gun.SetActive(false);
                    
                    Popup.SetActive(true);
                    /*stopwatchText.text = "00:00:00";
                    counterText.text = count + "0/30";*/
                }
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Reset();
            UpdateCounterText();
            Gun.SetActive(true);
            StartCoroutine(StartGameAfterDelay(5.0f));
            BtnActive = true;
            b.SetActive(true);
            StartStopwatch();
            count = 0;

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
    
    
    public void pausegame()
    {
        Time.timeScale = 0;
        
    }
    public void resumegame()
    {
        Time.timeScale = 1;
    }
   
    
    public void IncreaseScore(int value)
    {
        score += value;
        UnityEngine.Debug.Log(score);
        scoretext();
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

    // �����ġ ���� �޼���
    void StopStopwatch()
    {
        isStopwatchRunning = false;
    }

    // �����ġ ���� �޼���
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
}