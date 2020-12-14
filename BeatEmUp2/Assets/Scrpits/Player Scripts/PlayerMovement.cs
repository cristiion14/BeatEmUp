using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //animator reference
    private CharacterAnimation playerAnim;
    private Rigidbody rb;

    public LayerMask groundMask;
    public Transform groundCheck;
    public  bool isGrounded = true;
    public float walkSpeed = 2f;
    public float zSpeed = 1.5f;
    float jumpForce =4.25f;
   

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

        Debug.LogError("Is grounded: " + isGrounded);
    }

    void FixedUpdate()
    {
        //any phisics calculations goes in here
        if (!GetComponentInChildren<PlayerAttack>().isPunching && !GetComponentInChildren<PlayerAttack>().isKicking)
        {
            DetectMov();
        }

    }
    bool isRunningOnX = false;
    void DetectMov()
    {
        
        rb.velocity = new Vector3(Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * (-walkSpeed), rb.velocity.y, Input.GetAxisRaw(Axis.VERTICAL_AXIS) * (-zSpeed));

      //  Debug.LogError("THE SPEED ON X IS: " + Input.GetAxis(Axis.HORIZONTAL_AXIS) + "\n" + "THE SPEED ON THE Y IS: " + Input.GetAxis(Axis.VERTICAL_AXIS));

        //run anim on X
        if (Input.GetAxis(Axis.HORIZONTAL_AXIS) > .5f || Input.GetAxis(Axis.HORIZONTAL_AXIS) <-.5f)
        {
            playerAnim.anim.SetBool("Run", true);
            walkSpeed += .05F;
            isRunningOnX = true;
        }

       else
        {
            walkSpeed = 2;
            playerAnim.anim.SetBool("Run", false);
            isRunningOnX = false;
        }

        // run anim on the y
        if (Input.GetAxis(Axis.VERTICAL_AXIS) > .5f || Input.GetAxis(Axis.VERTICAL_AXIS) <-.5f)
        {
            playerAnim.anim.SetBool("Run", true);
            zSpeed += .05F;
        }

        else if(!isRunningOnX)
        {
            zSpeed = 1.5F;
            playerAnim.anim.SetBool("Run", false);
        }



        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //set the y value of the rb to 0 before jumping
            Vector3 vel = rb.velocity;
            vel.y = 0f;
            rb.velocity = vel;

            //jump
            GetComponentInChildren<PlayerAttack>().playerAnim.Jump();
            rb.AddForce(new Vector3(0,jumpForce,0), ForceMode.VelocityChange);
            isGrounded = false;
        }

        //Land anim
        if(GetComponentInChildren<PlayerAttack>().playerAnim.anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && transform.position.y <0.5f)
            playerAnim.Land();

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

        if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) < 0)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        else if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) > 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) > 0 && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > 0)
            transform.rotation = Quaternion.Euler(0, -135, 0);
        else if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) < 0 && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) < 0)
            transform.rotation = Quaternion.Euler(0, 135, 0);

        if(Input.GetAxisRaw(Axis.VERTICAL_AXIS) > 0 && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) < 0)
            transform.rotation = Quaternion.Euler(0, -225, 0);
       else if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) < 0 && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > 0)
            transform.rotation = Quaternion.Euler(0, 225, 0);

        if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) < 0 && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > 0)
            transform.rotation = Quaternion.Euler(0, -45, 0);
        else if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) < 0 && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) < 0)
            transform.rotation = Quaternion.Euler(0, 45, 0);


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

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.name == Tags.ENEMY_TAG)
        {
            Instantiate(GetComponentInChildren<PlayerAttack>().punchSystem, new Vector3(other.GetContact(0).point.x, other.GetContact(0).point.y, other.GetContact(0).point.z), Quaternion.identity);   //GetComponentInChildren<PlayerAttack>().leftPunch.transform.position, GetComponentInChildren<PlayerAttack>().leftPunch.transform.rotation);
            Debug.LogError("INSTANTIAZA");

        }
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Ground")
            isGrounded = false;
    }

}
