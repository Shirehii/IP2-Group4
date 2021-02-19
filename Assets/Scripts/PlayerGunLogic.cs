using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunLogic : MonoBehaviour
{
    private Rigidbody pMrb;
    private GunLogic gL;

    //array to keep track of which gun is active. blue is default and will be on the players' hands when they start a level, may be changed in the future
    private Sprite[] gunSprites;

    private SpriteRenderer gunRenderer;

    private AudioClip[] audioClips;

    //the variables we need in order to make the gun shoot and reload in the GunLogic script.
    public string fire1Button = "Fire1_P1";
    public string reloadButton = "Reload_P1";
    [HideInInspector]
    public bool fireShot = false;

    //some more varibales for reloading
    private int maxAmmo = 4;
    [HideInInspector]
    public int currentAmmo = 4;

    void Start()
    {
        //load the gun sprites
        gunSprites = new Sprite[3];
        gunSprites[0] = Resources.Load<Sprite>("blue");
        gunSprites[1] = Resources.Load<Sprite>("red");
        gunSprites[2] = Resources.Load<Sprite>("yellow");

        gunRenderer = gameObject.transform.Find("TestGun").GetComponent<SpriteRenderer>();

        gL = transform.GetChild(0).GetComponent<GunLogic>();

        //load the audio clips
        audioClips = new AudioClip[3];
        audioClips[0] = Resources.Load<AudioClip>("slimeball");
        audioClips[1] = Resources.Load<AudioClip>("flaunch");
        audioClips[2] = Resources.Load<AudioClip>("rlaunch");

        //get the rigidbody from PlayerMovement
        pMrb = GetComponent<PlayerMovement>().rb;
    }

    void Update()
    {
        if (Input.GetAxis(fire1Button) != 0 && currentAmmo > 0) //if the player pressed the button to fire and they have ammo
        {
            fireShot = true; //then send the signal to the gun to fire
        }

        if (Input.GetAxis(reloadButton) != 0 && currentAmmo < maxAmmo) //if the player pressed the reload button and they aren't topped off already
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    //method that checks if the thing we hit is one of the 3 guns, and if it is, enable that gun
    void OnTriggerEnter(Collider trigger)
    {
        switch (trigger.tag) //Switch statement for switching the gun's color
        {
            case "BlueGun":
                gunRenderer.sprite = gunSprites[0];
                gL.fireRate = 1;
                maxAmmo = 4;
                gL.source.clip = audioClips[0];
                break;
            case "RedGun":
                gunRenderer.sprite = gunSprites[1];
                gL.fireRate = 2;
                maxAmmo = 5;
                gL.source.clip = audioClips[1];
                break;
            case "YellowGun":
                gunRenderer.sprite = gunSprites[2];
                gL.fireRate = 3;
                maxAmmo = 3;
                gL.source.clip = audioClips[2];
                break;
        }

        if (currentAmmo > maxAmmo) //always run this check after swapping guns
        {
            currentAmmo = maxAmmo;
        }
    }

    //reloading method
    IEnumerator ReloadCoroutine()
    {
        pMrb.isKinematic = true; //stop the character
        yield return new WaitForSeconds((maxAmmo - currentAmmo)/2); //wait
        currentAmmo = maxAmmo; //reload
        pMrb.isKinematic = false; //and allow them to move again
    }
}
