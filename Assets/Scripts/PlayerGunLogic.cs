using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunLogic : MonoBehaviour
{
    //booleans to keep track of which gun is active. blue is default and will be on the players' hands when they start a level, may be changed in the future
    private bool blueGun = true;
    private bool yellowGun = false;
    private bool redGun = false;

    //the two variables we need in order to make the gun shoot in the shooting script.
    public string fire1Button = "Fire1_P1";
    [HideInInspector]
    public bool fireShot = false;

    void Start()
    {

    }
    
    void Update()
    {
        if (Input.GetAxis(fire1Button) != 0)
        {
            fireShot = true;
        }
    }

    //method that checks if the thing we hit is one of the 3 guns, and if it is, enable that gun
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "BlueGun")
        {
            blueGun = true;
            yellowGun = false;
            redGun = false;
        }
        else if (collision.collider.tag == "YellowGun")
        {
            blueGun = false;
            yellowGun = true;
            redGun = false;
        }
        else if (collision.collider.tag == "RedGun")
        {
            blueGun = false;
            yellowGun = false;
            redGun = true;
        }
    }
}
