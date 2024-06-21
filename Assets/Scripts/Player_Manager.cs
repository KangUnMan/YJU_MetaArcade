using Cinemachine;
using Photon.Pun;
using StarterAssets;
using System.ComponentModel;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public GameObject playerPrefab;
    private StarterAssetsInputs input;
    private ThirdPersonController controller;
    private Animator ani;
    private FirstPlayerController fpc;
    private Quaternion originalRotation;
    public GameObject Gun;
    public PhotonView PV;

    [Header("Aim")]
   // [SerializeField] private CinemachineVirtualCamera scopeCam;
    [SerializeField] private Camera scopeCam;
    [SerializeField] private GameObject PlayerCam;

    private bool isScoped = false;
    private bool isInShootingZone = false;

    void Start()
    {
        if (PV.IsMine)
        {
            input = GetComponent<StarterAssetsInputs>();
            controller = GetComponent<ThirdPersonController>();
            ani = GetComponent<Animator>();
            fpc = GetComponent<FirstPlayerController>();
            originalRotation = transform.rotation;
            ani.SetLayerWeight(1, 0);
        }
    }

    void Update()
    {
        if (PV.IsMine)
        {
            if (isInShootingZone)
            {

                ScopeMode();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PV.IsMine)
        {
            if (other.CompareTag("ShootingZone"))
            {
                isInShootingZone = true;
                Gun.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (PV.IsMine)
        {
            if (other.CompareTag("ShootingZone"))
            {
                isInShootingZone = false;
                Gun.SetActive(false);
                // Exit 시 스코프 모드 해제
                DisableScopeMode();
            }
        }
    }

    private void ScopeMode()
    {
        if (PV.IsMine)
        {
            if (Input.GetButtonDown("Fire2"))
            {

                scopeCam.gameObject.SetActive(true);
                PlayerCam.gameObject.SetActive(false);
                controller.enabled = false;
                fpc.enabled = true;
                ani.SetLayerWeight(1, 1);
                isScoped = true;
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                DisableScopeMode();
            }
        } 
            
    }

    private void DisableScopeMode()
    {
        if (PV.IsMine)
        {
            scopeCam.gameObject.SetActive(false);
            PlayerCam.gameObject.SetActive(true);
            controller.enabled = true;
            fpc.enabled = false;
            ani.SetLayerWeight(1, 0);
            transform.rotation = originalRotation;
            isScoped = false;
        }
    }
}
