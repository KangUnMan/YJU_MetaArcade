using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float maxVerticalAngle = 80f; // �÷��̾ �� �� �ִ� �ִ� ��� ����
    public float minVerticalAngle = -80f; // �÷��̾ �� �� �ִ� �ִ� �ϴ� ����

    private float verticalRotation = 0f; // ���� ȸ�� ����

    private Player_Manager playerManager;

    void Update()
    {
        // ���� ȸ��
        float horizontalInput = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

        // ���� ȸ��
        float verticalInput = -Input.GetAxis("Mouse Y");
        verticalRotation += verticalInput * rotationSpeed * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);
        transform.localRotation = Quaternion.Euler(verticalRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);

        // �̵�
        float forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * forwardInput * moveSpeed * Time.deltaTime);
    }
}