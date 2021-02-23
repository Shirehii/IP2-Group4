using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timerText;

    private float startTime;
    private float t;

    string minutes;
    string seconds;

    void Start()
    {
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        startTime = Time.time;
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
            print("aaaaaaaa");
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
