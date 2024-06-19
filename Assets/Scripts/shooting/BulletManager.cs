using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Blue") 
        {
            Debug.Log("3Á¡");
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Green")
        {
            Debug.Log("2Á¡");
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Red")
        {
            Debug.Log("1Á¡");
            Destroy(this.gameObject);
        }
    }
}
