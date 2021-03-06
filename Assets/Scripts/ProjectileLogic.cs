using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour //this script is used for VARIOUS projectiles, such as the normal bullets and the ability puddles
{
    private float lifespan = 3; //time until the projectile disappears
    private string projectileName;
    public string projectileColor;

    //used to tweak the projectile's sprite
    private PlayerGunLogic pGL; //for the color
    private GunLogic gL; //for the direction
    private SpriteRenderer projectileRenderer;

    BoxCollider boxCollider; //the box collider of the projectile, required if it's a bomb

    private Sprite[] projectileSprites;

    void Start()
    {
        //initialize some variables
        gL = gameObject.transform.parent.gameObject.GetComponent<GunLogic>();
        pGL = gL.pGL;
        projectileRenderer = GetComponent<SpriteRenderer>();

        //load the projectile sprites
        projectileName = gameObject.name.Replace("(Clone)", "");
        projectileSprites = new Sprite[3];
        projectileSprites[0] = Resources.Load<Sprite>("blue" + projectileName);
        projectileSprites[1] = Resources.Load<Sprite>("red" + projectileName);
        projectileSprites[2] = Resources.Load<Sprite>("yellow" + projectileName);

        switch (pGL.selectedGun) //Switch statement for switching the projectile's color
        {
            case "blue":
                projectileColor = "blue";
                projectileRenderer.sprite = projectileSprites[0];
                if (projectileName == "Bullet") //also change the lifespan of the projectile if it is a bullet
                {
                    lifespan = 2;
                }
                break;
            case "red":
                projectileColor = "red";
                projectileRenderer.sprite = projectileSprites[1];
                if (projectileName == "Bullet")
                {
                    lifespan = 2;
                }
                break;
            case "yellow":
                projectileColor = "yellow";
                projectileRenderer.sprite = projectileSprites[2];
                if (projectileName == "Bullet")
                {
                    lifespan = 0.5f;
                }
                break;
        }

        //for flipping the projectile sprite
        if (gL.gunRenderer.flipX)
        {
            projectileRenderer.flipX = true;
        }

        //if it's a bomb, get it's box collider and disable the trigger, will be reactivated when it touches the ground
        if (projectileName == "AbilityBomb")
        {
            boxCollider = GetComponent<BoxCollider>();
            boxCollider.isTrigger = false;
        }

        transform.parent = null; //projectile MUST be unparented after getting the above values, or else its movement will follow the player
        Invoke("DestroyProjectile", lifespan); //after set amount of time, destroy the projectile
    }

    //BOMB related stuff
    private void OnCollisionEnter(Collision other)
    {
        //bomb should explode when touching the ground
        if (projectileName == "AbilityBomb" && other.gameObject.name == "GroundPlane") //if the projectile is a bomb, and it collides with the ground,
        { 
            boxCollider.isTrigger = true; //enable it's trigger collider
            gameObject.tag = "AbilityBomb"; //change its tag so enemies recognise the explosion
            boxCollider.size = new Vector3(3.5f, 3.5f, 3.5f); //expand the collider to simulate the explosion
            //in this line we could add an explosion effect but we don't have the assets :')
            Invoke("DestroyProjectile", 0.1f); //destroy the bomb
        }
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}