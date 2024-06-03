using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // 캐릭터의 Transform

    public Vector3 offset; // 카메라와 캐릭터 간의 거리

    public float rotationSpeed = 100f;

    private FirstPlayerController controller;
    private float verticalRotation;

    void LateUpdate()
    {
        controller = GetComponent<FirstPlayerController>();
        // 캐릭터의 위치에 따라 카메라의 위치를 조정
        transform.position = target.position + offset;

        // 캐릭터를 따라가면서 회전
        float mouseX = Input.GetAxis("Mouse X");
        transform.RotateAround(target.position, Vector3.up, mouseX * rotationSpeed * Time.deltaTime);
        float mouseY = Input.GetAxis("Mouse Y");
        verticalRotation -= mouseY * rotationSpeed * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, -3f, 3f);
        transform.rotation = Quaternion.Euler(verticalRotation, target.eulerAngles.y, 0f);

    }
}