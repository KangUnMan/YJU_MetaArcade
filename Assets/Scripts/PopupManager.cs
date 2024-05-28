using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPanel;
    public Button resumeButton;
    public GameObject camera;

    void Start()
    {
        // �˾� â ��Ȱ��ȭ �� ��ư Ŭ�� �̺�Ʈ ����
        popupPanel.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        // ESC Ű�� ������ �� �˾� â�� Ȱ��ȭ�ϰ� ������ �Ͻ� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (popupPanel.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        popupPanel.SetActive(true);
        Time.timeScale = 0; // ���� �Ͻ� ����
        camera.SetActive(false);
        Cursor.visible = true; // Ŀ�� ���̱�
        Cursor.lockState = CursorLockMode.None;
    }

    void ResumeGame()
    {
        popupPanel.SetActive(false);
        Time.timeScale = 1; // ���� �簳
        camera.SetActive(true);
        Cursor.visible = false; // Ŀ�� �����
        Cursor.lockState = CursorLockMode.Locked; // Ŀ�� ���
    }
}