using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerPos;

    
    void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y, -10);
    }
}
