using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("[Attack Settings]")]
    [SerializeField] Transform playerPosition;
    [SerializeField] Transform attackPosition;
    [SerializeField] float attackRange = 1.2f;
    [SerializeField] float upDownOffset = 0.5f;
    [SerializeField] float timeToAttack = 0.4f;
    [SerializeField] float attackSpeed = 0.1f;
    float timer = 0;

    [SerializeField] GameObject showAttack;
    float showAttackXOffset = 0.65f;
    float showAttackYOffset = 0.05f;

    [NonSerialized] public bool downStrike = false;

    GameObject attackCollider;
    bool _attacking = false;
    bool _facingRight = true;
    playerMovement playerMovement;


    void Start()
    {
        attackCollider = transform.GetChild(0).gameObject;
        attackCollider.SetActive(false);
        playerMovement = GetComponent<playerMovement>();
        attackPosition.position = new Vector2(playerPosition.position.x - attackRange, playerPosition.position.y);
    }


    void Update()
    {
        GetAttackDirection();

        if (!_attacking) // Delay for player to attack again
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                if (Input.GetAxisRaw("Fire1") > 0)
                {
                    timer = 0;
                    Attack();
                }
            }
        }

        if (_attacking) // Timer for how long the attack area is active
        {
            timer += Time.deltaTime;

            if (timer >= attackSpeed)
            {
                Debug.Log("Attack done");
                timer = 0;
                _attacking = false;
                attackCollider.SetActive(_attacking);
            }
        }
    }


    void GetAttackDirection()
    {
        if (!_attacking) // Prevents palyer from switching direction while attacking
        {
            // Checks if player is looking up or down
            if (Input.GetAxisRaw("Vertical") > Mathf.Epsilon)
            {
                attackPosition.SetPositionAndRotation(new Vector2(playerPosition.position.x, playerPosition.position.y + attackRange + upDownOffset), Quaternion.Euler(0, 0, 90));
                downStrike = false;
            }
            else if (Input.GetAxisRaw("Vertical") < Mathf.Epsilon && !playerMovement.IsGrounded())
            {
                attackPosition.SetPositionAndRotation(new Vector2(playerPosition.position.x, playerPosition.position.y - attackRange - upDownOffset), Quaternion.Euler(0, 0, -90));
                downStrike = true;
            }

            // Checks if player is facing right or left
            if (Input.GetAxisRaw("Horizontal") > Mathf.Epsilon)
            {
                _facingRight = true;
                showAttackXOffset *= -1;
            }
            else if (Input.GetAxisRaw("Horizontal") < -Mathf.Epsilon)
            {
                _facingRight = false;
                showAttackXOffset *= -1;
            }

            //Attacks left or right if player is not looking up or down
            if (_facingRight && Input.GetAxisRaw("Vertical") == 0)
            {
                attackPosition.SetPositionAndRotation(new Vector2(playerPosition.position.x + attackRange, playerPosition.position.y), Quaternion.Euler(0, 0, 0));
                downStrike = false;
            }
            else if (!_facingRight && Input.GetAxisRaw("Vertical") == 0)
            {
                attackPosition.SetPositionAndRotation(new Vector2(playerPosition.position.x - attackRange, playerPosition.position.y), Quaternion.Euler(0, 0, 180));
                downStrike = false;
            }
        }
    }


    void Attack()
    {
        _attacking = true;
        attackCollider.SetActive(_attacking);

        // ! Only for show attack area | Use animation
        Instantiate(showAttack, new Vector3(attackPosition.position.x + showAttackXOffset, attackPosition.position.y + showAttackYOffset, attackPosition.position.z), attackPosition.localRotation);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(new Vector3(attackPosition.position.x, attackPosition.position.y, 0), 0.5f);
    }
}
