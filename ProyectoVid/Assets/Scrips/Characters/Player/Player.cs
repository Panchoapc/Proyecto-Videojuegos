using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
    public static readonly int MAX_LIVES = 3;
    public static readonly int MAX_SANITY = 100;
    public static readonly int NIGHTMARE_SANITY = 50; // nivel de sanidad por debajo del cual el enemigo entra en modo pesadilla
    public static readonly float MOVE_SPEED = 7;

    public AudioSource raygunSound = null;
    public AudioSource swordSound = null;
    public AudioSource eatSound = null;
    public AudioSource winSound = null;

    public int lives { get; private set; } // cantidad de intentos ϵ [0, MAX_LIVES]
    public int mentalSanity { get; private set; } // vida (sanidad mental) ϵ [0, MAX_SANITY]
    public string weapon { get; private set; } // nombre del arma equipada. Inicia nulo (desarmado).
    private Vector3 startingPos; // se guarda la posicion inicial para poder volver a esta en el caso de quedarse sin vida
    [SerializeField] private Insanity sanityBar = null;
    [SerializeField] private PlayerLives livesDisplay = null;

    [SerializeField] public SpriteRenderer spriteRenderer = null;
    //[SerializeField] private Sprite spriteDefaultIdle = null;
    [SerializeField] private Sprite spriteRayGunIdle = null;
    [SerializeField] private Sprite spriteSwordIdle = null;
    [SerializeField] public Sprite spriteSwordAttack = null;
    [SerializeField] public float swordAttackSpriteDuration = 0.3f; // duración del sprite de atacar con la espada

    private Tentacles tentaclesUI = null;
    public PlayerCQC swordCombatHandler = null;
    public PlayerGunCombat gunCombatHandler = null;

    private void Start() {
        this.moveSpeed = MOVE_SPEED;
        this.mentalSanity = MAX_SANITY;
        this.sanityBar.SetSanity(MAX_SANITY);
        this.startingPos = this.transform.position;
        this.lives = MAX_LIVES;
        this.tentaclesUI = GameObject.FindObjectOfType<Tentacles>();
        this.swordCombatHandler = GameObject.FindObjectOfType<PlayerCQC>();
        this.gunCombatHandler = GameObject.FindObjectOfType<PlayerGunCombat>();
    }

    private void FixedUpdate() {
        tentaclesCheck(); // checkea nivel de sanidad para hacer aparecer o desaparecer tentaculos
    }

    private void Update() {
        if (GameManager.isPaused) return;
        PlayerInput.Process(this);
    }

    void tentaclesCheck() {
        if (mentalSanity < 40) {
            this.tentaclesUI.ShowTentacles();
        }
        else {
            this.tentaclesUI.HideTentacles();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //ACA PAUSAR 1.5 SEGUNDOS
        PlayerPhysics.HandleCollision(this, collision);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerPhysics.HandleTrigger(this, collision);
    }

    /// <summary>
    /// Método que debe ser llamado cuando se cumple la condición de perder una vida.
    /// Revisa si hay GAME OVER, y sino reinicia la posición y sanidad iniciales.
    /// </summary>
    private void LooseLive() {
        this.lives--;

        if (this.lives <= 0) { // GAME OVER
            Debug.LogFormat("[Player] Out of lives!");
            GameManager.LooseGame();
            return;
        }
        this.transform.position = this.startingPos;
        this.mentalSanity = MAX_SANITY;
        this.sanityBar.SetSanity(MAX_SANITY);
        this.livesDisplay.DisplayLives(this.lives); // actualizando corazones visibles
    }

    /// <summary>
    /// Toma daño y luego revisa si perdió una vida.
    /// </summary>
    public void TakeDamage(int dmg) {
        if (PlayerCheats.GOD_MODE) return; // implementando cheat de invulnerabilidad

        if (dmg <= 0) Debug.LogErrorFormat("[Player] Warning: player took negative or zero damage!");
        this.mentalSanity = System.Math.Max(this.mentalSanity - dmg, 0); // asugurando nunca una vida negativa, para no tener problemas.
        Debug.LogFormat("[Player] Took {0} damage, {1} remaining", dmg, this.mentalSanity);
        this.sanityBar.SetSanity(mentalSanity);
        if (this.mentalSanity <= 0) {
            this.LooseLive();
        }
    }

    /// <summary>
    /// Curación.
    /// </summary>
    public void Heal(int dmg) {
        if (dmg <= 0) Debug.LogErrorFormat("[Player] Warning: player healed negative or zero points!");
        this.eatSound.Play();
        this.mentalSanity = System.Math.Min(this.mentalSanity + dmg, MAX_SANITY); // asegurando que nunca queda con más del máximo de vida
        Debug.LogFormat("[Player] Healed {0}, {1} remaining", dmg, this.mentalSanity);
        this.sanityBar.SetSanity(mentalSanity);
    }

    /// <summary>
    /// Agarra el arma `gotWeapon`. Dependiendo de `destroyAfter`, destruye el GameObject del arma recogida.
    /// Este valor solo será falso dentro de las cheats.
    /// </summary>
    public void PickUpWeapon(GameObject gotWeapon, bool destroyAfter) {
        //if (this.weapon != null) { // si ya tenía arma equipada, la respawnea donde estaba y la des-equipa
        //    Instantiate(this.weapon);
        //}
        this.weapon = gotWeapon.name;
        Debug.LogFormat("[Player] Picked up {0}", this.weapon);

        // cambiando el sprite de acuerdo al arma recogida
        switch (weapon) {
            case "RayGun":
                this.spriteRenderer.sprite = this.spriteRayGunIdle;
                break;
            case "ShockSword":
                this.spriteRenderer.sprite = this.spriteSwordIdle;
                break;
            default:
                Debug.LogErrorFormat("[Player] Error: unkown weapon name picked up (couldn't find sprite).");
                break;
        }
        if (destroyAfter) Destroy(gotWeapon.gameObject); // des-spawnea el arma recién recogida
    }

    /// <summary>
    /// Cambia al sprite al de la espada Idle.
    /// </summary>
    public void SwordRestoreSprite() {
        this.spriteRenderer.sprite = this.spriteSwordIdle;
    }

    public void ChangeMoveSpeed(float newSpeed) {
        this.moveSpeed = newSpeed;
    }

    /// <summary>
    /// Carga la siguiente escena. Sirve para invocar desde afuera (controlador de físicas) el cambio al
    /// siguiente nivel luego de entrar en la puerta de salida del nivel 1.
    /// </summary>
    public void LoadNextLevel() {
        GameManager.NextScene();
    }
}
