using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    private StarterAssetsInputs input;
    private ThirdPersonController controller;
    private Animator ani;

    [Header("Aim")]
    [SerializeField]
    private CinemachineVirtualCamera aimCam;
    [SerializeField]
    private GameObject aimImage;
    [SerializeField]
    //private GameObject aimObj;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        controller = GetComponent<ThirdPersonController>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AimCheck();
    }
    private void AimCheck()
    {
        if (input.aim)
        {
            aimCam.gameObject.SetActive(true);
            aimImage.SetActive(true);
            controller.isAimMove = true;

            ani.SetLayerWeight(1,1);

            Vector3 targetPositon = Vector3.zero;

            Transform camTransform = Camera.main.transform;
            RaycastHit hit;

            if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, Mathf.Infinity))
            {
                //Debug.Log("Name : " + hit.transform.gameObject.name);
                targetPositon = hit.point;
               // aimObj.transform.position = hit.point;
            }
            else
            {
                targetPositon = camTransform.position + camTransform.forward;
                //aimObj.transform.position = camTransform.position + camTransform.forward;
            }

            Vector3 targetAim = targetPositon;
            targetAim.y = transform.position.y;
            Vector3 aimDir = (targetAim - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 30f);
        }
        else
        {
            aimCam.gameObject.SetActive(false);
            aimImage.SetActive(false);
            controller.isAimMove = false;

            ani.SetLayerWeight(1, 0);
        }
    }
        
}
