using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// CQC: Close-Quarters Combat.
/// Sistmea de combate para espada del jugador.
/// </summary>
public class PlayerCQC : MonoBehaviour {

    AudioSource audiosourceCollision;
    public AudioSource hitSound;

    public static readonly int SWORD_ATTACK = 45;
    [SerializeField] private Transform hitBoxPosition;
    [SerializeField] private float hitBoxRange = 0.5f;
    [SerializeField] private LayerMask targetLayers;

    /// <summary>
    /// Dibuja en Unity el hitbox (en este caso un círculo) del ataque de espada del jugador.
    /// </summary>
    private void OnDrawGizmosSelected() {
        if (this.hitBoxPosition == null) return;
        Gizmos.DrawWireSphere(this.hitBoxPosition.position, this.hitBoxRange);
    }

    /// <summary>
    /// Maneja el ataque del jugador con la espada.
    /// </summary>
    public void SwordAttack() {
        Debug.LogFormat("[PlayerCQC] Attacked with sword");
        foreach (Collider2D hitEnemy in 
            Physics2D.OverlapCircleAll(this.hitBoxPosition.position, this.hitBoxRange, this.targetLayers)
        ) {
            Debug.LogFormat("[PlayerCQC] Attack reached {0}", hitEnemy.name);
            hitEnemy.GetComponent<Enemy>().TakeDamage(SWORD_ATTACK);
            //hitSound.Play();
        }
    }
}
