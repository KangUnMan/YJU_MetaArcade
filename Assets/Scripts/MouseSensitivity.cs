using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseSensitivity : MonoBehaviour
{
    public Slider sensitivitySlider; // �����̴� UI
    public TextMeshProUGUI sensitivityText; // �ؽ�Ʈ UI
    private float sensitivity = 1.0f; // �ʱ� ���� ��

    private Transform playerBody; // �÷��̾��� ��ü Transform
    private float xRotation = 0f; // ī�޶��� x�� ȸ�� ��

    void Start()
    {
        // �����̴� �ʱ� ����
        sensitivitySlider.minValue = 0.1f;
        sensitivitySlider.maxValue = 10.0f;
        sensitivitySlider.value = sensitivity;

        // �����̴� ���� ����� �� ȣ��� �޼��� ���
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);

        // �ʱ� ���� �� ����
        UpdateSensitivityText();

        // �÷��̾��� ��ü Transform ��������
        playerBody = transform.parent; // �θ� ������Ʈ�� ����, �����ϰ� ���� �ʿ�

        // ���콺 Ŀ�� �����
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ���콺 �������� ������ ���� ����
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // ī�޶��� x�� ȸ���� y�� �Է°����� ����
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // ī�޶�� �÷��̾��� ȸ�� ����
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void OnSensitivityChanged(float value)
    {
        sensitivity = value;
        UpdateSensitivityText();
    }

    void UpdateSensitivityText()
    {
        sensitivityText.text = $"Sensitivity: {sensitivity:F1}";
    }
}
