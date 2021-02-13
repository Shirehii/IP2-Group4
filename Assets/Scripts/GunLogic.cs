using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    //to get some variables on the gun's state
    public PlayerGunLogic pGL;
    private PlayerMovement pM;

    //the bullet prefab
    public GameObject bullet;

    //time related variables for shooting
    public float fireRate = 2; //the time the gun should be in 'cooldown' after a shot, can be set in inspector
    private float timeBetweenShots; //the time the gun has passed since the last shot

    void Start()
    {
        //get these components from the parent character
        pGL = GetComponentInParent<PlayerGunLogic>();
        pM = GetComponentInParent<PlayerMovement>();
    }
    
    void Update()
    {
        //for shooting
        if (pGL.fireShot == true && timeBetweenShots <= 0) //if player wants to shoot and gun is off cooldown
        {
            FireShot(); //shoot
        }
        else if (timeBetweenShots > 0) //else if it's on cooldown
        {
            timeBetweenShots -= Time.deltaTime; //decrease cooldown and reject the player's input to fire
            pGL.fireShot = false;
        }

        //for flipping the gun around
        if (!pM.facingRight) //if character is facing left
        {
            //flip the sprite, then set it's position to the parent character's + 0.25f
            GetComponent<SpriteRenderer>().flipX = true;
            gameObject.transform.position = gameObject.transform.parent.gameObject.transform.position + new Vector3(-0.25f, 0, 0);
        }
        else if (pM.facingRight) //similarly for facing right
        {
            GetComponent<SpriteRenderer>().flipX = false;
            gameObject.transform.position = gameObject.transform.parent.gameObject.transform.position + new Vector3(0.25f, 0, 0);
        }
    }

    //method for shooting bullets
    void FireShot()
    {
        Instantiate(bullet, gameObject.transform); //instantiate a bullet at the gun's position
        timeBetweenShots = fireRate; //put the gun on cooldown
        pGL.fireShot = false;
    }
}