using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour //this script is used for VARIOUS projectiles, such as the normal bullets and the ability puddles
{
    private float lifespan; //time until the projectile disappears
    private string projectileName;

    //used to tweak the projectile's sprite
    private PlayerGunLogic pGL; //for the color
    private GunLogic gL; //for the direction
    private SpriteRenderer projectileRenderer;

    private Sprite[] projectileSprites;

    void Start()
    {
        //initialize some variables
        gL = gameObject.transform.parent.gameObject.GetComponent<GunLogic>();
        pGL = gameObject.transform.parent.gameObject.transform.parent.GetComponent<PlayerGunLogic>();
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
                projectileRenderer.sprite = projectileSprites[0];
                lifespan = 2;
                break;
            case "red":
                projectileRenderer.sprite = projectileSprites[1];
                lifespan = 2;
                break;
            case "yellow":
                projectileRenderer.sprite = projectileSprites[2];
                lifespan = 0.5f;
                break;
        }

        //for flipping the projectile sprite
        if (gL.gunRenderer.flipX)
        {
            projectileRenderer.flipX = true;
        }

        transform.parent = null; //projectile MUST be unparented after getting the above values, or else its movement will follow the player
        Invoke("DestroyProjectile", lifespan); //after set amount of time, destroy the projectile
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}