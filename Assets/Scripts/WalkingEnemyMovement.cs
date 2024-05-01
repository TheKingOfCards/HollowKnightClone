using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WalkingEnemyMovement : MonoBehaviour
{
    [Header("[Movement Settings]")]
    [SerializeField] float patrolSpeed = 2;
    float speed;
    [SerializeField] Transform wallDropCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float checkSize = 0.2f;

    [Header("[Patrol Settings]")]
    [SerializeField] float rayDistance = 5;
    [SerializeField] LayerMask playerLayer;

    [Header("[Attack Settings]")]
    [SerializeField] Transform playerPosition;
    [SerializeField] float chasingTime = 5;
    [SerializeField] float chasingSpeed = 4;
    [SerializeField] Collider2D attackRange;
    float chasingTimer;

    int _direction = 1;
    Rigidbody2D _rb2D;

    EnemyState enemyState = EnemyState.patrol;

    void Start()
    {
        speed = patrolSpeed;
        chasingTimer = chasingTime;

        _rb2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (enemyState == EnemyState.patrol)
        {
            Patrol();

            if (CheckForPlayer()) 
            {
                enemyState = EnemyState.chasing;
                speed = chasingSpeed;
            }
        }
        else if (enemyState == EnemyState.chasing)
        {
            if (!CheckForPlayer()) { chasingTimer -= Time.deltaTime; }
            

            if (chasingTimer <= 0)
            {
                chasingTimer = chasingTime;
                enemyState = EnemyState.patrol;
                speed = patrolSpeed;
            }
            
            Chasing();
        }
        else if (enemyState == EnemyState.attacking)
        {
            
        }
    }


    bool CheckForPlayer() => Physics2D.Raycast(transform.position, new Vector2(_direction, 0), rayDistance, playerLayer);


    void Patrol()
    {
        if (!ChangeDirection())
        {
            _direction *= -1;
        }

        _rb2D.velocity = new Vector2(_direction * patrolSpeed, _rb2D.velocity.y);
        transform.localScale = new Vector3(_direction, 1, 1);
    }


    void Chasing()
    {
        Debug.Log("Chasing");
        if(playerPosition.position.x < transform.position.x)
        {
            _direction = -1;
            transform.localScale = new Vector3(_direction, 1, 1);
        }
        else
        {
            _direction = 1;
            transform.localScale = new Vector3(_direction, 1, 1);
        }


        _rb2D.velocity = new Vector2(_direction * patrolSpeed, _rb2D.velocity.y);
    }

    // Checks if player enters collider and attacks if timer is higher than max
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<PlayerHealth>() != null)
        {
            _rb2D.velocity = Vector2.zero;
            _rb2D.velocity = new Vector2(_direction * 10, _rb2D.velocity.y);
        }
    }


    bool ChangeDirection() => Physics2D.OverlapCircle(wallDropCheck.position, checkSize, groundLayer);


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawRay(transform.position, new Vector2(_direction, 0) * rayDistance);
    }



    enum EnemyState
    {
        patrol,
        chasing,
        attacking
    }
}
