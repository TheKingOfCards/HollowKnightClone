using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject attackArea;
    [SerializeField] float timeToAttack = 0.2f;
    float timeToAttackTimer = 0;

    [SerializeField] float attackSpeed = 0.5f;
    float attackSpeedTimer = 0;
    bool attacking = false;


    void Start()
    {
        attackArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && attackSpeedTimer <= 0)
        {
            attackSpeedTimer = attackSpeed;
            Attack();
        }
        else
        {
            attackSpeedTimer -= Time.deltaTime;
        }

        if (attacking)
        {
            timeToAttackTimer += Time.deltaTime;

            if (timeToAttackTimer >= timeToAttack)
            {
                timeToAttackTimer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
    }


    void Attack()
    {
        attacking = true;

        attackArea.SetActive(attacking);
    }
}
