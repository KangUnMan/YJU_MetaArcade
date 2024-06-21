using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPanel;
    public Button resumeButton;

    void Start()
    {
        // 팝업 창 비활성화 및 버튼 클릭 이벤트 설정
        popupPanel.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        // ESC 키를 눌렀을 때 팝업 창을 활성화하고 게임을 일시 정지
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
        Time.timeScale = 0; // 게임 일시 정지
        Cursor.visible = true; // 커서 보이기
        Cursor.lockState = CursorLockMode.None;
    }

    void ResumeGame()
    {
        popupPanel.SetActive(false);
        Time.timeScale = 1; // 게임 재개
        Cursor.visible = false; // 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked; // 커서 잠금
    }
}