using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    private Image selectedGunColor;
    private Transform abilityBar;

    private PlayerGunLogic pGL;
    private PlayerItem pI;
    
    private Sprite[] headIconSprites;
    private Sprite[] ammoSprites;
    private GameObject[] ammoObjects;
    private Image[] ammoObjectsImage;
    private Image itemImage;

    void Start()
    {
        //get the player scripts to check their values
        ammoObjects = new GameObject[6];
        ammoObjectsImage = new Image[6];
        headIconSprites = new Sprite[3];
        if (gameObject.name.Contains("1"))
        {
            pGL = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerGunLogic>();
            pI = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerItem>();
            
            headIconSprites[0] = Resources.Load<Sprite>("Player1Blue");
            headIconSprites[1] = Resources.Load<Sprite>("Player1Red");
            headIconSprites[2] = Resources.Load<Sprite>("Player1Yellow");

            //get the individual UI components
            selectedGunColor = GameObject.Find("HeadIcon1").GetComponent<Image>();

            abilityBar = GameObject.Find("AbilityBar1").transform;
            
            for (int i = 0; i != 6; i++)
            {
                ammoObjects[i] = GameObject.Find("Ammo1 (" + i + ")");
                ammoObjectsImage[i] = ammoObjects[i].GetComponent<Image>();
            }

            itemImage = GameObject.Find("ItemImage1").GetComponent<Image>();
        }
        else if (gameObject.name.Contains("2"))
        {
            pGL = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerGunLogic>();
            pI = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerItem>();

            headIconSprites[0] = Resources.Load<Sprite>("Player2Blue");
            headIconSprites[1] = Resources.Load<Sprite>("Player2Red");
            headIconSprites[2] = Resources.Load<Sprite>("Player2Yellow");

            //get the individual UI components
            selectedGunColor = GameObject.Find("HeadIcon2").GetComponent<Image>();

            abilityBar = GameObject.Find("AbilityBar2").transform;
            
            for (int i = 0; i != 6; i++)
            {
                ammoObjects[i] = GameObject.Find("Ammo2 (" + i + ")");
                ammoObjectsImage[i] = ammoObjects[i].GetComponent<Image>();
            }

            itemImage = GameObject.Find("ItemImage2").GetComponent<Image>();
        }

        ammoSprites = new Sprite[3];
        ammoSprites[0] = Resources.Load<Sprite>("blueAmmo");
        ammoSprites[1] = Resources.Load<Sprite>("redAmmo");
        ammoSprites[2] = Resources.Load<Sprite>("yellowAmmo");
    }
    
    void Update()
    {
        //player item UI
        if (pI.timeBetweenUses <= 0)
        {
            itemImage.enabled = true;
        }
        else if (pI.timeBetweenUses > 0)
        {
            itemImage.enabled = false;
        }

        //change the selected gun UI && ammo UI color if they don't match the selected gun's color
        if (pGL.selectedGun == "blue" && selectedGunColor != headIconSprites[0])
        {
            selectedGunColor.sprite = headIconSprites[0];
            for (int i = 0; i != 6; i++)
            {
                ammoObjectsImage[i].sprite = ammoSprites[0];
            }
        }
        else if (pGL.selectedGun == "red" && selectedGunColor != headIconSprites[1])
        {
            selectedGunColor.sprite = headIconSprites[1];
            for (int i = 0; i != 6; i++)
            {
                ammoObjectsImage[i].sprite = ammoSprites[1];
            }
        }
        else if (pGL.selectedGun == "yellow" && selectedGunColor != headIconSprites[2])
        {
            selectedGunColor.sprite = headIconSprites[2];
            for (int i = 0; i != 6; i++)
            {
                ammoObjectsImage[i].sprite = ammoSprites[2];
            }
        }

        //change the scale of the ability bar based on its cooldown from pGL
        if (pGL.abilityBar < 10)
        {
            Vector3 temp = transform.localScale;
            temp.x = pGL.abilityBar / 10;
            temp.y = 2;
            abilityBar.localScale = temp;

            //abilityBar.localScale.x = pGL.abilityBar / 10;
            //i wanted to just write the line above but transform.localScale can't be set so i had to use a temporary variable
        }
        else if (pGL.abilityBar >= 10)
        {
            abilityBar.localScale = new Vector3(1, 2, 1);
        }

        //switch statement for adjusting the quantity of the ammo ui
        
        //a for statement may be more efficient, will work on this later
        //for (int i = 0; i < pGL.currentAmmo; i++)
        //{
        //    ammoObjects[i].SetActive
        //}
        switch (pGL.currentAmmo)
        {
            case (0):
                ammoObjects[0].SetActive(false);
                ammoObjects[1].SetActive(false);
                ammoObjects[2].SetActive(false);
                ammoObjects[3].SetActive(false);
                ammoObjects[4].SetActive(false);
                ammoObjects[5].SetActive(false);
                break;
            case (1):
                ammoObjects[0].SetActive(true);
                ammoObjects[1].SetActive(false);
                ammoObjects[2].SetActive(false);
                ammoObjects[3].SetActive(false);
                ammoObjects[4].SetActive(false);
                ammoObjects[5].SetActive(false);
                break;
            case (2):
                ammoObjects[0].SetActive(true);
                ammoObjects[1].SetActive(true);
                ammoObjects[2].SetActive(false);
                ammoObjects[3].SetActive(false);
                ammoObjects[4].SetActive(false);
                ammoObjects[5].SetActive(false);
                break;
            case (3):
                ammoObjects[0].SetActive(true);
                ammoObjects[1].SetActive(true);
                ammoObjects[2].SetActive(true);
                ammoObjects[3].SetActive(false);
                ammoObjects[4].SetActive(false);
                ammoObjects[5].SetActive(false);
                break;
            case (4):
                ammoObjects[0].SetActive(true);
                ammoObjects[1].SetActive(true);
                ammoObjects[2].SetActive(true);
                ammoObjects[3].SetActive(true);
                ammoObjects[4].SetActive(false);
                ammoObjects[5].SetActive(false);
                break;
            case (5):
                ammoObjects[0].SetActive(true);
                ammoObjects[1].SetActive(true);
                ammoObjects[2].SetActive(true);
                ammoObjects[3].SetActive(true);
                ammoObjects[4].SetActive(true);
                ammoObjects[5].SetActive(false);
                break;
            case (6):
                ammoObjects[0].SetActive(true);
                ammoObjects[1].SetActive(true);
                ammoObjects[2].SetActive(true);
                ammoObjects[3].SetActive(true);
                ammoObjects[4].SetActive(true);
                ammoObjects[5].SetActive(true);
                break;
        }
    }
}