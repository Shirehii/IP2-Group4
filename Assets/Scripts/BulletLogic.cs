using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float speed = 8;
    public float lifespan = 2; //time until the bullet disappears, can be set in inspector

    //the gun this bullet was fired from
    private GameObject parentGun;

    //used to check what sprite the bullet should have
    private PlayerGunLogic pGL;
    private SpriteRenderer bulletRenderer;

    //to get the gun's direction when the bullet is fired
    private bool gunDirection;

    private Sprite[] bulletSprites;

    void Start()
    {
        //initialize some variables
        parentGun = gameObject.transform.parent.gameObject;
        pGL = parentGun.gameObject.transform.parent.GetComponent<PlayerGunLogic>();
        bulletRenderer = GetComponent<SpriteRenderer>();

        //load the bullet sprites
        bulletSprites = new Sprite[3];
        bulletSprites[0] = Resources.Load<Sprite>("bluebullet");
        bulletSprites[1] = Resources.Load<Sprite>("redbullet");
        bulletSprites[2] = Resources.Load<Sprite>("yellowbullet");

        gunDirection = parentGun.GetComponent<SpriteRenderer>().flipX; //check if the sprite is flipped or not

        switch (pGL.selectedGun) //Switch statement for switching the bullet's color
        {
            case "blue":
                bulletRenderer.sprite = bulletSprites[0];
                break;
            case "red":
                bulletRenderer.sprite = bulletSprites[1];
                break;
            case "yellow":
                bulletRenderer.sprite = bulletSprites[2];
                break;
        }

        transform.parent = null; //bullet MUST be unparented after getting the above values, or else its movement will follow the player
        Invoke("DestroyBullet", lifespan); //after set amount of time, destroy the bullet
    }

    void Update()
    {
        if (gunDirection) //if the sprite is flipped
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime); //go left
        }
        else if (!gunDirection) //similarly for right
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}