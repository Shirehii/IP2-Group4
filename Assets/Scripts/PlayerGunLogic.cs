using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunLogic : MonoBehaviour
{
    private Rigidbody pMrb;
    private GunLogic gL;
    private Animator animator;

    [HideInInspector]
    public string selectedGun;

    private SpriteRenderer gunRenderer; //the player's gun sprite renderer, used to change the sprite
    private PlayerGunLogic otherpGL; //the other player's gun sprite renderer, used to check what its sprite is

    //private AudioClip[] audioClips;

    //the variables we need in order to make the gun shoot and reload in the GunLogic script.
    private string fire1Button = "Fire1_P1";
    private string reloadButton = "Reload_P1";
    [HideInInspector]
    public bool fireShot = false;

    private bool reloading = false;

    //some more variables for reloading
    [HideInInspector]
    public int maxAmmo = 4;
    public int currentAmmo = 4;

    //variables for abilities
    private string abilityButton = "Ability_P1";
    [HideInInspector]
    public float abilityBar = 0;
    public bool fireAbility = false;

    public float scoreMultiplier = 1; //used in projectilelogic

    private AudioSource source;

    void Start()
    {
        if (gameObject.tag == "Player2") //if the player is not player 1, change the input axis
        {
            fire1Button = "Fire1_P2";
            reloadButton = "Reload_P2";
            abilityButton = "Ability_P2";
        }

        gL = transform.GetChild(0).GetComponent<GunLogic>();

        source = GetComponent<AudioSource>();
        //load the audio clips
        //audioClips = new AudioClip[3];
        //audioClips[0] = Resources.Load<AudioClip>("slimeball");
        //audioClips[1] = Resources.Load<AudioClip>("flaunch");
        //audioClips[2] = Resources.Load<AudioClip>("rlaunch");
        
        animator = GetComponent<Animator>();

        //get the two sprite renderers of the guns
        if (gameObject.tag == "Player1") //player 1's
        {
            gunRenderer = gameObject.transform.Find("GunBarrel1").GetComponent<SpriteRenderer>();
            otherpGL = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerGunLogic>();
            selectedGun = "blue";
        }
        else if (gameObject.tag == "Player2") //if this player is player 2, then immediatelly swap their gun to the red one, to avoid clashing colors
        {
            gunRenderer = gameObject.transform.Find("GunBarrel2").GetComponent<SpriteRenderer>();
            otherpGL = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerGunLogic>();
            selectedGun = "red";
            animator.SetInteger("gunColor", 1);

            gL.fireRate = 2;
            maxAmmo = 5;
            currentAmmo = maxAmmo;
            //gL.source.clip = audioClips[1];
        }

        //get the rigidbody from PlayerMovement
        pMrb = GetComponent<PlayerMovement>().rb;
    }

    void Update()
    {
        if (Input.GetAxis(fire1Button) != 0 && currentAmmo > 0 && !reloading) //if the player pressed the button to fire and they have ammo
        {
            fireShot = true; //then send the signal to the gun to fire
        }

        if (Input.GetAxis(reloadButton) != 0 && currentAmmo < maxAmmo && !reloading) //if the player pressed the reload button and they aren't topped off already
        {
            reloading = true;
            StartCoroutine(ReloadCoroutine()); //reload
        }

        if (Input.GetAxis(abilityButton) != 0 && abilityBar >= 10)
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
                //the if statements stop players from picking the same colored gun
                if (otherpGL.selectedGun != "blue")
                {
                    selectedGun = "blue";
                    animator.SetInteger("gunColor", 0);
                    gL.fireRate = 1;
                    maxAmmo = 4;
                    source.Play();
                    //gL.source.clip = audioClips[0];
                }
                break;
            case "RedGun":
                if (otherpGL.selectedGun != "red")
                {
                    selectedGun = "red";
                    animator.SetInteger("gunColor", 1);
                    gL.fireRate = 2;
                    maxAmmo = 5;
                    source.Play();
                    //gL.source.clip = audioClips[1];
                }
                break;
            case "YellowGun":
                if (otherpGL.selectedGun != "yellow")
                {
                    selectedGun = "yellow";
                    animator.SetInteger("gunColor", 2);
                    gL.fireRate = 3;
                    maxAmmo = 3;
                    source.Play();
                    //gL.source.clip = audioClips[2];
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
        animator.SetBool("shouldReload", true); //reload animation, needs two bools or else it doesn't animate properly
        animator.SetBool("isReloading", false); 
        for (int i = currentAmmo; i != maxAmmo; i++) //reload
        {
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("isReloading", true);
            yield return new WaitForSeconds(0.9f);
            currentAmmo += 1;
        }
        pMrb.isKinematic = false; //and allow them to move again
        animator.SetBool("shouldReload", false); //stop reload animation
        animator.SetBool("isReloading", false); 
        reloading = false;
    }
}
