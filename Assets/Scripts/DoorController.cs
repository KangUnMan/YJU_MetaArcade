using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    private bool isopen = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isopen)
            {
                CloseDoor();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isopen)
            {
                OpenDoor();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           if (isopen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
    {
        doorAnimator.SetBool("isopen", true);
        isopen = true;
    }
    private void CloseDoor()
    {
        doorAnimator.SetBool("isopen", false);
        isopen = false;
    }

}
