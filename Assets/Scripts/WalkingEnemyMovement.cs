using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WalkingEnemyMovement : MonoBehaviour
{
    [Header("[Movement Settings]")]
    [SerializeField] float speed = 2;
    [SerializeField] Transform wallDropCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float checkSize = 0.2f;

    [Header("[Patrol Settings]")]
    [SerializeField] float rayDistance = 5;
    [SerializeField] LayerMask playerLayer;

    [Header("[Attack Settings]")]
    [SerializeField] float attackingTime = 5;
    float attackingTimer;

    int direction = 1;
    Rigidbody2D rb2D;

    EnemyState enemyState = EnemyState.patrol;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (enemyState == EnemyState.patrol)
        {
            Patrol();

            if(CheckForPlayer()) enemyState = EnemyState.attacking; 
        }
        else if (enemyState == EnemyState.attacking)
        {
            if (attackingTimer >= attackingTime)
            {
                attackingTimer = 0;
                enemyState = EnemyState.patrol;
            }
            else
            {
                attackingTimer += Time.deltaTime;
            }

            Attacking();
        }
    }


    bool CheckForPlayer() => Physics2D.Raycast(transform.position, new Vector2(direction, 0), rayDistance, playerLayer);



    void Patrol()
    {
        if (!ChangeDirection())
        {
            direction *= -1;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }

        rb2D.velocity = new Vector2(direction * speed, -1);
    }


    void Attacking()
    {
        
    }


    bool ChangeDirection() => Physics2D.OverlapCircle(wallDropCheck.position, checkSize, groundLayer);


    enum EnemyState
    {
        patrol,
        attacking
    }
}
