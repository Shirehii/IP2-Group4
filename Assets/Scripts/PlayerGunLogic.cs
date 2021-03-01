using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunLogic : MonoBehaviour
{
    private Rigidbody pMrb;
    private GunLogic gL;

    //array to keep track of which gun is active
    private Sprite[] gunSprites;
    [HideInInspector]
    public string selectedGun;

    private SpriteRenderer gunRenderer; //the player's gun sprite renderer, used to change the sprite
    private SpriteRenderer otherGunRenderer; //the other player's gun sprite renderer, used to check what its sprite is

    private AudioClip[] audioClips;

    //the variables we need in order to make the gun shoot and reload in the GunLogic script.
    public string fire1Button = "Fire1_P1";
    public string reloadButton = "Reload_P1";
    [HideInInspector]
    public bool fireShot = false;

    //some more variables for reloading
    private int maxAmmo = 4;
    [HideInInspector]
    public int currentAmmo = 4;

    //variables for abilities
    public string ultimateButton = "Ability_P1";
    private float abilityBar = 0;
    [HideInInspector]
    public bool fireAbility = false;


    void Start()
    {
        //load the gun sprites
        gunSprites = new Sprite[3];
        gunSprites[0] = Resources.Load<Sprite>("blue");
        gunSprites[1] = Resources.Load<Sprite>("red");
        gunSprites[2] = Resources.Load<Sprite>("yellow");

        gL = transform.GetChild(0).GetComponent<GunLogic>();

        //load the audio clips
        audioClips = new AudioClip[3];
        audioClips[0] = Resources.Load<AudioClip>("slimeball");
        audioClips[1] = Resources.Load<AudioClip>("flaunch");
        audioClips[2] = Resources.Load<AudioClip>("rlaunch");

        //get the two sprite renderers of the guns
        gunRenderer = gameObject.transform.Find("HeldGun").GetComponent<SpriteRenderer>(); //this player's
        if (gameObject.tag == "Player1") //the other player's
        {
            otherGunRenderer = GameObject.FindGameObjectWithTag("Player2").gameObject.transform.Find("HeldGun").GetComponent<SpriteRenderer>();
            selectedGun = "blue";
        }
        else if (gameObject.tag == "Player2") //if this player is player 2, then immediatelly swap their gun to the red one, to avoid clashing colors
        {
            otherGunRenderer = GameObject.FindGameObjectWithTag("Player1").gameObject.transform.Find("HeldGun").GetComponent<SpriteRenderer>();
            selectedGun = "red";

            gunRenderer.sprite = gunSprites[1];
            gL.fireRate = 2;
            maxAmmo = 5;
            currentAmmo = maxAmmo;
            gL.source.clip = audioClips[1];
        }

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
            StartCoroutine(ReloadCoroutine()); //reload
        }

        if (Input.GetAxis(ultimateButton) != 0 && abilityBar >= 10)
        {
            fireAbility = true;
            abilityBar = 0;
        }
        else if (abilityBar < 10)
        {
            abilityBar = abilityBar + Time.deltaTime;
        }
    }

    //method that checks if the thing we hit is one of the 3 guns, and if it is, enable that gun
    void OnTriggerEnter(Collider trigger)
    {
        switch (trigger.tag) //Switch statement for switching the gun's color
        {
            case "BlueGun":
                //if statements that stop players from picking the same colored gun
                if (otherGunRenderer.sprite != gunSprites[0])
                {
                    gunRenderer.sprite = gunSprites[0];
                    selectedGun = "blue";
                    gL.fireRate = 1;
                    maxAmmo = 4;
                    gL.source.clip = audioClips[0];
                }
                break;
            case "RedGun":
                if (otherGunRenderer.sprite != gunSprites[1])
                {
                    gunRenderer.sprite = gunSprites[1];
                    selectedGun = "red";
                    gL.fireRate = 2;
                    maxAmmo = 5;
                    gL.source.clip = audioClips[1];
                }
                break;
            case "YellowGun":
                if (otherGunRenderer.sprite != gunSprites[2])
                {
                    gunRenderer.sprite = gunSprites[2];
                    selectedGun = "yellow";
                    gL.fireRate = 3;
                    maxAmmo = 3;
                    gL.source.clip = audioClips[2];
                }
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
