using UnityEngine;

public class PlayerShootting : MonoBehaviour
{
    public Transform firePoint; // 총알이 발사되는 위치
    public GameObject bulletPrefab; // 총알 프리팹
    public float bulletForce = 20f; // 총알 발사 속도

    void Update()
    {
        // 마우스 왼쪽 버튼을 누르면 발사
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 총알을 생성하고 총구의 위치에 배치
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 총구의 방향을 기준으로 총알의 발사 방향을 설정
        Vector3 shootDirection = firePoint.forward;

        // 총알의 방향에 일정한 무작위 값을 더하여 총알이 약간 휘어지도록 함
        float deviation = Random.Range(-0.01f, 0.01f);
        shootDirection += firePoint.right * deviation;

        // 발사 속도와 방향에 따라 총알을 전진시킴
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            // Rigidbody 컴포넌트가 있으면 총알에 힘을 가해 움직이게 함
            bulletRigidbody.AddForce(shootDirection * bulletForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("총알에 Rigidbody가 없습니다.");
        }
    }
}