using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingg : MonoBehaviourPun
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform firePoint; // �߻� ��ġ

    public float bulletForce = 20f; // �Ѿ� �߻� �ӵ�

    void Update()
    {
        if (photonView.IsMine && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // �Ѿ� �ν��Ͻ�ȭ
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position, firePoint.rotation);

        // �Ѿ˿� ���� ���� �߻�
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
    }
}