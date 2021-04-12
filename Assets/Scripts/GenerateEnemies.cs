using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject theEnemy;
    public GameObject arrow;
    private float xPos;
    private float zPos;
    public int maxEnemies = 8;
    private int enemyCount = 0;
    private bool currentlySpawning;
    private GameObject indicatorArrow;
    private Animator indArrowAnimator;
    private Quaternion arrowRotation;
    [HideInInspector]
    public float spawnRate = 5;
    [HideInInspector]
    public bool syntheticsEnabled = false;
    [HideInInspector]
    public string enemyColor;

    private CrystalHP crystalHP;

    void Start()
    {
        crystalHP = GameObject.FindGameObjectWithTag("Crystal").GetComponent<CrystalHP>();

        currentlySpawning = true;
        StartCoroutine(EnemyDrop());
    }

    void Update()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        }
    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < maxEnemies)
        {
            enemyCount += 1;
            int value = Random.Range(0, 3);
            if (value == 0) //enemy spawn (left side)
            {
                xPos = -6.5f;
                zPos = Random.Range(-2.5f, 6.5f);
                arrowRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (value == 1) //enemy spawn (right side)
            {
                xPos = 6.5f;
                zPos = Random.Range(-2.5f, 6.5f);
                arrowRotation = Quaternion.Euler(0, 0, 180);
            }
            else if (value == 2) //enemy spawn (bottom side)
            {
                xPos = Random.Range(-6.4f, 6.4f);
                zPos = -2.5f;
                arrowRotation = Quaternion.Euler(0, 0, 90);
            }

            indicatorArrow = Instantiate(arrow, new Vector3(xPos, -1, zPos), arrowRotation);
            indArrowAnimator = indicatorArrow.GetComponent<Animator>();
            DetermineEnemyColor();
            yield return new WaitForSeconds(3f);
            Destroy(indicatorArrow);

            GameObject enemy = Instantiate(theEnemy, new Vector3(xPos, -1, zPos), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
        currentlySpawning = false;
    }

    private void DetermineEnemyColor()
    {
        int value = Random.Range(0, 6);
        if (value == 0) //blue
        {
            enemyColor = "blue";
            indArrowAnimator.SetInteger("colorNumber", 0);
        }
        else if (value == 1) //red
        {
            enemyColor = "red";
            indArrowAnimator.SetInteger("colorNumber", 1);
        }
        else if (value == 2) //yellow
        {
            enemyColor = "yellow";
            indArrowAnimator.SetInteger("colorNumber", 2);
        }
        else if (value == 3 && syntheticsEnabled) //green
        {
            enemyColor = "green";
            indArrowAnimator.SetInteger("colorNumber", 3);
        }
        else if (value == 4 && syntheticsEnabled) //orange
        {
            enemyColor = "orange";
            indArrowAnimator.SetInteger("colorNumber", 4);
        }
        else if (value == 5 && syntheticsEnabled) //purple
        {
            enemyColor = "purple";
            indArrowAnimator.SetInteger("colorNumber", 5);
        }
        print(enemyColor);
        print(indArrowAnimator);
        print(indArrowAnimator.GetInteger("colorNumber"));

        if (enemyColor == null || enemyColor == "" || enemyColor == " " || (value != 0 && indArrowAnimator.GetInteger("colorNumber") == 0)) //rerun the code in the chance it turns up as null
        {
            DetermineEnemyColor();
        }
    }

    public void EnemyDied() //method that gets triggered from Enemy.cs
    {
        if (enemyCount - 1 < maxEnemies && !currentlySpawning)
        {
            enemyCount -= 1;
            currentlySpawning = true;
            StartCoroutine(EnemyDrop());
        }
    }
}