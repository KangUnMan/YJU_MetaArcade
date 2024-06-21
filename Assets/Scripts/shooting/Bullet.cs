using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks // ÃÑ¾Ë ÆÄ±« ½ºÅ©¸³Æ®
{
    private void Start()
    {
        StartCoroutine(DestroyAfterTime(0.9f)); // 2.5ÃÊ ÈÄ¿¡ ÆÄ±«
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Destroy(gameObject);
    }
}
