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
        // 슬라이더 초기 설정
        sensitivitySlider.minValue = 0.1f;
        sensitivitySlider.maxValue = 10.0f;
        sensitivitySlider.value = sensitivity;

        // 슬라이더 값이 변경될 때 호출될 메서드 등록
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);

        // 초기 감도 값 설정
        UpdateSensitivityText();
    }

    void Update()
    {
        // 마우스 움직임을 감도에 맞춰 조정
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // 여기서 마우스 움직임을 이용한 캐릭터 움직임 또는 카메라 회전을 처리합니다.
        // 예를 들어, 카메라 회전:
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