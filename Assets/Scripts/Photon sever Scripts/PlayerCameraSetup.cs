using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class PlayerCameraSetup : MonoBehaviour
{
    public CinemachineVirtualCamera playerCamera; // �ó׸ӽ� ī�޶�

    void Start()
    {
        SetPlayerCamera();
    }

    public void SetPlayerCamera()
    {
        // �÷��̾��� PhotonView�� �����ɴϴ�.
        PhotonView photonView = GetComponent<PhotonView>();
        if (photonView != null)
        {
            // ���� �÷��̾��� ��쿡�� ���� ī�޶� �����մϴ�.
            if (photonView.IsMine)
            {
                // �� �÷��̾��� �ó׸ӽ� ���� ī�޶��� �켱 ������ ���� �ش� �÷��̾��� ������ �켱������ �������� �մϴ�.
                playerCamera.Priority = 10;
            }
            else
            {
                // �ٸ� �÷��̾�Դ� ī�޶� �Ҵ����� �ʽ��ϴ�.
                playerCamera.gameObject.SetActive(false);
            }
        }
    }
}