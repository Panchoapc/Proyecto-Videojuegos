using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2;
    private Vector3 playerDir;
    private float lastTimeMove = 0;
    void Start()
    {

    }
    
    void Update()
    {
        followPlayer();
    }

    void followPlayer()
    {
        if (Time.time - lastTimeMove > 0.2)
        {
            playerDir = FindObjectOfType<PlayerController>().transform.position - transform.position;
            playerDir = playerDir.normalized * moveSpeed/10;
            transform.position += playerDir;
            lastTimeMove = Time.time;
        }
    }
}
