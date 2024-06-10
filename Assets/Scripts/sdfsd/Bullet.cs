using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulttet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 다른 물체와 충돌하면 파괴
        Destroy(gameObject);
    }
}
