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
    }
}
