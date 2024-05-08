using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player_Manager : MonoBehaviour
{
    private StarterAssetsInputs input;
    private ThirdPersonController controller;
    private Animator ani;
    private FirstPlayerController fpc;
    private Quaternion originalRotation;
    //private TransformController tfc;

    [Header("Aim")]
    [SerializeField]
    private CinemachineVirtualCamera aimCam;
    [SerializeField]
    private GameObject aimImage;
    [SerializeField]
    private GameObject aimObj;
    [SerializeField]
    private Camera scopeCam;
    [SerializeField]
    private bool IsScoped = false;
    [SerializeField]
    private GameObject PlayerCam;
    //public GameObject UpperBody;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        controller = GetComponent<ThirdPersonController>();
        ani = GetComponent<Animator>();
        fpc = GetComponent<FirstPlayerController>();
        originalRotation = transform.rotation;
        //tfc = GetComponent<TransformController>();
    }

    // Update is called once per frame
    void Update()
    {
        //AimCheck();
        ScopeMode();
    }
    //private void AimCheck()
    //{
    //    if (input.aim)
    //    {
    //        aimCam.gameObject.SetActive(true);
    //        aimImage.SetActive(true);
    //        controller.isAimMove = true;

    //        ani.SetLayerWeight(1,1);

    //        Vector3 targetPositon = Vector3.zero;

    //        Transform camTransform = Camera.main.transform;
    //        RaycastHit hit;

    //        if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, Mathf.Infinity))
    //        {
    //            //Debug.Log("Name : " + hit.transform.gameObject.name);
    //            targetPositon = hit.point;
    //            aimObj.transform.position = hit.point;
    //        }
    //        else
    //        {
    //            targetPositon = camTransform.position + camTransform.forward;
    //            aimObj.transform.position = camTransform.position + camTransform.forward;
    //        }

    //        Vector3 targetAim = targetPositon;
    //        targetAim.y = transform.position.y;
    //        Vector3 aimDir = (targetAim - transform.position).normalized;

    //        transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 30f);
    //    }
    //    else
    //    {
    //        aimCam.gameObject.SetActive(false);
    //        aimImage.SetActive(false);
    //        controller.isAimMove = false;

    //        ani.SetLayerWeight(1, 0);
    //    }
    //}
    private void ScopeMode()
    {
        //FirstPlayerController fpcScript = UpperBody.GetComponent<FirstPlayerController>();
        if (UnityEngine.Input.GetButtonDown("Fire2"))
        {
            //fpcScript.enabled = true;
            scopeCam.gameObject.SetActive(true);
            PlayerCam.gameObject.SetActive(false);
            //controller.isAimMove = true;
            controller.enabled = false;
            //tfc.enabled = true;
            fpc.enabled = true;
            ani.SetLayerWeight(1,1);
        }
        else if (UnityEngine.Input.GetButtonUp("Fire2"))
        {
            //fpcScript.enabled = false;
            scopeCam.gameObject.SetActive(false);
            PlayerCam.gameObject.SetActive(true);
            //controller.isAimMove = false;
            controller.enabled = true;
            //tfc.enabled = false;
            fpc.enabled = false;
            ani.SetLayerWeight(1, 0);
            transform.rotation = originalRotation;
        }

        if (UnityEngine.Input.GetButton("Fire2") && UnityEngine.Input.GetKey(KeyCode.LeftShift))
        {
            scopeCam.fieldOfView = 30;
            //ani.speed = 0;
            Debug.Log("쉬프트키다운");
        }
        else
        {
            scopeCam.fieldOfView = 60;
            //ani.speed = 1;
            Debug.Log("쉬프트키업");
        }
    }
}
