using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class GameController : MonoBehaviourPunCallbacks
{
    public Text counterText; // UI Text�� ������ ����
    private int count = 0; // ī���� ����
    private int maxCount = 30; // �ִ� ī��
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

            // �ִ� ī��Ʈ�� ���� �ʵ��� ����
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
            ResetStopwatch(); // �����ġ �ʱ�ȭ
            StopStopwatch();
            Reset();

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
    private void StartGame()
    {
        Debug.Log("������ ���۵˴ϴ�.");
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

    // �����ġ �ؽ�Ʈ ������Ʈ �޼���
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