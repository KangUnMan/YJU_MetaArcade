using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class PlayerCameraSetup : MonoBehaviour
{
    public CinemachineVirtualCamera playerCamera; // 시네머신 카메라

    void Start()
    {
        SetPlayerCamera();
    }

    public void SetPlayerCamera()
    {
        // 플레이어의 PhotonView를 가져옵니다.
        PhotonView photonView = GetComponent<PhotonView>();
        if (photonView != null)
        {
            // 로컬 플레이어인 경우에만 가상 카메라를 설정합니다.
            if (photonView.IsMine)
            {
                // 각 플레이어의 시네머신 가상 카메라의 우선 순위를 높여 해당 플레이어의 시점이 우선순위를 가지도록 합니다.
                playerCamera.Priority = 10;
            }
            else
            {
                // 다른 플레이어에게는 카메라를 할당하지 않습니다.
                playerCamera.gameObject.SetActive(false);
            }
        }
    }
}