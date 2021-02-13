using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float speed = 8;
    public float lifespan = 2; //time until the bullet disappears, can be set in inspector

    //to get the gun's direction when the bullet is fired
    private bool gunDirection;

    void Start()
    {
        gunDirection = gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().flipX; //check if the sprite is flipped or not
        transform.parent = null;
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