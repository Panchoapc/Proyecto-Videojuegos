using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for every character (player and NPCs)
/// </summary>
public abstract class Character : MonoBehaviour {
    protected float moveSpeed;
    protected bool isFacingRight = true; // dice si está mirando a la derecha

    /// <summary>
    /// Flips the sprite according to movement on X axis (horizontal).
    /// In order to work, must be facing right at game start.
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
