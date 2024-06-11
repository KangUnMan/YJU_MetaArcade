using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class GameController : MonoBehaviourPunCallbacks
{
    public Text counterText; // UI Text를 연결할 변수
    private int count = 0; // 카운터 변수
    private int maxCount = 30; // 최대 카운
    public bool BtnActive = false;
    private bool isCursorOverObject = false;
    public Text CountDownText;
    public GameObject Gun;
    private RandomObjectDisplay _randomObjectDisplay;
    public TextMeshProUGUI a;
    public GameObject b;
    public Text stopwatchText; // 스톱워치 시간을 표시할 UI Text
    private float elapsedTime = 0f; // 경과 시간
    private bool isStopwatchRunning = false; // 스톱워치 작동 여부
    public GameObject but;
    private void Start()
    {
        _randomObjectDisplay = GetComponent<RandomObjectDisplay>();
        _randomObjectDisplay.enabled = false;
    }

    private void Update()
    {
        if (isStopwatchRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateStopwatchText();
        }
        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {

            // 최대 카운트를 넘지 않도록 설정
            if (count < maxCount)
            {
                UpdateCounterText();
                count++;
                UpdateCounterText();
            }


        }
    }
    [PunRPC]
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

            //but.SetActive(false);


        }


    }
    [PunRPC]
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Gun.SetActive(false);
            BtnActive = false;
            b.SetActive(false);
            ResetStopwatch(); // 스톱워치 초기화
            StopStopwatch();
            Reset();

        }
    }

    private IEnumerator StartGameAfterDelay(float delay)
    {
        //float remainingTime = delay;
        //while (remainingTime > 0)
        //{
        //    CountDownText.text = remainingTime.ToString("F1"); // 소수점 한 자리까지 표시
        //    yield return new WaitForSeconds(0.1f); // 0.1초마다 업데이트
        //    remainingTime -= 0.1f;
        //}

        //CountDownText.text = "0";
        yield return new WaitForSeconds(delay);
        StartGame();
    }
    private void StartGame()
    {
        Debug.Log("게임이 시작됩니다.");
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

    // 스톱워치 멈춤 메서드
    void StopStopwatch()
    {
        isStopwatchRunning = false;
    }

    // 스톱워치 리셋 메서드
    void ResetStopwatch()
    {
        elapsedTime = 0f;
        UpdateStopwatchText();
    }

    // 스톱워치 텍스트 업데이트 메서드
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
    }
    private void Reset()
    {
        count = 0;
    }
    /* void OnMouseEnter()
     {
         isCursorOverObject = true;
     }

     // 커서가 오브젝트를 벗어날 때 호출되는 함수
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