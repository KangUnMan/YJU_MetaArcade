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
}
