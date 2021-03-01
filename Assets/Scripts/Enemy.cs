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
    public float health;

    void Start()
    {
        health = 3;
        rb = this.GetComponent<Rigidbody>(); //Get this objects Rigidbody Component
    }

    void FixedUpdate()
    {
        Player1Enemy(); //Calling code within private function "Player1Enemy"
        EnemyDie();
    }

    private void Flip() //Controls the "Flip" of the eney based on the characters position on X
    {
        facingRight = !facingRight;
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x *= -1; //Minus the current enemy localScale on X
        gameObject.transform.localScale = tmpScale;
    }

    private void Player1Enemy() //Chase Player 1 & Flip
    {
        transform.position = Vector3.MoveTowards(transform.position, crystal.position, speed * Time.deltaTime); //Move towards the player1 position

        if (crystal.transform.position.x < gameObject.transform.position.x && facingRight)
            Flip();
        if (crystal.transform.position.x > gameObject.transform.position.x && !facingRight)
            Flip();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "AbilityPuddle")
        {
            health--; //Enemy health -1 per hit
            Destroy(GameObject.FindWithTag("Bullet")); //Destroy the bullet once it enters the trigger of enemy
        }
    }

    private void EnemyDie()
    {
        if (health <= 0)
        {
            Destroy(gameObject); //Enemy death as health <= 0
        }
    }
}