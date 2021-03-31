using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject theEnemy;
    private float xPos;
    private float zPos;
    public int maxEnemies = 8;
    private int enemyCount = 0;
    private bool currentlySpawning;
    [HideInInspector]
    public float spawnRate = 5;
    public bool syntheticsEnabled = false;

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
            if (Random.value < 0.3f) //enemy spawn (left side)
            {
                xPos = -6.5f;
                zPos = Random.Range(-2.5f, 6.5f);
            }
            else if (Random.value >= 0.3f && Random.value < 0.6f) //enemy spawn (right side)
            {
                xPos = 6.5f;
                zPos = Random.Range(-2.5f, 6.5f);
            }
            else if (Random.value >= 0.6f) //enemy spawn (bottom side)
            {
                xPos = Random.Range(-6.4f, 6.4f);
                zPos = -2.5f;
            }
            Instantiate(theEnemy, new Vector3(xPos, -1, zPos), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
        currentlySpawning = false;
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