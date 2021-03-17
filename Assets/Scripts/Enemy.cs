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
    private float xPos;
    private float zPos;
    private Vector3 offset;
    private Vector3 direction;
    private Vector3 startingPosition;

    //color related stuff
    public string enemyColor;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isAttacking = false;
    private GenerateEnemies enemyGen;

    void Start()
    {
        enemyGen = GameObject.FindGameObjectWithTag("EnemyGen").GetComponent<GenerateEnemies>();

        startingPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        crystal = GameObject.FindGameObjectWithTag("Crystal").transform;
        rb = GetComponent<Rigidbody>(); //Get this objects Rigidbody Component

        //offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        Random.InitState(System.DateTime.Now.Millisecond);
        if (Random.value < 0.5f) //enemy left side of crystal attack
        {
            xPos = -1.25f;
            zPos = Random.Range(-1f, 1f);
        }
        else if (Random.value >= 0.5f) //enemy right side of crystal attack
        {
            xPos = 1.25f;
            zPos = Random.Range(-1f, 1f);
        }
        if (xPos != 1.25f && xPos != -1.25f)
        {
            xPos = -1.25f;
        }
        offset = new Vector3(xPos, 0, zPos);
        
        animator = GetComponent<Animator>();

        //set the enemy color
        SetEnemyColor();
    }

    void FixedUpdate()
    {
        if (enemyColor == null || enemyColor == "" || enemyColor == " ")
        {
            SetEnemyColor();
        }

        EnemyMovement(); //Calling code within private function "Player1Enemy"

        if (isAttacking)
        {
            StartCoroutine(EnemyAttack());
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
        if (otherTag == "Bullet" || otherTag == "AbilityPuddle" || otherTag == "AbilityBomb" || otherTag == "AbilityPierce")
        {
            float scoreMultiplier = other.gameObject.GetComponent<ProjectileLogic>().scoreMultiplier;

            string otherSpriteName = other.gameObject.GetComponent<SpriteRenderer>().sprite.name;
            if (otherTag == "Bullet") //destroy the bullet that hit it
            {
                Destroy(other.gameObject);
            }
            if (other.gameObject.GetComponent<ProjectileLogic>().projectileColor == enemyColor) //if the two sprites are the same color (blue, red, or yellow)
            {
                enemyGen.EnemyDied(); //trigger enemy death in GenerateEnemies.cs
                ScoreText.scoreValue += 10 * scoreMultiplier;
                Destroy(gameObject); //Enemy death
            }
            else if (enemyColor == "green") //if the enemy is green
            {
                if (otherSpriteName.Replace(otherTag, "") == "blue") //and gets hit by a blue projectile
                {
                    enemyColor = "yellow"; //turn yellow
                    animator.SetInteger("EnemyColor", 2);
                }
                else if (otherSpriteName.Replace(otherTag, "") == "yellow") //or gets hit by a yellow projectile
                {
                    enemyColor = "blue"; //turn blue
                    animator.SetInteger("EnemyColor", 0);
                }
            }
            else if (enemyColor == "orange") //if the enemy is orange
            {
                if (otherSpriteName.Replace(otherTag, "") == "red")
                {
                    enemyColor = "yellow";
                    animator.SetInteger("EnemyColor", 2);
                }
                else if (otherSpriteName.Replace(otherTag, "") == "yellow")
                {
                    enemyColor = "red";
                    animator.SetInteger("EnemyColor", 1);
                }
            }
            else if (enemyColor == "purple") //if the enemy is purple
            {
                if (otherSpriteName.Replace(otherTag, "") == "blue")
                {
                    enemyColor = "red";
                    animator.SetInteger("EnemyColor", 1);
                }
                else if (otherSpriteName.Replace(otherTag, "") == "red")
                {
                    enemyColor = "blue";
                    animator.SetInteger("EnemyColor", 0);
                }
            }
        }

        if (otherTag == "Crystal")
        {
            animator.SetBool("isAttacking", true);
            isAttacking = true;
        }
    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(4.4f);
        GameObject.FindGameObjectWithTag("Crystal").GetComponent<CrystalHP>().LoseMoreHP();
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    
    void SetEnemyColor()
    {
        if (Random.value < 0.2f) //blue
        {
            animator.SetInteger("EnemyColor", 0);
            enemyColor = "blue";
        }
        else if (Random.value >= 0.2f && Random.value < 0.4f) //red
        {
            animator.SetInteger("EnemyColor", 1);
            enemyColor = "red";
        }
        else if (Random.value >= 0.4f && Random.value < 0.7f) //yellow
        {
            animator.SetInteger("EnemyColor", 2);
            enemyColor = "yellow";
        }
        else if (Random.value >= 0.7f && Random.value < 0.8f) //green
        {
            animator.SetInteger("EnemyColor", 3);
            enemyColor = "green";
        }
        else if (Random.value >= 0.8f && Random.value < 0.9f) //orange
        {
            animator.SetInteger("EnemyColor", 4);
            enemyColor = "orange";
        }
        else if (Random.value >= 0.9f) //purple
        {
            animator.SetInteger("EnemyColor", 5);
            enemyColor = "purple";
        }
        print(enemyColor);
    }
}