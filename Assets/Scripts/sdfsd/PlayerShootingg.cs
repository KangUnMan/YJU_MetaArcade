using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingg : MonoBehaviourPun
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform firePoint; // 발사 위치

    public float bulletForce = 20f; // 총알 발사 속도

    void Update()
    {
        if (photonView.IsMine && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 총알 인스턴스화
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position, firePoint.rotation);

        // 총알에 힘을 가해 발사
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
    }
}