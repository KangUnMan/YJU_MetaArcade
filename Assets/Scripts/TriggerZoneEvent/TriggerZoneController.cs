using System.Collections;
using UnityEngine;
using Photon.Pun;

public class TriggerZoneController : MonoBehaviourPunCallbacks
{
    public FirstPlayerController targetScript2;
    public PlayershootManager targetScript;
    public static TriggerZoneController Instance; // �̱��� �ν��Ͻ�
    private int fix = 0;
    private int count = 0; // ī���� ����
    private int maxCount = 31; // �ִ� ī��Ʈ
    private bool isStopwatchRunning = false; // �����ġ ���� ����
    private float elapsedTime = 0f; // ��� �ð�

    private ScoreBoardController playerScoreBoardController; // Ʈ���� ���� ���� �÷��̾��� ScoreBoardController

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (playerScoreBoardController == null)
        {
            Debug.LogWarning("PlayerScoreBoardController is null");
            return;
        }

        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
        {
            if (fix == 0)
            {
                if (count < maxCount)
                {
                    playerScoreBoardController.UpdateCounterText(count);
                    count++;
                    playerScoreBoardController.UpdateCounterText(count);
                    playerScoreBoardController.PlayShootSound();
                }
                if (count == 31)
                {
                    count = 0;
                    targetScript.enabled = false;
                    playerScoreBoardController.ShowPopup();
                    StopStopwatch();
                    playerScoreBoardController.EndGame();
                    fix = 1;
                }
            }
            if (fix == 1)
            {
                playerScoreBoardController.ResetScore();
            }
        }

        if (isStopwatchRunning)
        {
            elapsedTime += Time.deltaTime;
            playerScoreBoardController.UpdateStopwatchText(elapsedTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PhotonView photonView = other.GetComponent<PhotonView>();

            if (photonView.IsMine) // ���� �÷��̾����� Ȯ��
            {
                PlayershootManager playerShootManager = other.GetComponent<PlayershootManager>();
                targetScript = playerShootManager;

                // Ʈ���� ���� ���� �÷��̾��� ScoreBoardController ��������
                playerScoreBoardController = other.GetComponentInChildren<ScoreBoardController>();
                if (playerScoreBoardController == null)
                {
                    Debug.LogError("ScoreBoardController not found on the player");
                    return;
                }

                playerScoreBoardController.ResetScoreText();
                Reset();
                playerScoreBoardController.UpdateCounterText(count);
                StartCoroutine(StartGameAfterDelay(5.0f));
                playerScoreBoardController.ActivateButton(true);
                StartStopwatch();
                fix = 0;
                playerScoreBoardController.ResetScore();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PhotonView photonView = other.GetComponent<PhotonView>();

            if (photonView.IsMine) // ���� �÷��̾����� Ȯ��
            {
                if (playerScoreBoardController == null)
                {
                    Debug.LogError("ScoreBoardController is null when exiting the trigger");
                    return;
                }

                playerScoreBoardController.ActivateButton(false);
                ResetStopwatch();
                StopStopwatch();
                Reset();
                targetScript.enabled = true;
                targetScript2.enabled = true;
                Debug.Log("Resetting score...");
                playerScoreBoardController.ResetScore();
                Debug.Log("Score after reset: " + playerScoreBoardController.GetScore());
                count = 0;
            }
        }
    }

    private IEnumerator StartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (playerScoreBoardController != null)
        {
            playerScoreBoardController.StartGame();
        }
    }

    private void StartStopwatch()
    {
        isStopwatchRunning = true;
    }

    private void StopStopwatch()
    {
        isStopwatchRunning = false;
    }

    private void ResetStopwatch()
    {
        elapsedTime = 0f;
        if (playerScoreBoardController != null)
        {
            playerScoreBoardController.UpdateStopwatchText(elapsedTime);
        }
    }

    private void Reset()
    {
        count = 0;
    }
}
