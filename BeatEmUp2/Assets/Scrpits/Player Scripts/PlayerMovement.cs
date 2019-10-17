using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //animator reference
    private CharacterAnimation playerAnim;
    private Rigidbody rb;
    public float walkSpeed = 2f;
    public float zSpeed = 1.5f;

    private float rotationY = -90f;
    private float rotationSpeed = 15f;

	// Use this for initialization
	void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponentInChildren<CharacterAnimation>();
    }
	
	// Update is called once per frame
	void Update () {
        RotatePlayer();
        AnimatePlayerWalk();
       
	}

    void FixedUpdate()
    {
        //any phisics calculations goes in here
        DetectMov();
    }
    void DetectMov()
    {
        rb.velocity = new Vector3(Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * (-walkSpeed), rb.velocity.y, Input.GetAxisRaw(Axis.VERTICAL_AXIS) * (-zSpeed));
    }

    void RotatePlayer()
    {                 //if is moving to the right
        if(Input.GetAxisRaw(Axis.HORIZONTAL_AXIS)>0)
        {
            transform.rotation = Quaternion.Euler(0f, -Mathf.Abs(rotationY), 0);
        }
        else if(Input.GetAxisRaw(Axis.HORIZONTAL_AXIS)<0)
        {            //if going to the left
            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotationY), 0);
        }
        
    }

    void AnimatePlayerWalk()
    {                   //if the player moves in either position ---> call animation
        if(Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) != 0 || Input.GetAxisRaw(Axis.VERTICAL_AXIS) !=0)
        {
            playerAnim.Walk(true);
        }
        else
        {
            playerAnim.Walk(false);
        }
    }
   
}
