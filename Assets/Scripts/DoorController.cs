using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool isFKeyPressed = false;
    private bool isCursorOverObject = false;
    public Animator doorAnimator;
    private bool isopen = false;
    public GameController GameController;
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
        /*if (Input.GetKeyDown(KeyCode.E))
        {
           if (isopen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }*/
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
    void OnGUI()
    {
        Event e = Event.current;
        if (isCursorOverObject && e.isKey && e.keyCode == KeyCode.F)
        {
            isFKeyPressed = true;
        }
    }
}
