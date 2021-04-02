using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEvents : MonoBehaviour
{
    private PlayerMovement pM1;
    private PlayerMovement pM2;
    private GunLogic gL1;
    private GunLogic gL2;
    private BoxCollider box1;
    private BoxCollider box2;

    public PhysicMaterial semiSlip;
    public PhysicMaterial verySlip;

    private Animator animator;
    public string whichEvent; //string that shows in the inspector which event is currently active, for debug purposes

    private bool running = false;

    void Start()
    {
        pM1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerMovement>();
        pM2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerMovement>();
        gL1 = GameObject.Find("GunBarrel1").GetComponent<GunLogic>();
        gL2 = GameObject.Find("GunBarrel2").GetComponent<GunLogic>();
        box1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<BoxCollider>();
        box2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<BoxCollider>();

        animator = GetComponent<Animator>();
        
        PickEvent();
    }

    void Update()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        if (!running)
        {
            running = true;
            PickEvent();
        }
    }

    void PickEvent()
    {
        if (Random.value < 0.5f)
        {
            StartCoroutine(RainEvent());
        }
        else if (Random.value >= 0.5f)
        {
            StartCoroutine(SnowEvent());
        }
        //else if (Random.value >= 0.6f)
        //{
        //    StartCoroutine(HailEvent());
        //}
    }

    IEnumerator RainEvent() //rain event slows the players' speed for a few seconds
    {
        yield return new WaitForSeconds(10f);
        whichEvent = "rain";
        animator.SetBool("isRaining", true);
        pM1.horizontalSpeed /= 2;
        pM1.verticalSpeed /= 2;
        pM2.horizontalSpeed /= 2;
        pM2.verticalSpeed /= 2;
        yield return new WaitForSeconds(10f);
        animator.SetBool("isRaining", false);
        pM1.horizontalSpeed *= 2;
        pM1.verticalSpeed *= 2;
        pM2.horizontalSpeed *= 2;
        pM2.verticalSpeed *= 2;
        whichEvent = "none";

        running = false;
    }

    IEnumerator SnowEvent() //snow event slows the guns' fire rate for a few seconds
    {
        yield return new WaitForSeconds(10f);
        whichEvent = "snow";
        animator.SetBool("isSnowing", true);
        gL1.fireRate *= 2;
        gL2.fireRate *= 2;
        yield return new WaitForSeconds(10f);
        animator.SetBool("isSnowing", true);
        gL1.fireRate /= 2;
        gL2.fireRate /= 2;
        whichEvent = "none";

        running = false;
    }

    IEnumerator HailEvent() //hail event makes the ground slippery for a few seconds, had trouble adding this one in, might try again some other time
    {
        yield return new WaitForSeconds(10f);
        whichEvent = "hail";
        yield return new WaitForSeconds(10f);
        whichEvent = "none";

        running = false;
    }
}