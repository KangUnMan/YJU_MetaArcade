using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text CountDownText;
    public GameObject Gun;
    private RandomObjectDisplay _randomObjectDisplay;

    private void Start()
    {
        _randomObjectDisplay = GetComponent<RandomObjectDisplay>();
        _randomObjectDisplay.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Gun.SetActive(true);
            StartCoroutine(StartGameAfterDelay(5.0f));
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
        _randomObjectDisplay.enabled=true;
    }
}