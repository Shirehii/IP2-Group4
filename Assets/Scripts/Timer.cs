using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour //timer script. also takes care of game pausing/unpausing because i didn't want to make a separate script haha
{
    private Text timerText;

    private float startTime;
    private float t;

    string minutes;
    string seconds;

    private GenerateEnemies enemyGen;

    //variables below this comment are not directly linked to the timer, but are for pausing the game
    private GameObject PauseModal;

    void Start()
    {
        //get the timer-related variables
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        startTime = Time.time;

        //get the pausing-unpausing related variables
        PauseModal = GameObject.Find("PauseModal");
        PauseModal.SetActive(false);

        enemyGen = GameObject.FindGameObjectWithTag("EnemyGen").GetComponent<GenerateEnemies>();
    }

    void Update()
    {
        //general timer stuff (counting up)
        t = Time.time - startTime;

        minutes = ((int)t / 60).ToString();
        seconds = (t % 60).ToString("f1");

        timerText.text = minutes + ":" + seconds;

        //for pausing the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale > 0)
            {
                PauseModal.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                PauseModal.SetActive(false);
            }
        }

        if (t >= 30 && t < 60)
        {
            enemyGen.spawnRate = 4f;
        }
        else if (t >= 60 && t < 80)
        {
            enemyGen.spawnRate = 3.5f;
        }
        else if (t >= 80 && t < 100)
        {
            enemyGen.spawnRate = 3f;
            enemyGen.syntheticsEnabled = true;
        }
        else if (t >= 100 && t < 120)
        {
            enemyGen.spawnRate = 2.5f;
        }
        else if (t >= 120 && t < 140)
        {
            enemyGen.spawnRate = 2f;
        }
        else if (t >= 140 && t < 160)
        {
            enemyGen.spawnRate = 1.5f;
        }
        else if (t >= 160)
        {
            enemyGen.spawnRate = 1f;
        }

    }
}