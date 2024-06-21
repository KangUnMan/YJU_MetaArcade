using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPunCallbacks // ÃÑ¾Ë ÆÄ±« ½ºÅ©¸³Æ®
{
 
    private void Awake()
    {
    }
    void Start()
    {
        Destroy(gameObject, 2.5f);
    }



    void DestroyRPC()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
