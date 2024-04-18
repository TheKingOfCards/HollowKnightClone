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
        if(_health <= 0) Debug.Log("Dead");
        Heal();
    }


    void Heal()
    {

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            _health--;
            rb2d.AddForce(new Vector2(0, yForce), ForceMode2D.Impulse);
        }
    }
}
