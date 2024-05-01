using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAttack : MonoBehaviour
{
    [SerializeField] float destroyTime = 0.1f;
    
    void Update()
    {
        Destroy(gameObject, destroyTime);
    }
}
