using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Clase base de todo personaje en movimiento (jugador e IA) */
public class Character : MonoBehaviour {
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float spriteSize; // ancho y largo en Unity del sprite

    /* Voltea el sprite de acuerdo a dirección de movimento en eje X */
    protected void FlipMovementX(float xMove) {
        if (xMove > 0) {
            this.transform.localScale = new Vector3(this.spriteSize, this.spriteSize, this.spriteSize);
        }
        else if (xMove < 0) {
            this.transform.localScale = new Vector3(-this.spriteSize, this.spriteSize, this.spriteSize);
        }
    }
}
