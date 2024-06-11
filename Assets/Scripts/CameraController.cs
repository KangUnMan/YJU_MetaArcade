using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // ĳ������ Transform

    public Vector3 offset; // ī�޶�� ĳ���� ���� �Ÿ�

    public float rotationSpeed = 100f;

    private FirstPlayerController controller;
    private float verticalRotation;

    void LateUpdate()
    {
        controller = GetComponent<FirstPlayerController>();
        // ĳ������ ��ġ�� ���� ī�޶��� ��ġ�� ����
        transform.position = target.position + offset;

        // ĳ���͸� ���󰡸鼭 ȸ��
        float mouseX = Input.GetAxis("Mouse X");
        transform.RotateAround(target.position, Vector3.up, mouseX * rotationSpeed * Time.deltaTime);
        float mouseY = Input.GetAxis("Mouse Y");
        verticalRotation -= mouseY * rotationSpeed * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, -3f, 3f);
        transform.rotation = Quaternion.Euler(verticalRotation, target.eulerAngles.y, 0f);

    }
}