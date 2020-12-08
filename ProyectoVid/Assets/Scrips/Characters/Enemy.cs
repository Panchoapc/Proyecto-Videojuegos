using UnityEngine;

/// <summary>
/// Clase base para enmigos NPC.
/// </summary>
public abstract class Enemy : Character {
    public int touchAttack { get; protected set; } // daño que hace al tocar al jugador (por colisión)
    public int health { get; protected set; } // resistencia al daño
    protected Vector3 moveDir;
    protected Player player = null;
    protected bool isInNightmareMode = false;
    [SerializeField] private AudioSource hitSound = null;

    protected virtual void Start() {
        this.player = GameObject.FindObjectOfType<Player>(); // <- siempre implementar. Siempre sobreescribir con `base.Start()`.
    }

    /// <summary>
    /// Maneja los triggers de daño.
    /// </summary>
    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        // recibiendo daño si choca con un hitbox de un ataque del jugador
        //Debug.LogFormat("[Enemy] Triggered with collider named \"{0}\" and tagged \"{1}\"", collider.name, collider.tag);
        if (collider.name.Contains("RayGunShot")) {
            this.TakeDamage(PlayerGunCombat.RAYGUN_SHOT_ATTACK);
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
        this.moveDir = (player.transform.position - this.transform.position).normalized;
        this.FlipOnMovementX(moveDir.x - this.transform.position.x); // siempre mira al jugador
        this.Move(this.moveDir, this.moveSpeed);
    }

    public void TakeDamage(int dmg) {
        if (dmg <= 0) Debug.LogErrorFormat("[Enemy] Warning: took negative or zero damage!");

        this.health = System.Math.Max(this.health - dmg, 0);
        Debug.LogFormat("[Enemy] Took {0} damage, {1} remaining", dmg, this.health);
        if (hitSound != null) hitSound.Play();
        if (this.health <= 0) {
            Debug.LogFormat("[Enemy] Defeated!");
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Retorna si en este instante se cumple la condición para entrar/permanecer en modo pesadilla.
    /// Ya implementa `EnterNightmareMode` y `ExitNightmareMode` de acuerdo al cambio de la condición,
    /// i.e. si el estado de pesadilla no ha cambiado, no ejecuta ninguna de esas dos funciones.
    /// </summary>
    protected bool CheckNightmareCondition() {
        bool nightmare_condition = (this.player.mentalSanity <= Player.NIGHTMARE_SANITY) || PlayerCheats.FORCED_NIGHTMARE_MODE;
        if (nightmare_condition && !this.isInNightmareMode) this.EnterNightmareMode();
        else if (!nightmare_condition && this.isInNightmareMode) this.ExitNightmareMode();
        return nightmare_condition;
    }

    /// <summary>
    /// Cambio de comportamiento al entrar en modo pesadilla.
    /// </summary>
    abstract protected void EnterNightmareMode();

    /// <summary>
    /// Cambio de comportamiento al salir del modo mesadilla.
    /// </summary>
    abstract protected void ExitNightmareMode();
}
