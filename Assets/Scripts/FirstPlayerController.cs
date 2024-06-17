using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayerController : MonoBehaviour
{
    public GameObject a; // 상체에 해당하는 GameObject
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float maxVerticalAngle = 100f; // 플레이어가 볼 수 있는 최대 상단 각도
    public float minVerticalAngle = -80f; // 플레이어가 볼 수 있는 최대 하단 각도

    private CharacterController controller; // CharacterController 참조
    private float verticalRotation = 0f; // 수직 회전 각도

    void Start()
    {
        // CharacterController 컴포넌트 참조 가져오기
        controller = GetComponent<CharacterController>();

        // 초기화할 때 충돌 감지 활성화
        controller.detectCollisions = true;
    }

    void Update()
    {
        // 수평 회전
        float horizontalInput = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

        // 수직 회전
        float verticalInput = -Input.GetAxis("Mouse Y");
        verticalRotation += verticalInput * rotationSpeed * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);

        // 상체(a)만 회전
        if (a != null)
        {
            // 현재 수평 회전 각도 가져오기
            float currentYaw = transform.localEulerAngles.y;

            // z축을 0으로 고정한 새로운 회전 적용
            a.transform.localRotation = Quaternion.Euler(verticalRotation, currentYaw, 0.0f);
        }

        // 이동
        float forwardInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * forwardInput * moveSpeed * Time.deltaTime;
        controller.Move(moveDirection);
    }
}
