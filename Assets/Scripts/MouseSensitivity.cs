using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseSensitivity : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityText;
    private float sensitivity = 1.0f;

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
    }

    void Update()
    {
        // ���콺 �������� ������ ���� ����
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // ���⼭ ���콺 �������� �̿��� ĳ���� ������ �Ǵ� ī�޶� ȸ���� ó���մϴ�.
        // ���� ���, ī�޶� ȸ��:
        // transform.Rotate(Vector3.up * mouseX);
        // transform.Rotate(Vector3.left * mouseY);
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