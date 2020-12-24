using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    //animator reference
    private CharacterAnimation playerAnim;
    private Rigidbody rb;

    public Collider isGround;

    float distToGround;
    public LayerMask groundMask;
    public Transform groundCheck;
    public  bool isGrounded = true;

    [SerializeField]
    float initialSpeed = 2f;

    float speed;
    float acceleration = 50f;
    bool canIncreaseAcc = false;

    [SerializeField]
    float maxSpeed = 3.5f;

    [SerializeField]
    float jumpForce =4.25f;


    private float rotationY = -90f;
    private float rotationSpeed = 15f;

	// Use this for initialization
	void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponentInChildren<CharacterAnimation>();
        speed = initialSpeed;
    }

    void Start()
    {
        //get the distance to the ground
        distToGround = isGround.bounds.extents.y;
    }
	
	// Update is called once per frame
	void Update () {
        RotatePlayer();
        AnimatePlayerWalk();


    //    isGrounded = Physics.CheckSphere(groundCheck.position, .2f, groundMask);

        Debug.LogError("Is grounded: " + checkGround());
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
    bool isRunningonY = false;

    bool checkGround()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(isGround.transform.position, -isGround.transform.forward, out raycastHit, .5f))
            return true;
        else
            return false;
    }


   public void DetectMov()
    {
        //clamp the speed
        if (speed > maxSpeed)
            speed = maxSpeed;

        //move the player
        rb.velocity = new Vector3(Input.GetAxis(Axis.HORIZONTAL_AXIS) * (-speed), rb.velocity.y, Input.GetAxis(Axis.VERTICAL_AXIS) * (-speed));


        //acceleration
        if ((Input.GetAxisRaw(Axis.VERTICAL_AXIS)>0 && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS)>0) ||          
            Input.GetAxisRaw(Axis.VERTICAL_AXIS)>0 && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS)<0  ||
            Input.GetAxisRaw(Axis.VERTICAL_AXIS)<0 && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS)>0  ||
            Input.GetAxisRaw(Axis.VERTICAL_AXIS)<0 && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS)<0  ||
            Input.GetAxis(Axis.HORIZONTAL_AXIS) >.5f || Input.GetAxis(Axis.HORIZONTAL_AXIS) <-.5f ||
            Input.GetAxis(Axis.VERTICAL_AXIS) >.5f || Input.GetAxis(Axis.VERTICAL_AXIS) <-.5f && speed<maxSpeed)
        {
            //run and increase speed
            playerAnim.anim.SetBool("Run", true);
            canIncreaseAcc = true;
           // speed += acceleration * Time.deltaTime;
        }
        else
        {
            //disable running anim and set the speed to default value
            playerAnim.anim.SetBool("Run", false);
         //   speed = initialSpeed;
            canIncreaseAcc = false;
        }

        Debug.LogError("can increase speed: " + canIncreaseAcc + " the speed is: " + speed);

        //increase acceleration
        if (canIncreaseAcc)
            rb.AddForce(new Vector3(rb.velocity.x, 0, rb.velocity.y) * acceleration, ForceMode.Acceleration);
        //  speed += acceleration * Time.deltaTime;
        else
            speed = initialSpeed;

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && checkGround())
        {
            //set the velocity of the rb on the Y axis to the jump value
            Vector3 vel = rb.velocity;
            vel.y = jumpForce;
            rb.velocity = vel;

            //play jump animation and make the player jump 
            GetComponentInChildren<PlayerAttack>().playerAnim.Jump();
        }


        //Land anim
        //maybe try landing after jumping higher stuff

        if (!checkGround() && rb.velocity.y < 0.5f)
        {
            //    playerAnim.Land();
        }

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

        //top right
        if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) > 0 && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > 0)
            transform.rotation = Quaternion.Euler(0, -135, 0);
        else if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) < 0 && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) < 0)
            transform.rotation = Quaternion.Euler(0, 135, 0);

        //top left
        if(Input.GetAxisRaw(Axis.VERTICAL_AXIS) > 0 && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) < 0)
            transform.rotation = Quaternion.Euler(0, -225, 0);
       else if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) < 0 && Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > 0)
            transform.rotation = Quaternion.Euler(0, 225, 0);

        //down right
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
            Instantiate(GetComponentInChildren<PlayerAttack>().hitEffect, new Vector3(other.GetContact(0).point.x, other.GetContact(0).point.y, other.GetContact(0).point.z), Quaternion.identity);   //GetComponentInChildren<PlayerAttack>().leftPunch.transform.position, GetComponentInChildren<PlayerAttack>().leftPunch.transform.rotation);
            Debug.LogError("INSTANTIAZA");
        }


    }

    void OnCollisionExit(Collision other)
    {
    

    }

    void OnCollisionStay()
    {
      //  isGrounded = true;
    }


}
