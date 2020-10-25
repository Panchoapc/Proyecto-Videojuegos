using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/* Clase base para los enemigos (NPC). Esta clase nunca debe instanciarse en Unity, se hereda según el tipo de enemigo. */
public class Enemy : Character {
    [Range(0,100)] protected int touchAttack; // daño que hace al tocar al jugador (por colisión)
    [Range(0,300)] protected int health; // resistencia al daño
    private Vector3 playerDir;

    /* Por defecto, simplemente persigue al jugador. */
    protected virtual void Update() {
        this.FollowPlayer();
    }

    /* Se dirige en la dirección del jugador. */
    protected void FollowPlayer() {
        this.playerDir = (FindObjectOfType<Player>().transform.position - this.transform.position).normalized;
        this.FlipOnMovementX(playerDir.x - this.transform.position.x); // siempre mira al jugador
        this.transform.position += this.playerDir * this.moveSpeed * Time.deltaTime;
    }
}
