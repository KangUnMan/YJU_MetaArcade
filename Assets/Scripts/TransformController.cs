using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformController : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameObject leftHand;
    // Start is called before the first frame update
    private void Awake()
    {
        TransformUpdate();
    }

    // Update is called once per frame
    public void TransformUpdate()
    {
        weapon.transform.position = new Vector3(0.03f, 0.133f, -0.031f);
        leftHand.transform.position = new Vector3(-0.2821f, 0.0748f, -0.0504f);
        weapon.transform.Rotate(new Vector3(0.03f, 0.133f, -0.031f));
        leftHand.transform.Rotate(new Vector3(75.464f, -549.511f, -476.017f));
    }
}
