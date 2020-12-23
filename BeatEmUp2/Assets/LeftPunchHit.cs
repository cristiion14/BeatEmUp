using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPunchHit : MonoBehaviour
{
    public Vector3 leftPunchHit = Vector3.zero;

    
  void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == Tags.GROUND_TAG)
            other.transform.position = leftPunchHit;
    }
}
