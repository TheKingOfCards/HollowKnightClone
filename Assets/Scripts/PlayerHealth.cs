using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;
    int _health;

    [Header("[Take Damage Settings]")]
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] float yForce = 2;

    void Start()
    {
        _health = maxHealth;
    }

    void Update()
    {

    }


    void Heal()
    {

    }

    public void TakeDamage()
    {
        _health--;
        rb2d.AddForce(new Vector2(0, yForce), ForceMode2D.Impulse);
    }
}
