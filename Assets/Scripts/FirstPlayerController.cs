using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayerController : MonoBehaviour
{
    public GameObject a; // ��ü�� �ش��ϴ� GameObject
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float maxVerticalAngle = 100f; // �÷��̾ �� �� �ִ� �ִ� ��� ����
    public float minVerticalAngle = -80f; // �÷��̾ �� �� �ִ� �ִ� �ϴ� ����

    private CharacterController controller; // CharacterController ����
    private float verticalRotation = 0f; // ���� ȸ�� ����

    void Start()
    {
        // CharacterController ������Ʈ ���� ��������
        controller = GetComponent<CharacterController>();

        // �ʱ�ȭ�� �� �浹 ���� Ȱ��ȭ
        controller.detectCollisions = true;
    }

    void Update()
    {
        // ���� ȸ��
        float horizontalInput = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

        // ���� ȸ��
        float verticalInput = -Input.GetAxis("Mouse Y");
        verticalRotation += verticalInput * rotationSpeed * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);

        // ��ü(a)�� ȸ��
        if (a != null)
        {
            // ���� ���� ȸ�� ���� ��������
            float currentYaw = transform.localEulerAngles.y;

            // z���� 0���� ������ ���ο� ȸ�� ����
            a.transform.localRotation = Quaternion.Euler(verticalRotation, currentYaw, 0.0f);
        }

        // �̵�
        float forwardInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * forwardInput * moveSpeed * Time.deltaTime;
        controller.Move(moveDirection);
    }
}
