using UnityEngine;

/// <summary>
/// CQC: Close-Quarters Combat.
/// Sistmea de combate para espada (ShockSword) del jugador.
/// </summary>
public class PlayerCQC : MonoBehaviour {
    public static readonly int SWORD_ATTACK = 45; // daño que hace el ataque de espada
    public static readonly float SWORD_COOLDOWN = 0.4f; // tiempo mínimo entre ataques (segundos)

    [SerializeField] private Transform hitBoxPosition = null;
    [SerializeField] private float hitBoxRange = 0.5f;
    [SerializeField] private LayerMask targetLayers;
    private bool isCoolingDown = false;

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
    /// <returns>`true` si es que pudo atacar (por el cooldown).</returns>
    public bool SwordAttack() {
        if (this.isCoolingDown) return false;

        Debug.LogFormat("[PlayerCQC] Attacked with sword");
        foreach (Collider2D hitEnemy in
            Physics2D.OverlapCircleAll(this.hitBoxPosition.position, this.hitBoxRange, this.targetLayers)
        ) { // dañando a todos los enemigos dentro del hitbox
            Debug.LogFormat("[PlayerCQC] Attack reached {0}", hitEnemy.name);
            hitEnemy.GetComponent<Enemy>().TakeDamage(SWORD_ATTACK);
        }
        this.isCoolingDown = true;
        Invoke(nameof(this.ReactivateAttack), SWORD_COOLDOWN);
        return true;
    }

    private void ReactivateAttack() {
        this.isCoolingDown = false;
    }
}
