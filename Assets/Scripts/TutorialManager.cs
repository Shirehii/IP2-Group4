using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private int tutorialsPassed = 0; //0 is for when the tutorial level has just started up. 1: movement. 2: shooting + enemy colors. 3: reloading. 4: using abilities/items. 5: synthetics. 6: events
    private bool inTutorial = false;

    private GameObject eventManager;
    private GenerateEnemies enemyGen;

    private GameObject middleBox;
    private Text textBoxMiddle;
    private GameObject leftBox;
    private Text textBoxLeft;
    private GameObject rightBox;
    private Text textBoxRight;

    private string inputTextMiddle = "";
    private string inputTextLeft = "";
    private string inputTextRight = "";

    IEnumerator Start()
    {
        eventManager = GameObject.FindGameObjectWithTag("EventManager");
        enemyGen = GameObject.FindGameObjectWithTag("EnemyGen").GetComponent<GenerateEnemies>();
        middleBox = GameObject.Find("MiddleBox");
        leftBox = GameObject.Find("LeftBox");
        rightBox = GameObject.Find("RightBox");
        textBoxMiddle = middleBox.GetComponentInChildren<Text>();
        textBoxLeft = leftBox.GetComponentInChildren<Text>();
        textBoxRight = rightBox.GetComponentInChildren<Text>();

        yield return new WaitForSeconds(0.01f);
        eventManager.SetActive(false);
        middleBox.SetActive(false);
        leftBox.SetActive(false);
        rightBox.SetActive(false);
    }

    void Update()
    {
        if (!inTutorial)
        {
            if (tutorialsPassed == 0)
            {
                inTutorial = true;
                StartCoroutine(WelcomeTutorial());
            }
            else if (tutorialsPassed == 1)
            {
                inTutorial = true;
                StartCoroutine(MovementTutorial());
            }
            else if (tutorialsPassed == 2)
            {
                inTutorial = true;
                StartCoroutine(ShootingTutorial());
            }
            else if (tutorialsPassed == 3)
            {
                inTutorial = true;
                StartCoroutine(ReloadingTutorial());
            }
            else if (tutorialsPassed == 4)
            {
                inTutorial = true;
                StartCoroutine(AbilityTutorial());
            }
            else if (tutorialsPassed == 5)
            {
                inTutorial = true;
                StartCoroutine(EventTutorial());
            }
            else if (tutorialsPassed == 6)
            {
                inTutorial = true;
                StartCoroutine(CompleteTutorial());
            }
        }
    }

    IEnumerator WelcomeTutorial()
    {
        yield return new WaitForSeconds(2);
        middleBox.SetActive(true);
        inputTextMiddle = "Welcome to the tutorial for Pain-Teds! In this short level you'll learn the basics of how to play the game. When done, feel free to press P to pause the game and return to the main menu to select another level.";
        StartCoroutine(PrintText(textBoxMiddle, inputTextMiddle));
        yield return new WaitForSeconds(8);
        middleBox.SetActive(false);
        tutorialsPassed = 1;
        inTutorial = false;
    }

    IEnumerator MovementTutorial()
    {
        yield return new WaitForSeconds(2);
        leftBox.SetActive(true);
        rightBox.SetActive(true);
        inputTextLeft = "P2 use W-A-S-D to move";
        inputTextRight = "P1 use ARROWS to move";
        StartCoroutine(PrintText(textBoxLeft, inputTextLeft));
        StartCoroutine(PrintText(textBoxRight, inputTextRight));
        yield return new WaitForSeconds(8);
        leftBox.SetActive(false);
        rightBox.SetActive(false);
        tutorialsPassed = 2;
        inTutorial = false;
    }

    IEnumerator ShootingTutorial()
    {
        yield return new WaitForSeconds(2);
        enemyGen.tutorialSpawn = true;
        leftBox.SetActive(true);
        rightBox.SetActive(true);
        inputTextLeft = "P2 use R to shoot";
        inputTextRight = "P1 use 7 to shoot";
        StartCoroutine(PrintText(textBoxLeft, inputTextLeft));
        StartCoroutine(PrintText(textBoxRight, inputTextRight));
        yield return new WaitForSeconds(14);
        enemyGen.tutorialSpawn = false;
        enemyGen.StopAllCoroutines();
        leftBox.SetActive(false);
        rightBox.SetActive(false);
        tutorialsPassed = 3;
        inTutorial = false;
    }

    IEnumerator ReloadingTutorial()
    {
        yield return new WaitForSeconds(2);
        leftBox.SetActive(true);
        rightBox.SetActive(true);
        inputTextLeft = "P2 use T to reload";
        inputTextRight = "P1 use 8 to reload";
        StartCoroutine(PrintText(textBoxLeft, inputTextLeft));
        StartCoroutine(PrintText(textBoxRight, inputTextRight));
        yield return new WaitForSeconds(8);
        leftBox.SetActive(false);
        rightBox.SetActive(false);
        tutorialsPassed = 4;
        inTutorial = false;
    }

    IEnumerator AbilityTutorial()
    {
        yield return new WaitForSeconds(2);
        enemyGen.tutorialSpawn = true;
        leftBox.SetActive(true);
        rightBox.SetActive(true);
        inputTextLeft = "P2 use SPACE to use ability";
        inputTextRight = "P1 use 9 to use ability";
        StartCoroutine(PrintText(textBoxLeft, inputTextLeft));
        StartCoroutine(PrintText(textBoxRight, inputTextRight));
        yield return new WaitForSeconds(14);
        leftBox.SetActive(false);
        rightBox.SetActive(false);
        enemyGen.tutorialSpawn = false;
        enemyGen.StopAllCoroutines();
        tutorialsPassed = 5;
        inTutorial = false;
    }

    IEnumerator EventTutorial()
    {
        yield return new WaitForSeconds(2);
        middleBox.SetActive(true);
        inputTextMiddle = "Rain makes it harder to move around the arena, and snow lowers the fire rate of your weapons.";
        StartCoroutine(PrintText(textBoxMiddle, inputTextMiddle));
        eventManager.SetActive(true);
        yield return new WaitForSeconds(8);
        middleBox.SetActive(false);
        eventManager.SetActive(false);
        tutorialsPassed = 6;
        inTutorial = false;
    }

    IEnumerator CompleteTutorial()
    {
        yield return new WaitForSeconds(2);
        middleBox.SetActive(true);
        inputTextMiddle = "That's all! You are now ready to go out there and defeat some monsters! You can press P to pull up the pause menu and go back.";
        StartCoroutine(PrintText(textBoxMiddle, inputTextMiddle));
        yield return new WaitForSeconds(8);
        middleBox.SetActive(false);
        yield return new WaitForSeconds(20);
        middleBox.SetActive(true);
        inputTextMiddle = "You're still here? You can press P to exit, you know. Or maybe you'd like to stay here. That's fine too I guess. But you'll have to fight for your right to stay here. Have fun!";
        StartCoroutine(PrintText(textBoxMiddle, inputTextMiddle));
        yield return new WaitForSeconds(10);
        middleBox.SetActive(false);
        eventManager.SetActive(true);
        enemyGen.tutorialSpawn = true;
        tutorialsPassed = 7;
        inTutorial = false;
    }

    IEnumerator PrintText(Text targetBox, string inputText)
    {
        targetBox.text = "";
        for (int i = 0; i < inputText.Length; i++)
        {
            targetBox.text += inputText[i];
            yield return new WaitForSeconds(0.01f);
        }
    }
}
