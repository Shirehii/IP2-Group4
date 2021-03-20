using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour //script for pickup items. for items that are attached to the players themselves see PlayerItem.cs
{
    private PlayerMovement pM;
    private BoxCollider boxCol;
    private SpriteRenderer spriteRen;

    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        spriteRen = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 8)
        {
            pM = collider.gameObject.GetComponent<PlayerMovement>();
            StartCoroutine(BuffPlayer());
        }
    }

    IEnumerator BuffPlayer()
    {
        pM.horizontalSpeed *= 2;
        pM.verticalSpeed *= 2;
        boxCol.enabled = false;
        spriteRen.enabled = false;
        yield return new WaitForSeconds(10f);
        pM.horizontalSpeed /= 2;
        pM.verticalSpeed /= 2;
        Destroy(gameObject);
    }
}