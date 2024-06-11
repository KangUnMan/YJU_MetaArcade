using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    public float gravityDelay = 2f; // 중력이 적용되기까지의 지연 시간
    public float bulletLifetime = 5f; // 총알이 사라지기까지의 시간
    public float gravityScale = 1f; // 중력 스케일 조정
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 총알이 발사된 후 일정 시간이 지난 후에 중력을 적용하기 위한 코루틴 실행
        StartCoroutine(EnableGravityAfterDelay());
    }

    IEnumerator EnableGravityAfterDelay()
    {
        // 중력이 적용되기까지의 지연 시간만큼 대기
        yield return new WaitForSeconds(gravityDelay);

        // Rigidbody의 중력 스케일 조정
        rb.velocity += Physics.gravity * gravityScale;

        // 일정 시간이 지난 후 총알을 파괴
        Destroy(gameObject, bulletLifetime - gravityDelay);
    }
}
