using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEvents : MonoBehaviour
{
    private PlayerMovement pM1;
    private PlayerMovement pM2;

    private bool running = false;

    void Start()
    {
        pM1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerMovement>();
        pM2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerMovement>();

        running = true;
        StartCoroutine(StartEvents());
    }

    void Update()
    {
        if (!running)
        {
            running = true;
            StartCoroutine(StartEvents());
        }
    }

    IEnumerator StartEvents()
    {
        yield return new WaitForSeconds(10f);
        pM1.horizontalSpeed /= 2;
        pM1.verticalSpeed /= 2;
        pM2.horizontalSpeed /= 2;
        pM2.verticalSpeed /= 2;
        yield return new WaitForSeconds(10f);
        pM1.horizontalSpeed *= 2;
        pM1.verticalSpeed *= 2;
        pM2.horizontalSpeed *= 2;
        pM2.verticalSpeed *= 2;

        running = false;
    }
}