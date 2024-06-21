using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks // �Ѿ� �ı� ��ũ��Ʈ
{
    private void Start()
    {
        StartCoroutine(DestroyAfterTime(0.9f)); // 2.5�� �Ŀ� �ı�
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
            // Rigidbody ������Ʈ�� ������ �Ѿ˿� ���� ���� �����̰� ��
            bulletRigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("�Ѿ˿� Rigidbody�� �����ϴ�.");
        }
    }
}
