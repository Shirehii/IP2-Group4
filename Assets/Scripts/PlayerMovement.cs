using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //initialize some variables
    public float horizontalSpeed = 5f;
    public float verticalSpeed = 5f;
    [HideInInspector]
    public Rigidbody rb;
    public bool facingRight = true;

    //these variables are for player input. they are bound to player 1 by default, but if you create a second player just change P1 to P2 in the inspector.
    private string horizontalButton = "Horizontal_P1";
    private string verticalButton = "Vertical_P1";

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Awake()
    {
        Time.timeScale = 1; //unfreeze game, just in case
        //get some components
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (gameObject.tag == "Player2") //if the player is not player 1, change the input axis
        {
            horizontalButton = "Horizontal_P2";
            verticalButton = "Vertical_P2";
        }
    }

    void Update()
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

        //running animation
        if (Mathf.Abs(rb.velocity.x + rb.velocity.z) > 0)
        {
            animator.SetFloat("speed", Mathf.Abs(rb.velocity.x + rb.velocity.z));
        }
        if (Mathf.Abs(rb.velocity.x + rb.velocity.z) == 0)
        {
            animator.SetFloat("speed", Mathf.Abs(rb.velocity.x - rb.velocity.z));
        }

        //checking the player's movement direction, in case the sprite has to be flipped
        float h = Input.GetAxis(horizontalButton); //get the value of the horizontal axis
        if (h > 0 && !facingRight) //if it's positive and the player isn't facing right
        {
            Flip();
            animator.SetBool("facingRight", true);
        }
        else if (h < 0 && facingRight) //if it's negative and the player is facing right
        {
            Flip();
            animator.SetBool("facingRight", false);
        }
    }

    void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        facingRight = !facingRight;
    }

    //FixedUpdate might be better to use than Update for physics related stuff
    //void FixedUpdate()
    //{
    //    
    //}
}