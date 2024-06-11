using UnityEngine;

public class PlayerShootting : MonoBehaviour
{
    public Transform firePoint; // �Ѿ��� �߻�Ǵ� ��ġ
    public GameObject bulletPrefab; // �Ѿ� ������
    public float bulletForce = 20f; // �Ѿ� �߻� �ӵ�

    void Update()
    {
        // ���콺 ���� ��ư�� ������ �߻�
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // �Ѿ��� �����ϰ� �ѱ��� ��ġ�� ��ġ
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // �ѱ��� ������ �������� �Ѿ��� �߻� ������ ����
        Vector3 shootDirection = firePoint.forward;

        // �Ѿ��� ���⿡ ������ ������ ���� ���Ͽ� �Ѿ��� �ణ �־������� ��
        float deviation = Random.Range(-0.01f, 0.01f);
        shootDirection += firePoint.right * deviation;

        // �߻� �ӵ��� ���⿡ ���� �Ѿ��� ������Ŵ
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            // Rigidbody ������Ʈ�� ������ �Ѿ˿� ���� ���� �����̰� ��
            bulletRigidbody.AddForce(shootDirection * bulletForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("�Ѿ˿� Rigidbody�� �����ϴ�.");
        }
    }
}