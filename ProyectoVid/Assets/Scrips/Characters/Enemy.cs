using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// Base class for NPC enemies.
/// </summary>
public abstract class Enemy : Character {
    [Range(0,100)] protected int touchAttack; // daño que hace al tocar al jugador (por colisión)
    [Range(0,300)] protected int health; // resistencia al daño
    private Vector3 playerDir;

    public int GetTouchAttack() {
        return touchAttack;
    }

    /// <summary>
    /// By default, just chases the player. Remember this can be overriden
    /// </summary>
    protected virtual void Update() {
        this.FollowPlayer();
    }

    /// <summary>
    /// Moves in the direction of the player.
    /// </summary>
    protected void FollowPlayer() {
        this.playerDir = (FindObjectOfType<Player>().transform.position - this.transform.position).normalized;
        this.FlipOnMovementX(playerDir.x - this.transform.position.x); // siempre mira al jugador
        this.transform.position += this.playerDir * this.moveSpeed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        // taking damage if collided with a hitbox of an attack of the player
        Debug.LogFormat("[Enemy] Triggered with collider named \"{0}\" and tagged \"{1}\"", collider.name, collider.tag);
        if (collider.name.Contains("LightRay")) {
            this.health -= LightRay.DAMAGE;
            Debug.LogFormat("[Enemy] Got {0} damage from LightRay. Remaining health: {1}", LightRay.DAMAGE, this.health);
        }

        // checking if defeated
        if (this.health <= 0) {
            Debug.LogFormat("[Enemy] Defeated!");
            Destroy(this);
        }
    }
}
