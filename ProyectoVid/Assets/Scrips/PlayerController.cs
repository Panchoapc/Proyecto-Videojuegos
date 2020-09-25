using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float spriteSize = 2f; // escala del sprite
    private int mentalSanity; // vida (sanidad mental) ϵ [0, 100]

    void Start()
    {
        this.mentalSanity = 100;
    }
    
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        // voltear sprite según dirección de movimiento horizontal
        if (xMove > 0) {
            this.transform.localScale = new Vector3(this.spriteSize, this.spriteSize, this.spriteSize);
        } else if (xMove < 0) {
            this.transform.localScale = new Vector3(-this.spriteSize, this.spriteSize, this.spriteSize);
        }

        this.transform.position += new Vector3(xMove, yMove, 0) * Time.deltaTime * this.moveSpeed;
    }
}
