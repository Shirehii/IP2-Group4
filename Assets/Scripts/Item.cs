using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
  void OnTriggerEnter(Collider collider)
  {
        if(collider.gameObject.tag == "Player1" || collider.gameObject.tag == "Player2")
        {
            print("Item picked up");
            ScoreText.scoreValue += 10;
            Destroy(gameObject);
        }
  }
}
