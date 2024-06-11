using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviourPunCallbacks
{
    public Transform aimPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    public ParticleSystem muzzleFlash;

    void Update()
    {
        if (photonView.IsMine && Input.GetButtonDown("Fire1")) // 로컬 플레이어만 발사할 수 있도록
        {
            photonView.RPC("ShootBullet", RpcTarget.AllViaServer, aimPoint.position);
        }
    }

    [PunRPC]
    void ShootBullet(Vector3 aimPointPosition)
    {
        Vector3 direction = (aimPointPosition - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        StartCoroutine(ApplyForceAfterDelay(bullet, aimPointPosition));
        muzzleFlash.Play();
    }

    IEnumerator ApplyForceAfterDelay(GameObject bullet, Vector3 aimPointPosition)
    {
        // 일정 시간 대기
        yield return new WaitForSeconds(0.05f);

        // 총알에 힘을 가함
        Vector3 direction = (aimPointPosition - bullet.transform.position).normalized;
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.velocity = direction * bulletSpeed;
    }
}