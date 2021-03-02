using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Declaring Variables

    public Transform crystal;
    private Rigidbody rb; //Rigidbody of object
    public bool facingRight = false; //Set the local X scale for the objects renderer
    public float speed; //Set enemy speed
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        crystal = GameObject.FindGameObjectWithTag("Crystal").transform;
        rb = GetComponent<Rigidbody>(); //Get this objects Rigidbody Component
    }

    void FixedUpdate()
    {
        EnemyMovement(); //Calling code within private function "Player1Enemy"
    }

    private void Flip() //Controls the "Flip" of the eney based on the characters position on X
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void EnemyMovement() //Move towards crystal & Flip
    {
        transform.position = Vector3.MoveTowards(transform.position, crystal.position, speed * Time.deltaTime); //Move towards the crystal's position

        if (crystal.transform.position.x < gameObject.transform.position.x && facingRight)
            Flip();
        if (crystal.transform.position.x > gameObject.transform.position.x && !facingRight)
            Flip();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "AbilityPuddle")
        {
            if (other.gameObject.tag == "Bullet") //destroy the bullet that hit it
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject); //Enemy death
        }
    }
}