using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Declaring Variables

    public Transform crystal;
    private Rigidbody rb; //Rigidbody of object
    public bool facingRight = false; //Set the local X scale for the objects renderer
    public float speed = 2; //Set enemy speed
    private Vector3 offset;
    private Vector3 direction;
    private SpriteRenderer spriteRenderer;
    private Vector3 startingPosition;
    public int isTouchingCrystal = 0; //0 if not touching, 1 if touching

    void Start()
    {
        startingPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        crystal = GameObject.FindGameObjectWithTag("Crystal").transform;
        rb = GetComponent<Rigidbody>(); //Get this objects Rigidbody Component
        offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
    }

    void FixedUpdate()
    {
        if (isTouchingCrystal == 0)
        { 
            EnemyMovement(); //Calling code within private function "Player1Enemy"
        }
    }

    private void Flip() //Controls the "Flip" of the eney based on the characters position on X
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void EnemyMovement() //Move towards crystal & Flip
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(crystal.position.x, -1, crystal.position.z) + offset, speed * Time.deltaTime); //Move towards the crystal's position
        //direction = (transform.position - crystal.position).normalized;
        //transform.position -= direction * speed * Time.deltaTime;

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
            GameObject.FindGameObjectWithTag("EnemyGen").GetComponent<GenerateEnemies>().EnemyDied();
            Destroy(gameObject); //Enemy death
        }

        //added this check because enemies would stop following crystal after collisions
        if (other.gameObject.tag == "Crystal")
        {
            isTouchingCrystal = 1;
        }
    }

    //added this check because enemies would stop following crystal after collisions
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Crystal")
        {
            isTouchingCrystal = 0;
        }
    }
}