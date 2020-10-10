using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/* Clase base para los enemigos (NPC). Esta clase nunca debe instanciarse en Unity, se hereda según el tipo de enemigo. */
public class Enemy : Character {
    [Range(0,100)] protected float touchAttack; // daño que hace al tocar al jugador
    [Range(0,300)] protected float health; // digamos que normalmente tienen 100 de vida pero los más fuertes tienen hasta 300.
    private Vector3 playerDir;
    //private float lastTimeMove = 0;
    
    protected void FollowPlayer() {
        //if (Time.time - lastTimeMove > 0.2)
        //{
        //    playerDir = (FindObjectOfType<Player>().transform.position - this.transform.position).normalized;
        //    this.FlipOnMovementX(this.transform.position.x - playerDir.x); // siempre mira al jugador

        //    transform.position += playerDir * this.moveSpeed * Time.deltaTime;
        //    lastTimeMove = Time.time;
        //}
        this.playerDir = (FindObjectOfType<Player>().transform.position - this.transform.position).normalized;
        this.FlipOnMovementX(playerDir.x - this.transform.position.x); // siempre mira al jugador
        this.transform.position += this.playerDir * this.moveSpeed * Time.deltaTime;
    }
}
