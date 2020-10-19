using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Insanity sanityBar;
    [Range(0,100)] private int mentalSanity; // vida (sanidad mental)

    void Start()
    {
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

        this.transform.position += new Vector3(xMove, yMove, 0) * this.moveSpeed * Time.deltaTime;
    }

    void takeDamage(int dmg)
    {
        mentalSanity += dmg;
        sanityBar.setSanity(mentalSanity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(string.Format("Player collided with {0}", collision.tag));

        /// sería más fácil hacer este trigger en cada enemigo, para que cada tipo de enemigo le haga daño distinto al jugador, en vez de poner 1 tag por cada tipo de enemigo
        
        if (collision.tag == "Enemy")
        {
            //Debug.Log("damge taken");
            //takeDamage(10);
        }
    }
}
