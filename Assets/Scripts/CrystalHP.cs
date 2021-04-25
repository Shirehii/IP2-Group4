using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHP : MonoBehaviour
{
    //HP variables
    public float maxHP = 1000;
    [HideInInspector]
    public float currentHP;

    //game over UI
    private GameObject gameOverModal;

    private Sprite[] crystalSprites;
    private SpriteRenderer spriteRen;

    private bool crystalDestroyed = false;

    private AudioSource source;
    private AudioClip breakSound;

    void Start()
    {
        //Initialize stuff
        currentHP = maxHP;
        gameOverModal = GameObject.Find("GameOverModal");
        gameOverModal.SetActive(false); //set the game over ui as inactive at the start

        crystalSprites = new Sprite[3];
        crystalSprites = Resources.LoadAll<Sprite>("Crystals");
        spriteRen = GetComponent<SpriteRenderer>();

        source = GetComponent<AudioSource>();
        breakSound = Resources.Load<AudioClip>("crystalBreak");
    }

    void Update()
    {
        //Sprite changes & Game Over stuff
        if (currentHP > maxHP * 0.7)
        {
            spriteRen.sprite = crystalSprites[0];
        }
        else if (currentHP <= maxHP * 0.7 && currentHP > maxHP * 0.3)
        {
            spriteRen.sprite = crystalSprites[1];
        }
        else if (currentHP <= maxHP * 0.3 && currentHP > 0)
        {
            spriteRen.sprite = crystalSprites[2];
        }
        else if (currentHP <= 0 && !crystalDestroyed)
        {
            crystalDestroyed = true;
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        source.PlayOneShot(breakSound);
        gameOverModal.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0;
    }
}