using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour //for items attached to the players. for pickup items see Item.cs
{
    private string useItemButton = "UseItem_P1";

    private PlayerMovement pM;

    public int cooldown = 20; //the cooldown of items, can be set in inspector
    private float timeBetweenUses; //the time that has passed between uses of the item

    void Start()
    {
        if (gameObject.tag == "Player2") //if the player is not player 1, change the input axis
        {
            useItemButton = "UseItem_P2";
        }

        pM = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetAxis(useItemButton) != 0 && timeBetweenUses <= 0)
        {
            StartCoroutine(UseItem());
        }
        else if (timeBetweenUses > 0) //else if it's on cooldown
        {
            timeBetweenUses -= Time.deltaTime;
        }
    }

    IEnumerator UseItem()
    {
        timeBetweenUses = cooldown; //put the item on cooldown
        pM.horizontalSpeed *= 2;
        pM.verticalSpeed *= 2;
        yield return new WaitForSeconds(10f);
        pM.horizontalSpeed /= 2;
        pM.verticalSpeed /= 2;
    }
}