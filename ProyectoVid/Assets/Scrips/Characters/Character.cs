using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Clase base de todo personaje en movimiento (jugador y NPC) */
public class Character : MonoBehaviour {
    protected float moveSpeed;
    private bool isFacingRight = true; // dice si está mirando a la derecha

    /**
     * Voltea el sprite de acuerdo a dirección de movimento en eje X.
     * Para que funcione siempre debe mirar hacia la derecha al iniciar.
     * */
    protected void FlipOnMovementX(float xMove) {
        if (xMove > 0 && !isFacingRight || xMove < 0 && isFacingRight) {
            isFacingRight = !isFacingRight;
            this.FlipSprite();
        }
    }

    private void FlipSprite() {
        Vector3 aux = this.transform.localScale;
        aux.x *= -1;
        this.transform.localScale = aux;
    }
}
