using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    //to get some variables on the gun's state
    public PlayerGunLogic pGL;

    //child bullet, specific to the player
    public GameObject bullet;
    
    void Start()
    {
        pGL = GetComponentInParent<PlayerGunLogic>();
    }
    
    void Update()
    {
        if (pGL.fireShot == true)
        {
            FireShot();
            pGL.fireShot = false;
        }
    }

    void FireShot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}