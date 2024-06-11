using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    public float gravityDelay = 2f; // �߷��� ����Ǳ������ ���� �ð�
    public float bulletLifetime = 5f; // �Ѿ��� ������������ �ð�
    public float gravityScale = 1f; // �߷� ������ ����
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // �Ѿ��� �߻�� �� ���� �ð��� ���� �Ŀ� �߷��� �����ϱ� ���� �ڷ�ƾ ����
        StartCoroutine(EnableGravityAfterDelay());
    }

    IEnumerator EnableGravityAfterDelay()
    {
        // �߷��� ����Ǳ������ ���� �ð���ŭ ���
        yield return new WaitForSeconds(gravityDelay);

        // Rigidbody�� �߷� ������ ����
        rb.velocity += Physics.gravity * gravityScale;

        // ���� �ð��� ���� �� �Ѿ��� �ı�
        Destroy(gameObject, bulletLifetime - gravityDelay);
    }
}
