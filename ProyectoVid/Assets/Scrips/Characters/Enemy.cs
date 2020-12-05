using UnityEngine;

/// <summary>
/// Clase base para enmigos NPC.
/// </summary>
public abstract class Enemy : Character {
    public int touchAttack { get; protected set; } // daño que hace al tocar al jugador (por colisión)
    public int health { get; protected set; } // resistencia al daño
    protected Vector3 moveDir;
    protected Transform playerTransform = null;

    [SerializeField] private AudioSource hitSound = null;

    protected virtual void Start() {
        this.playerTransform = GameObject.FindObjectOfType<Player>().transform;
    }

    protected virtual void Update() {
        this.FollowPlayer();
    }

    /// <summary>
    /// Maneja los triggers de daño.
    /// </summary>
    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        // recibiendo daño si choca con un hitbox de un ataque del jugador
        Debug.LogFormat("[Enemy] Triggered with collider named \"{0}\" and tagged \"{1}\"", collider.name, collider.tag);
        if (collider.name.Contains("RayGunShot")) {
            this.health -= PlayerGunCombat.RAYGUN_SHOT_ATTACK;
            Debug.LogFormat("[Enemy] Got {0} damage from LightRay. Remaining health: {1}", PlayerGunCombat.RAYGUN_SHOT_ATTACK, this.health);
        }
        else {
            return; // evitar cálculo de derrota si es que no recibió daño en el trigger
        }

        // revisando condición de derrota
        if (this.health <= 0) {
            Debug.LogFormat("[Enemy] Defeated!");
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Función que se va a ejecutar luego de calcularse la colisión con el jugador y dañarlo en `PlayerPhysics`.
    /// Sirve por ejemplo para la animación de la pizza que ocurre al atacar.
    /// </summary>
    public virtual void OnPostTouchAttack() { }

    /// <summary>
    /// Persigue al jugador.
    /// </summary>
    protected void FollowPlayer() {
        this.moveDir = (playerTransform.position - this.transform.position).normalized;
        this.FlipOnMovementX(moveDir.x - this.transform.position.x); // siempre mira al jugador
        this.Move(this.moveDir, this.moveSpeed);
    }

    public void TakeDamage(int dmg) {
        this.health = System.Math.Max(this.health - dmg, 0);
        Debug.LogFormat("[Enemy] Took {0} damage, {1} remaining", dmg, this.health);
        if (hitSound != null) hitSound.Play();
        if (this.health <= 0) {
            Debug.LogFormat("[Enemy] Defeated!");
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Entra en modo pesadilla.
    /// </summary>
    abstract protected void EnterNightmareMode();

    /// <summary>
    /// Sale de modo mesadilla.
    /// </summary>
    abstract protected void ExitNightmareMode();
}
