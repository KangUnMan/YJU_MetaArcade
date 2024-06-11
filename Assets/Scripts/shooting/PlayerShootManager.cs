using UnityEngine;

public class PlayershootManager : MonoBehaviour
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
        // 총알을 생성하고 발사 위치에 배치
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 총알의 방향 설정
        //Vector3 shootDirection = (firePoint.forward + Random.insideUnitSphere * 0.05f).normalized;
        //bullet.transform.forward = shootDirection;

        // 발사 속도와 방향에 따라 총알을 전진시킴
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