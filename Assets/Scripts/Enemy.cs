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
    private Vector3 startingPosition;

    //sprite related stuff
    public string enemyColor;
    private Sprite[] enemySprites;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        startingPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        crystal = GameObject.FindGameObjectWithTag("Crystal").transform;
        rb = GetComponent<Rigidbody>(); //Get this objects Rigidbody Component
        offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

        //get the enemy sprites
        enemySprites = new Sprite[3];
        enemySprites[0] = Resources.Load<Sprite>("blueEnemy");
        enemySprites[1] = Resources.Load<Sprite>("redEnemy");
        enemySprites[2] = Resources.Load<Sprite>("yellowEnemy");
        animator = GetComponent<Animator>();

        //set the enemy color
        if (Random.value < 0.3f)
        {
            enemyColor = "blue";
            spriteRenderer.sprite = enemySprites[0];
            animator.SetInteger("EnemyColor", 0); //blue
        }
        else if (Random.value >= 0.3f && Random.value < 0.6f)
        {
            enemyColor = "red";
            spriteRenderer.sprite = enemySprites[1];
            animator.SetInteger("EnemyColor", 1); //red
        }
        else if (Random.value >= 0.6f)
        {
            enemyColor = "yellow";
            spriteRenderer.sprite = enemySprites[2];
            animator.SetInteger("EnemyColor", 2); //yellow
        }
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
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(crystal.position.x, -1, crystal.position.z) + offset, speed * Time.deltaTime); //Move towards the crystal's position
        //direction = (transform.position - crystal.position).normalized;  //these two lines are alternatives in case MoveTowards breaks .-.
        //transform.position -= direction * speed * Time.deltaTime;

        if (crystal.transform.position.x < gameObject.transform.position.x && facingRight)
            Flip();
        if (crystal.transform.position.x > gameObject.transform.position.x && !facingRight)
            Flip();
    }

    private void OnTriggerEnter(Collider other)
    {
        string otherTag = other.gameObject.tag;
        string otherSpriteName = other.gameObject.GetComponent<SpriteRenderer>().sprite.name;
        if (otherTag == "Bullet" || otherTag == "AbilityPuddle" || otherTag == "AbilityBomb" || otherTag == "AbilityPierce")
        {
            if (otherTag == "Bullet") //destroy the bullet that hit it
            {
                Destroy(other.gameObject);
            }
            if (other.gameObject.GetComponent<ProjectileLogic>().projectileColor == enemyColor) //if the two sprites are the same color
            {
                GameObject.FindGameObjectWithTag("EnemyGen").GetComponent<GenerateEnemies>().EnemyDied(); //trigger enemy death in GenerateEnemies.cs
                ScoreText.scoreValue += 10;
                Destroy(gameObject); //Enemy death
            }
        }

        if (otherTag == "Crystal")
        {
            animator.SetBool("isAttacking", true);
        }
    }
}