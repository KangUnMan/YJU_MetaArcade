using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseSensitivity : MonoBehaviour
{
    public Slider sensitivitySlider; // 슬라이더 UI
    public TextMeshProUGUI sensitivityText; // 텍스트 UI
    private float sensitivity = 1.0f; // 초기 감도 값

    private Transform playerBody; // 플레이어의 몸체 Transform
    private float xRotation = 0f; // 카메라의 x축 회전 값

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

        // 플레이어의 몸체 Transform 가져오기
        playerBody = transform.parent; // 부모 오브젝트로 가정, 적절하게 수정 필요

        // 마우스 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스 움직임을 감도에 맞춰 조정
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // 카메라의 x축 회전을 y축 입력값으로 변경
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 카메라와 플레이어의 회전 적용
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
