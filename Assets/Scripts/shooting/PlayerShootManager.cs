using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

public class PlayershootManager : MonoBehaviour
{
    public Transform firePoint; // 총알이 발사되는 위치
    public GameObject bulletPrefab; // 총알 프리팹
    public float bulletForce = 20f; // 총알 발사 속도
    private bool isRightClick = false;
    public ParticleSystem muzzleFlash;
    void Update()
    {
        if(Input.GetButtonDown("Fire2")) {
            isRightClick = true;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            isRightClick=false;
        }
        
        if(isRightClick)
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                
            }
        
        

    }

    [PunRPC]
    void Shoot()
    {
        Vector3 spawnPosition = firePoint.position + new Vector3(0, -0.1f, 0);
        // 총알을 생성하고 발사 위치에 배치
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, spawnPosition, firePoint.rotation);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            // Rigidbody 컴포넌트가 있으면 총알에 힘을 가해 움직이게 함
            bulletRigidbody.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("총알에 Rigidbody가 없습니다.");
        }
    }


}