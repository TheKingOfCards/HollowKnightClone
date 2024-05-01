using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] int damage = 10; 
    [SerializeField] float pogoForce = 30;
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] Rigidbody2D rb2D;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<EnemyHealth>() != null)
        {
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();

            enemyHealth.TakeDamge(damage);

            if(playerAttack.downStrike)
            {
                rb2D.velocity = new Vector2(0, 0);
                rb2D.AddForce(Vector2.up * pogoForce);
            }
        }
    }
}
