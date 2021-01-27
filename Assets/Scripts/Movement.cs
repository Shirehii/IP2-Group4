using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //initialize some variables
    private Rigidbody rb;
    private float horizontalSpeed = 10;
    private float verticalSpeed = 5;

    private void Start()
    {
        //get the rigidbody
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //this is where the movement magic happens
        if (Input.GetAxis("Horizontal") != 0) //left+right
        {
            rb.velocity = new Vector3(Input.GetAxis("Horizontal") * horizontalSpeed, rb.velocity.y, rb.velocity.z);
        }
        
        if (Input.GetAxis("Vertical") != 0) //forward+back
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Input.GetAxis("Vertical") * verticalSpeed);
        }
    }

    //FixedUpdate might be better to use than Update for physics related stuff
    //private void FixedUpdate()
    //{
    //    
    //}
}