using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks // 총알 파괴 스크립트
{
    private void Start()
    {
        StartCoroutine(DestroyAfterTime(0.9f)); // 2.5초 후에 파괴
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    public void AddBulletForce(Vector3 direction, float force)
    {
        Rigidbody bulletRigidbody = GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            // Rigidbody 컴포넌트가 있으면 총알에 힘을 가해 움직이게 함
            bulletRigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("총알에 Rigidbody가 없습니다.");
        }
    }
}
