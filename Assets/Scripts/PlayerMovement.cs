using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //initialize some variables
    private Rigidbody rb;
    private float horizontalSpeed = 10f;
    private float verticalSpeed = 7.5f;

    //these variables are for player input. they are bound to player 1 by default, but if you create a second player just change P1 to P2 in the inspector.
    public string horizontalButton = "Horizontal_P1";
    public string verticalButton = "Vertical_P1";

    private void Start()
    {
        //get the rigidbody
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //this is where the movement magic happens
        if (Input.GetAxis(horizontalButton) != 0) //left+right
        {
            rb.velocity = new Vector3(Input.GetAxis(horizontalButton) * horizontalSpeed, rb.velocity.y, rb.velocity.z);
        }
        
        if (Input.GetAxis(verticalButton) != 0) //forward+back
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Input.GetAxis(verticalButton) * verticalSpeed);
        }
    }

    //FixedUpdate might be better to use than Update for physics related stuff
    //private void FixedUpdate()
    //{
    //    
    //}
}