using UnityEngine;

public class MachineEvent : MonoBehaviour
{
    public GameObject popup; // 팝업창 오브젝트를 할당하기 위한 변수
    private bool isCursorOverObject = false; // 커서가 오브젝트 위에 있는지 여부를 판단하기 위한 변수
    private bool isFKeyPressed = false; // 'f'키가 눌렸는지 여부를 판단하기 위한 변수
    private float popupDelay = 3f; // 팝업이 나타나는 지연 시간

    private float timer = 0f; // 시간을 계산하기 위한 타이머 변수

    void Update()
    {
        // 'f'키가 눌렸고 팝업이 열려있지 않으면 팝업을 엽니다.
        if (isFKeyPressed && !popup.activeSelf)
        {
            popup.SetActive(true);
        }

        // 팝업이 열려있는 동안에만 시간을 계산합니다.
        if (popup.activeSelf)
        {
            timer += Time.deltaTime;

            // 지정된 지연 시간이 지나면 팝업을 닫습니다.
            if (timer >= popupDelay)
            {
                popup.SetActive(false);
                timer = 0f; // 타이머 초기화
                isFKeyPressed = false; // 'f'키 상태 초기화
            }
        }
    }

    // 커서가 오브젝트 위에 있는 동안 호출되는 함수
    void OnMouseEnter()
    {
        isCursorOverObject = true;
    }

    // 커서가 오브젝트를 벗어날 때 호출되는 함수
    void OnMouseExit()
    {
        isCursorOverObject = false;
    }

    // 키 입력 감지
    void OnGUI()
    {
        Event e = Event.current;
        if (isCursorOverObject && e.isKey && e.keyCode == KeyCode.F)
        {
            isFKeyPressed = true;
        }
    }
}