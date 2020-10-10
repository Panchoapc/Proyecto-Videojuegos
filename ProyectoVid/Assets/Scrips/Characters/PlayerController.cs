using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public Insanity sanityBar;
    [Range(0,100)] private int mentalSanity; // vida (sanidad mental)

    void Start() {
        this.moveSpeed = 5;
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

        this.FlipOnMovementX(xMove);
        // voltear sprite según dirección de movimiento horizontal
        //if (xMove > 0) {
        //    this.transform.localScale = new Vector3(this.spriteSize, this.spriteSize, this.spriteSize);
        //} else if (xMove < 0) {
        //    this.transform.localScale = new Vector3(-this.spriteSize, this.spriteSize, this.spriteSize);
        //}

        this.transform.position += new Vector3(xMove, yMove, 0) * Time.deltaTime * this.moveSpeed;
    }

    void takeDamage(int dmg)
    {
        mentalSanity += dmg;
        sanityBar.setSanity(mentalSanity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("COLLISION ENTER");
        if (collision.tag == "Enemy")
        {
            Debug.Log("damge taken");
            takeDamage(10);
        }
    }
}
