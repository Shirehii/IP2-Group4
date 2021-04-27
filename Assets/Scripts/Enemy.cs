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

    private bool shouldAttack = false;
    private CrystalHP crystalHP;
    private GenerateEnemies enemyGen;

    private AudioSource source;
    private AudioClip bigAttack;
    private AudioClip deathSFX;

    public SpriteRenderer highlight;

    private TextMesh dmgNumber;

    void Start()
    {
        source = GetComponent<AudioSource>();
        bigAttack = Resources.Load<AudioClip>("crystalHit2");
        deathSFX = Resources.Load<AudioClip>("enemyDeath");

        enemyGen = GameObject.FindGameObjectWithTag("EnemyGen").GetComponent<GenerateEnemies>();

        startingPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        crystal = GameObject.FindGameObjectWithTag("Crystal").transform;
        rb = GetComponent<Rigidbody>(); //Get this objects Rigidbody Component

        //offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        int value = Random.Range(0, 2);
        if (value == 0) //enemy left side of crystal attack
        {
            xPos = -1.25f;
            zPos = Random.Range(-1f, 1f);
        }
        else if (value == 1) //enemy right side of crystal attack
        {
            xPos = 1.25f;
            zPos = Random.Range(-1f, 1f);
        }
        offset = new Vector3(xPos, 0, zPos);
        
        animator = GetComponent<Animator>();
        crystalHP = GameObject.FindGameObjectWithTag("Crystal").GetComponent<CrystalHP>();

        //set the enemy color
        highlight = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        SetEnemyColor();
        highlight.enabled = false;

        dmgNumber = GetComponentInChildren<TextMesh>();
    }

    void FixedUpdate()
    {        
        if (!shouldAttack && !animator.GetBool("isDying"))
        {
            EnemyMovement(); //Calling code within private function "Player1Enemy"
        }

        highlight.enabled = false; //disable the highlight sprite every frame
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
        GameObject otherObject = other.gameObject;
        string otherTag = otherObject.tag;
        if (otherTag == "Bullet" || otherTag == "AbilityPuddle" || otherTag == "AbilityBomb" || otherTag == "AbilityPierce")
        {
            ProjectileLogic otherPL = otherObject.GetComponent<ProjectileLogic>();
            float scoreMultiplier = otherPL.scoreMultiplier;
            string otherSpriteName = otherObject.GetComponent<SpriteRenderer>().sprite.name;
            
            if (other.gameObject.GetComponent<ProjectileLogic>().projectileColor == enemyColor) //if the two sprites are the same color (blue, red, or yellow)
            {
                otherPL.EnemyKilled();
                enemyGen.EnemyDied(); //trigger enemy death in GenerateEnemies.cs
                ScoreText.scoreValue += 10 * scoreMultiplier;
                StartCoroutine(EnemyDeath()); //Enemy death
            }
            else if (enemyColor == "green") //if the enemy is green
            {
                if (otherSpriteName.Replace(otherTag, "") == "blue") //and gets hit by a blue projectile
                {
                    enemyColor = "yellow"; //turn yellow
                    highlight.sprite = Resources.Load<Sprite>(enemyColor + "Target");
                    animator.SetInteger("EnemyColor", 2);
                }
                else if (otherSpriteName.Replace(otherTag, "") == "yellow") //or gets hit by a yellow projectile
                {
                    enemyColor = "blue"; //turn blue
                    highlight.sprite = Resources.Load<Sprite>(enemyColor + "Target");
                    animator.SetInteger("EnemyColor", 0);
                }
            }
            else if (enemyColor == "orange") //if the enemy is orange
            {
                if (otherSpriteName.Replace(otherTag, "") == "red")
                {
                    enemyColor = "yellow";
                    highlight.sprite = Resources.Load<Sprite>(enemyColor + "Target");
                    animator.SetInteger("EnemyColor", 2);
                }
                else if (otherSpriteName.Replace(otherTag, "") == "yellow")
                {
                    enemyColor = "red";
                    highlight.sprite = Resources.Load<Sprite>(enemyColor + "Target");
                    animator.SetInteger("EnemyColor", 1);
                }
            }
            else if (enemyColor == "purple") //if the enemy is purple
            {
                if (otherSpriteName.Replace(otherTag, "") == "blue")
                {
                    enemyColor = "red";
                    highlight.sprite = Resources.Load<Sprite>(enemyColor + "Target");
                    animator.SetInteger("EnemyColor", 1);
                }
                else if (otherSpriteName.Replace(otherTag, "") == "red")
                {
                    enemyColor = "blue";
                    highlight.sprite = Resources.Load<Sprite>(enemyColor + "Target");
                    animator.SetInteger("EnemyColor", 0);
                }
            }
        }

        if (otherTag == "Crystal")
        {
            animator.SetBool("isAttacking", true);
            shouldAttack = true;
            StartCoroutine(EnemyAttack());
        }
    }

    IEnumerator EnemyAttack()
    {
        for (int i = 0; i < 3; i++)
        {
            dmgNumber.text = "";
            yield return new WaitForSeconds(0.7f);
            if (shouldAttack)
            {
                dmgNumber.text = "-1";
                source.Play();
                crystalHP.currentHP -= 1;
                yield return new WaitForSeconds(0.4f);
            }
        }
        dmgNumber.text = "";
        yield return new WaitForSeconds(1f);
        if (shouldAttack)
        {
            dmgNumber.text = "-3";
            source.PlayOneShot(bigAttack);
            crystalHP.currentHP -= 3;
        }
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isDying", true);
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

    IEnumerator EnemyDeath()
    {
        shouldAttack = false;
        animator.SetBool("isDying", true);
        source.PlayOneShot(deathSFX);
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }


    void SetEnemyColor()
    {
        if (enemyGen.enemyColor == "blue") //blue
        {
            animator.SetInteger("EnemyColor", 0);
            enemyColor = "blue";
            source.clip = Resources.Load<AudioClip>("enemySmallAttack1");
        }
        else if (enemyGen.enemyColor == "red") //red
        {
            animator.SetInteger("EnemyColor", 1);
            enemyColor = "red";
            source.clip = Resources.Load<AudioClip>("enemySmallAttack2");
        }
        else if (enemyGen.enemyColor == "yellow") //yellow
        {
            animator.SetInteger("EnemyColor", 2);
            enemyColor = "yellow";
            source.clip = Resources.Load<AudioClip>("enemySmallAttack3");
        }
        else if (enemyGen.enemyColor == "green") //green
        {
            animator.SetInteger("EnemyColor", 3);
            enemyColor = "green";
            source.clip = Resources.Load<AudioClip>("enemySmallAttack4");
        }
        else if (enemyGen.enemyColor == "orange") //orange
        {
            animator.SetInteger("EnemyColor", 4);
            enemyColor = "orange";
            source.clip = Resources.Load<AudioClip>("enemySmallAttack5");
        }
        else if (enemyGen.enemyColor == "purple") //purple
        {
            animator.SetInteger("EnemyColor", 5);
            enemyColor = "purple";
            source.clip = Resources.Load<AudioClip>("enemySmallAttack6");
        }
        highlight.sprite = Resources.Load<Sprite>(enemyColor + "Target");
        print(enemyColor + " enemy spawned");
    }
}