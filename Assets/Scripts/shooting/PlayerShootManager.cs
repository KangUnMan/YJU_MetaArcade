using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

public class PlayershootManager : MonoBehaviour
{
    public Transform firePoint; // �Ѿ��� �߻�Ǵ� ��ġ
    public GameObject bulletPrefab; // �Ѿ� ������
    public float bulletForce = 20f; // �Ѿ� �߻� �ӵ�
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
        // �Ѿ��� �����ϰ� �߻� ��ġ�� ��ġ
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, spawnPosition, firePoint.rotation);

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