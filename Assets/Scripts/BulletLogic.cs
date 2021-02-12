using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float speed = 8;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}