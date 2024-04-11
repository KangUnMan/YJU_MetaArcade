using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingManager : MonoBehaviour
{
    public Transform target;        // ����ٴ� Ÿ�� ������Ʈ�� Transform

    private Transform tr;                // ī�޶� �ڽ��� Transform

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        tr.position = new Vector3(target.position.x, tr.position.y + 0.8f, target.position.z - 2.17f);

        tr.LookAt(target);
    }
}
