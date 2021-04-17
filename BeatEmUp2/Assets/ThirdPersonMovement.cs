using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float turnSmoothTime = .1f;
    float turnSmoothVelocity;
    private void Update()
    {
        float x = -Input.GetAxisRaw(Axis.HORIZONTAL_AXIS);
        float z = -Input.GetAxisRaw(Axis.VERTICAL_AXIS);

        Vector3 direction = new Vector3(x, 0, z).normalized;

        //if wants to move
        if(direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) *Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            controller.Move(direction * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            GetComponentInChildren<CharacterAnimation>().Walk(true);
        else
            GetComponentInChildren<CharacterAnimation>().Walk(false);

    }
}
