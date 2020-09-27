using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [Range(0,100)]   protected float touchAttack; // daño que hace al tocar al jugador
    [SerializeField] protected float spriteSize; // tamaño del sprite cuadrado
    private float health;
    private Vector3 playerDir;
    private float lastTimeMove = 0;

    void Start()
    {

    }
    
    void Update()
    {
        FollowPlayer();
    }

    protected void FollowPlayer()
    {
        if (Time.time - lastTimeMove > 0.2)
        {
            playerDir = FindObjectOfType<Player>().transform.position - transform.position;
            playerDir = playerDir.normalized * moveSpeed/10;

            // voltear sprite según movimiento en X
            if (playerDir.x > 0) {
                this.transform.localScale = new Vector3(this.spriteSize, this.spriteSize, this.spriteSize);
            }
            else if (playerDir.x < 0) {
                this.transform.localScale = new Vector3(-this.spriteSize, this.spriteSize, this.spriteSize);
            }

            transform.position += playerDir;
            lastTimeMove = Time.time;
        }
    }
}
