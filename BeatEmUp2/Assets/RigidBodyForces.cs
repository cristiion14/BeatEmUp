using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyForces : MonoBehaviour
{
    Rigidbody rb;
    float thrust =10.0f;
   void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Y))
            rb.AddForce(new Vector3(-thrust,0,0), ForceMode.Acceleration);
            
    }
}
