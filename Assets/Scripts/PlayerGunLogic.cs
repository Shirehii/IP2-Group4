using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunLogic : MonoBehaviour
{
    //array to keep track of which gun is active. blue is default and will be on the players' hands when they start a level, may be changed in the future
    private Sprite[] gunSprites;

    private SpriteRenderer gunRenderer; 

    //the two variables we need in order to make the gun shoot in the GunLogic script.
    public string fire1Button = "Fire1_P1";
    [HideInInspector]
    public bool fireShot = false;

    void Start()
    {
        gunSprites = new Sprite[3];
        gunSprites[0] = Resources.Load<Sprite>("blue");
        gunSprites[1] = Resources.Load<Sprite>("red");
        gunSprites[2] = Resources.Load<Sprite>("yellow");

        gunRenderer = gameObject.transform.Find("TestGun").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetAxis(fire1Button) != 0) //if the player pressed the button to fire
        {
            fireShot = true; //then send the signal to the gun to fire
        }
    }

    //method that checks if the thing we hit is one of the 3 guns, and if it is, enable that gun
    void OnTriggerEnter(Collider trigger)
    {
        switch (trigger.tag) //Switch statement for switching the gun's color
        {
            case "BlueGun":
                gunRenderer.sprite = gunSprites[0];
                break;
            case "RedGun":
                gunRenderer.sprite = gunSprites[1];
                break;
            case "YellowGun":
                gunRenderer.sprite = gunSprites[2];
                break;
        }
    }
}
