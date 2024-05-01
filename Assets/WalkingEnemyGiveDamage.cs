using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyGiveDamage : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<PlayerHealth>() != null)
        {
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();

            playerHealth.TakeDamage();
        }
    }
}
