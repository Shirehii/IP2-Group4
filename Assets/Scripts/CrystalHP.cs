using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHP : MonoBehaviour
{
    //HP variables
    private float maxHP = 1000;
    private float currentHP;

    //variables for checking how many enemies are attacking
    private CapsuleCollider capsuleCol;
    [HideInInspector]
    public int attackingEnemies;

    //misc variables to make sure everything works properly
    private bool losingHP = false;
    private GenerateEnemies genEnemies;

    //game over UI
    private GameObject gameOverModal;

    void Start()
    {
        //Initialize stuff
        genEnemies = GameObject.FindGameObjectWithTag("EnemyGen").GetComponent<GenerateEnemies>();
        currentHP = maxHP;
        capsuleCol = GetComponent<CapsuleCollider>();
        gameOverModal = GameObject.Find("GameOverModal");
        gameOverModal.SetActive(false); //set the game over ui as inactive at the start
    }

    void Update()
    {
        if (attackingEnemies > genEnemies.maxEnemies) //if for whatever reason the script bugs out and thinks there are more enemies attacking than there ever could be
        {
            attackingEnemies = genEnemies.maxEnemies; //set it to the maximum possible enemies
        }

        //Game Over stuff
        if (currentHP <= 0)
        {
            gameOverModal.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //method for counting how many enemies are attacking, and to trigger LoseHP()
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            attackingEnemies += 1;
        }
        if (!losingHP)
        {
            losingHP = true;
            StartCoroutine(LoseHP());
        }
    }

    //method for losing HP
    IEnumerator LoseHP()
    {
        while (attackingEnemies > 0)
        {
            currentHP -= attackingEnemies;
            yield return new WaitForSeconds(1f);
        }
        losingHP = false;
    }

    public void LoseMoreHP() //when an enemy attacks 3 times, they get destroyed after making a 'big' attack that takes more HP off the crystal
    {
        currentHP -= 3;
    }
}