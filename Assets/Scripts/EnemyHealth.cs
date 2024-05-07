using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 30;

    public void TakeDamge(float damage)
    {
        health -= damage;

        Debug.Log("I Got Called");

        if(health <= 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
