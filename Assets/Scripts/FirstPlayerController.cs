using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float maxVerticalAngle = 80f; // 플레이어가 볼 수 있는 최대 상단 각도
    public float minVerticalAngle = -80f; // 플레이어가 볼 수 있는 최대 하단 각도

    private float verticalRotation = 0f; // 수직 회전 각도

    private Player_Manager playerManager;

    void Update()
    {
        // 수평 회전
        float horizontalInput = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

        // 수직 회전
        float verticalInput = -Input.GetAxis("Mouse Y");
        verticalRotation += verticalInput * rotationSpeed * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);
        transform.localRotation = Quaternion.Euler(verticalRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);

        // 이동
        float forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * forwardInput * moveSpeed * Time.deltaTime);
    }
}