using UnityEngine;

public class MachineEvent : MonoBehaviour
{
    public GameObject popup; // �˾�â ������Ʈ�� �Ҵ��ϱ� ���� ����
    private bool isCursorOverObject = false; // Ŀ���� ������Ʈ ���� �ִ��� ���θ� �Ǵ��ϱ� ���� ����
    private bool isFKeyPressed = false; // 'f'Ű�� ���ȴ��� ���θ� �Ǵ��ϱ� ���� ����
    private float popupDelay = 3f; // �˾��� ��Ÿ���� ���� �ð�

    private float timer = 0f; // �ð��� ����ϱ� ���� Ÿ�̸� ����

    void Update()
    {
        // 'f'Ű�� ���Ȱ� �˾��� �������� ������ �˾��� ���ϴ�.
        if (isFKeyPressed && !popup.activeSelf)
        {
            popup.SetActive(true);
        }

        // �˾��� �����ִ� ���ȿ��� �ð��� ����մϴ�.
        if (popup.activeSelf)
        {
            timer += Time.deltaTime;

            // ������ ���� �ð��� ������ �˾��� �ݽ��ϴ�.
            if (timer >= popupDelay)
            {
                popup.SetActive(false);
                timer = 0f; // Ÿ�̸� �ʱ�ȭ
                isFKeyPressed = false; // 'f'Ű ���� �ʱ�ȭ
            }
        }
    }

    // Ŀ���� ������Ʈ ���� �ִ� ���� ȣ��Ǵ� �Լ�
    void OnMouseEnter()
    {
        isCursorOverObject = true;
    }

    // Ŀ���� ������Ʈ�� ��� �� ȣ��Ǵ� �Լ�
    void OnMouseExit()
    {
        isCursorOverObject = false;
    }

    // Ű �Է� ����
    void OnGUI()
    {
        Event e = Event.current;
        if (isCursorOverObject && e.isKey && e.keyCode == KeyCode.F)
        {
            isFKeyPressed = true;
        }
    }
}