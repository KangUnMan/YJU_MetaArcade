using UnityEngine;

public class PlayershootManager : MonoBehaviour
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
        // �Ѿ��� �����ϰ� �߻� ��ġ�� ��ġ
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // �Ѿ��� ���� ����
        //Vector3 shootDirection = (firePoint.forward + Random.insideUnitSphere * 0.05f).normalized;
        //bullet.transform.forward = shootDirection;

        // �߻� �ӵ��� ���⿡ ���� �Ѿ��� ������Ŵ
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            // Rigidbody ������Ʈ�� ������ �Ѿ˿� ���� ���� �����̰� ��
            bulletRigidbody.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("�Ѿ˿� Rigidbody�� �����ϴ�.");
        }
    }
}