using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class GameController1 : MonoBehaviour
{
    private bool isFKeyPressed = false;
    private bool isCursorOverObject = false;
    public Text CountDownText;
    public GameObject Gun;
    private RandomObjectDisplay _randomObjectDisplay;
    public TextMeshProUGUI a;

    private void Start()
    {
        _randomObjectDisplay = GetComponent<RandomObjectDisplay>();
        _randomObjectDisplay.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (isFKeyPressed)
            {
                Gun.SetActive(true);
                StartCoroutine(StartGameAfterDelay(5.0f));
            }
            
        }

        else
        {
            Gun.SetActive(false);
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
        _randomObjectDisplay.enabled=true;
    }
    void OnMouseEnter()
    {
        isCursorOverObject = true;
    }

    // 커서가 오브젝트를 벗어날 때 호출되는 함수
    void OnMouseExit()
    {
        isCursorOverObject = false;
    }
    void OnGUI()
    {
        Event e = Event.current;
        if (isCursorOverObject && e.isKey && e.keyCode == KeyCode.F)
        {
            isFKeyPressed = true;
        }
    }
}
