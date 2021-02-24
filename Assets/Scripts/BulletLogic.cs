using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private float lifespan; //time until the bullet disappears, can be set in inspector

    //used to check what sprite the bullet should have
    private PlayerGunLogic pGL;
    private SpriteRenderer bulletRenderer;

    //to get the gun's direction when the bullet is fired
    private bool gunDirection;

    private Sprite[] bulletSprites;

    void Start()
    {
        //initialize some variables
        pGL = gameObject.transform.parent.gameObject.transform.parent.GetComponent<PlayerGunLogic>();
        bulletRenderer = GetComponent<SpriteRenderer>();

        //load the bullet sprites
        bulletSprites = new Sprite[3];
        bulletSprites[0] = Resources.Load<Sprite>("bluebullet");
        bulletSprites[1] = Resources.Load<Sprite>("redbullet");
        bulletSprites[2] = Resources.Load<Sprite>("yellowbullet");

        switch (pGL.selectedGun) //Switch statement for switching the bullet's color
        {
            case "blue":
                bulletRenderer.sprite = bulletSprites[0];
                lifespan = 2;
                break;
            case "red":
                bulletRenderer.sprite = bulletSprites[1];
                lifespan = 2;
                break;
            case "yellow":
                bulletRenderer.sprite = bulletSprites[2];
                lifespan = 0.5f;
                break;
        }

        transform.parent = null; //bullet MUST be unparented after getting the above values, or else its movement will follow the player
        Invoke("DestroyBullet", lifespan); //after set amount of time, destroy the bullet
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}