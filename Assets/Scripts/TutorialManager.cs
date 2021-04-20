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

    private GameObject infoBox;
    private Text textBox;

    IEnumerator Start()
    {
        eventManager = GameObject.FindGameObjectWithTag("EventManager");
        enemyGen = GameObject.FindGameObjectWithTag("EnemyGen").GetComponent<GenerateEnemies>();
        infoBox = GameObject.Find("InfoBox");
        textBox = infoBox.GetComponentInChildren<Text>();

        yield return new WaitForSeconds(0.1f);
        eventManager.SetActive(false);
        infoBox.SetActive(false);
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
                StartCoroutine(SyntheticsTutorial());
            }
            else if (tutorialsPassed == 6)
            {
                inTutorial = true;
                StartCoroutine(EventTutorial());
            }
            else if (tutorialsPassed == 7)
            {
                inTutorial = true;
                StartCoroutine(CompleteTutorial());
            }
        }
    }

    IEnumerator WelcomeTutorial()
    {
        yield return new WaitForSeconds(2);
        infoBox.SetActive(true);
        textBox.text = "Welcome to the tutorial for Pain-Teds! In this short level you'll learn the basics of how to play the game. When done, feel free to press P to pause the game and return to the main menu to select another level. Let's get started!";
        yield return new WaitForSeconds(8);
        infoBox.SetActive(false);
        tutorialsPassed = 1;
        inTutorial = false;
    }

    IEnumerator MovementTutorial()
    {
        yield return new WaitForSeconds(2);
        infoBox.SetActive(true);
        textBox.text = "Player 1 on the right moves with the arrow keys. Player 2 on the left uses WASD. Move around!";
        yield return new WaitForSeconds(8);
        infoBox.SetActive(false);
        tutorialsPassed = 2;
        inTutorial = false;
    }

    IEnumerator ShootingTutorial()
    {
        yield return new WaitForSeconds(2);
        infoBox.SetActive(true);
        textBox.text = "In this game, enemies of different colors will spawn and try to attack the crystal you're protecting. Defeat them by shooting them with a gun of the same color! You can change your gun's color by going to the weapon crate at the top of the screen.";
        yield return new WaitForSeconds(8);
        enemyGen.tutorialSpawn = true;
        textBox.text = "Enemies are spawning! Try to defeat them. Remember that you and your teammate can't carry guns of the same color, so try to cooperate!";
        yield return new WaitForSeconds(5);
        infoBox.SetActive(false);
        yield return new WaitForSeconds(14);
        enemyGen.tutorialSpawn = false;
        enemyGen.StopAllCoroutines();
        infoBox.SetActive(true);
        textBox.text = "Good work! Enemies have stopped for now, so let's try something else.";
        yield return new WaitForSeconds(5);
        infoBox.SetActive(false);
        tutorialsPassed = 3;
        inTutorial = false;
    }
    IEnumerator ReloadingTutorial()
    {
        yield return new WaitForSeconds(2);
        infoBox.SetActive(true);
        textBox.text = "After that battle, you must have run low on bullets. Each gun has it's own ammo capacity, and you'll need to replenish every once in a while. To reload ammo, press 8 (Player 1) or R (Player 2).";
        yield return new WaitForSeconds(8);
        textBox.text = "Excellent! Let's teach you two a trick for when you're in a pinch!";
        yield return new WaitForSeconds(3);
        infoBox.SetActive(false);
        tutorialsPassed = 4;
        inTutorial = false;
    }
    IEnumerator AbilityTutorial()
    {
        yield return new WaitForSeconds(2);
        infoBox.SetActive(true);
        textBox.text = "See that light blue bar at the top of the screen? That's your ability bars! When it's full, you can use your abilities, which are different depending on the weapon's color. Try using them now! Player 1 uses 9 and Player 2 uses Space.";
        yield return new WaitForSeconds(8);
        textBox.text = "You guys are naturals! Abilities are a great way to clear multiple enemies of the same color. The blue gun has a grenade, the red gun has a piercing bullet, and the yellow one fires a puddle on the ground.";
        yield return new WaitForSeconds(8);
        infoBox.SetActive(false);
        tutorialsPassed = 5;
        inTutorial = false;
    }
    IEnumerator SyntheticsTutorial()
    {
        yield return new WaitForSeconds(2);
        infoBox.SetActive(true);
        textBox.text = "Are you ready to learn more advanced stuff? The longer you survive, you'll discover that enemies get progressively more difficult to defeat.";
        yield return new WaitForSeconds(5);
        textBox.text = "Synthetic enemies are enemies made up of 2 colors. You don't necessarily have to be an expert in color theory to defeat them though!";
        yield return new WaitForSeconds(5);
        textBox.text = "Green enemies need to be shot by one blue and one yellow bullet. Orange enemies require a red and a yellow shot, and purple enemies, you guessed it, require blue and red.";
        yield return new WaitForSeconds(5);
        textBox.text = "We'll let some more enemies loose for you to practice with, watch out for the synthetics!";
        enemyGen.tutorialSpawn = true;
        yield return new WaitForSeconds(3);
        infoBox.SetActive(false);
        yield return new WaitForSeconds(17);
        infoBox.SetActive(true);
        textBox.text = "Great work! There's only one thing left to teach you now.";
        enemyGen.tutorialSpawn = false;
        enemyGen.StopAllCoroutines();
        yield return new WaitForSeconds(3);
        infoBox.SetActive(false);
        tutorialsPassed = 6;
        inTutorial = false;
    }
    IEnumerator EventTutorial()
    {
        yield return new WaitForSeconds(2);
        infoBox.SetActive(true);
        textBox.text = "The final thing you should watch out for are events. These meteorological phenomena can drag you down. Rain specifically makes it harder to move around the arena, and snow lowers the fire rate of your weapons.";
        eventManager.SetActive(true);
        yield return new WaitForSeconds(8);
        infoBox.SetActive(false);
        eventManager.SetActive(false);
        tutorialsPassed = 7;
        inTutorial = false;
    }
    IEnumerator CompleteTutorial()
    {
        yield return new WaitForSeconds(2);
        infoBox.SetActive(true);
        textBox.text = "And that is all! You are now ready to go out there and defeat some monsters! You can press P to pull up the pause menu and go back.";
        yield return new WaitForSeconds(8);
        infoBox.SetActive(false);
        yield return new WaitForSeconds(20);
        infoBox.SetActive(true);
        textBox.text = "You're still here? You can press P to exit you know. Or maybe you're hoping for some more monsters to kill. That's fine too I guess. Here, I'll let the monsters loose again. Have fun!";
        yield return new WaitForSeconds(10);
        infoBox.SetActive(false);
        eventManager.SetActive(true);
        enemyGen.tutorialSpawn = true;
        tutorialsPassed = 8;
        inTutorial = false;
    }
}
