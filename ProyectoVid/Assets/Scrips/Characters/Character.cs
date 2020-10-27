using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase base para todo personaje (jugador y NPCs).
/// </summary>
public abstract class Character : MonoBehaviour {
    protected float moveSpeed;
    protected bool isFacingRight = true; // dice si está mirando a la derecha

    /// <summary>
    /// Voltea el sprite de acuerdo al movimiento horizontal.
    /// Para que funcione, al inicio del juego debe estar mirando hacia la derecha.
    /// </summary>
    protected void FlipOnMovementX(float xMove) {
        if (xMove > 0 && !isFacingRight || xMove < 0 && isFacingRight) {
            isFacingRight = !isFacingRight;
            this.FlipSprite();
        }
    }

    public bool IsFacingRight() {
        return this.isFacingRight;
    }

    private void FlipSprite() {
        Vector3 aux = this.transform.localScale;
        aux.x *= -1;
        this.transform.localScale = aux;
    }
}
