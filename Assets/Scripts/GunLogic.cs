using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour //this script is used for GENERAL gun logic that applies to ALL guns, for gun properties specific to the players, use PlayerGunLogic
{
    //to get some variables on the gun's state
    public PlayerGunLogic pGL;
    private PlayerMovement pM;

    //the projectile prefabs, need to be set in the inspector
    public GameObject bullet;
    public GameObject puddle;

    //time related variables for shooting
    public float fireRate = 1; //the time the gun should be in 'cooldown' after a shot, can be set in inspector
    private float timeBetweenShots; //the time the gun has passed since the last shot

    public AudioSource source;

    private bool gunDirection;
    [HideInInspector]
    public SpriteRenderer gunRenderer;

    private void Awake()
    {
        gunRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //get these components from the parent character
        pGL = GetComponentInParent<PlayerGunLogic>();
        pM = GetComponentInParent<PlayerMovement>();

        source = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        //for shooting
        if (pGL.fireShot == true && timeBetweenShots <= 0) //if player wants to shoot and gun is off cooldown
        {
            gunDirection = gunRenderer.flipX; //check if the sprite is flipped or not
            FireShot(); //shoot
            pGL.currentAmmo -= 1;
            source.Play();
        }
        else if (timeBetweenShots > 0) //else if it's on cooldown
        {
            timeBetweenShots -= Time.deltaTime; //decrease cooldown and reject the player's input to fire
            pGL.fireShot = false;
        }

        //for ability use
        if (pGL.fireAbility == true)
        {
            gunDirection = gunRenderer.flipX; //check if the sprite is flipped or not
            FireAbility();
            pGL.fireAbility = false;
        }

        //for flipping the gun around
        if (!pM.facingRight) //if character is facing left
        {
            //flip the sprite, then set it's position to the parent character's + 0.25f
            gunRenderer.flipX = true;
            gameObject.transform.position = gameObject.transform.parent.gameObject.transform.position + new Vector3(-0.25f, 0, 0);
        }
        else if (pM.facingRight) //similarly for facing right
        {
            gunRenderer.flipX = false;
            gameObject.transform.position = gameObject.transform.parent.gameObject.transform.position + new Vector3(0.25f, 0, 0);
        }
    }

    //method for shooting bullets
    void FireShot()
    {
        timeBetweenShots = fireRate; //put the gun on cooldown
        pGL.fireShot = false;

        //this is where the shooting magic happens

        if (pGL.selectedGun == "blue" || pGL.selectedGun == "red") //single bullet shots
        {
            GameObject spawnedBullet = Instantiate(bullet, gameObject.transform); //instantiate a bullet at the gun's position, and get it
            if (gunDirection)
            {
                spawnedBullet.GetComponent<Rigidbody>().AddForce(-500, 0, 0); //shoot left
            }
            else if (!gunDirection)
            {
                spawnedBullet.GetComponent<Rigidbody>().AddForce(500, 0, 0); //shoot right
            }
        }
        else if (pGL.selectedGun == "yellow") //spread pattern shots
        {
            for (int i = 0; i <= 2; i++) //loop 3 times
            {
                GameObject spawnedBullet = Instantiate(bullet, gameObject.transform); //instantiate a bullet at the gun's position, and get it

                switch (i)
                {
                    case 0:
                        if (gunDirection)
                        {
                            spawnedBullet.GetComponent<Rigidbody>().AddForce(-500, 100, 0);
                        }
                        else if (!gunDirection)
                        {
                            spawnedBullet.GetComponent<Rigidbody>().AddForce(500, 100, 0);
                        }
                        break;
                    case 1:
                        if (gunDirection)
                        {
                            spawnedBullet.GetComponent<Rigidbody>().AddForce(-500, 0, 0);
                        }
                        else if (!gunDirection)
                        {
                            spawnedBullet.GetComponent<Rigidbody>().AddForce(500, 0, 0);
                        }
                        break;
                    case 2:
                        if (gunDirection)
                        {
                            spawnedBullet.GetComponent<Rigidbody>().AddForce(-500, -100, 0);
                        }
                        else if (!gunDirection)
                        {
                            spawnedBullet.GetComponent<Rigidbody>().AddForce(500, -100, 0);
                        }
                        break;
                }
            }
        }
    }

    void FireAbility()
    {
        GameObject spawnedPuddle = Instantiate(puddle, gameObject.transform);
        if (gunDirection)
        {
            spawnedPuddle.transform.position = new Vector3(spawnedPuddle.transform.position.x - 3, 0.21f, spawnedPuddle.transform.position.z);
        }
        else
        {
            spawnedPuddle.transform.position = new Vector3(spawnedPuddle.transform.position.x + 3, 0.21f, spawnedPuddle.transform.position.z);
        }
    }
}