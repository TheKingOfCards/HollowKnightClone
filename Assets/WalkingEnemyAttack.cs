using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyAttack : MonoBehaviour
{
    Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            
        }
    }
}
