using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour //for items attached to the players. for pickup items see Item.cs
{
    private string useItemButton = "UseItem_P1";
    public string itemType = "MovementBuff"; //available types: MovementBuff, ScoreMultiplier | for now, player 1 can only have movement, and player 2 score

    private PlayerMovement pM;
    private PlayerGunLogic pGL;

    public int cooldown = 20; //the cooldown of items, can be set in inspector
    [HideInInspector]
    public float timeBetweenUses; //the time that has passed between uses of the item

    void Start()
    {
        if (gameObject.tag == "Player2") //if the player is not player 1, change the input axis
        {
            useItemButton = "UseItem_P2";
            //itemType = "ScoreMultiplier";
        }

        pM = GetComponent<PlayerMovement>();
        pGL = GetComponent<PlayerGunLogic>();
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
        if (itemType == "MovementBuff")
        {
            pM.horizontalSpeed *= 2;
            pM.verticalSpeed *= 2;
            yield return new WaitForSeconds(10f);
            pM.horizontalSpeed /= 2;
            pM.verticalSpeed /= 2;
        }
        else if (itemType == "ScoreMultiplier")
        {
            pGL.scoreMultiplier = 1.5f;
            yield return new WaitForSeconds(10f);
            pGL.scoreMultiplier = 1;
        }
    }
}