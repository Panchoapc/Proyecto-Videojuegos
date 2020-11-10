using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : Character {
    public static readonly int MAX_LIVES = 3;
    public static readonly int MAX_SANITY = 100;
    public static readonly float MOVE_SPEED = 7;
    
    AudioSource audiosourceCollision;
    public AudioSource raygunSound;
    public AudioSource swordSound;
    public AudioSource eatSound;

    public int lives { get; private set; } // cantidad de intentos ϵ [0, MAX_LIVES]
    public int mentalSanity { get; private set; } // vida (sanidad mental) ϵ [0, MAX_SANITY]
    private string weapon; // nombre del arma equipada. Inicia null (desarmado).
    private Vector3 startingPos; // se guarda la posicion inicial para poder volver a esta en el caso de quedarse sin vida
    [SerializeField] private Insanity sanityBar;
    [SerializeField] private PlayerLives livesDisplay;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite spriteDefaultIdle;
    [SerializeField] private Sprite spriteRayGunIdle;
    [SerializeField] private Sprite spriteSwordIdle;
    [SerializeField] private Sprite spriteSwordAttack;
    [SerializeField] private float swordAttackSpriteDuration = 0.3f; // duración del sprite de atacar con la espada

    [SerializeField] public PlayerCQC swordCombatHandler;

    private void Start() {
        this.moveSpeed = MOVE_SPEED;
        this.mentalSanity = MAX_SANITY;
        this.sanityBar.setSanity(MAX_SANITY);
        this.startingPos = this.transform.position;
        this.lives = MAX_LIVES;
    }
    
    private void FixedUpdate() {
        InputController.Process(this);
        tentaclesCheck();       // checkea nivel de sanidad para hacer aparecer o desaparecer tentaculos
    }
    void tentaclesCheck()
    {
        if (mentalSanity < 40)
        {
            FindObjectOfType<Tentacles>().showTentacles();
        }
        else
        {
            FindObjectOfType<Tentacles>().hideTentacles();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        PhysicsController.HandleCollision(this, collision);
    }

    /// <summary>
    /// Método que debe ser llamado cuando se cumple la condición de perder una vida.
    /// Revisa si hay GAME OVER, y sino reinicia la posición y sanidad iniciales.
    /// </summary>
    private void LooseLive() {
        this.lives--;

        if (this.lives <= 0) { // GAME OVER
            Debug.LogFormat("[Player] Out of lives!");
            FindObjectOfType<GameManager>().LooseGame();
            return;
        }
        this.transform.position = this.startingPos;
        this.mentalSanity = MAX_SANITY;
        this.sanityBar.setSanity(MAX_SANITY);
        this.livesDisplay.DisplayLives(this.lives); // actualizando corazones visibles
    }

    /// <summary>
    /// Toma daño y luego revisa si perdió una vida.
    /// </summary>
    public void TakeDamage(int dmg) {
        this.mentalSanity = System.Math.Max(this.mentalSanity - dmg, 0); // asugurando nunca una vida negativa, para no tener problemas.
        Debug.LogFormat("[Player] Took {0} damage, {1} remaining", dmg, this.mentalSanity);
        this.sanityBar.setSanity(mentalSanity);
        if (this.mentalSanity <= 0) {
            this.LooseLive();
        }
    }

    /// <summary>
    /// Curación.
    /// </summary>
    public void Heal(int dmg) {
        eatSound.Play();
        this.mentalSanity = System.Math.Min(this.mentalSanity + dmg, MAX_SANITY); // asegurando que nunca queda con más del máximo de vida
        Debug.LogFormat("[Player] Healed {0}, {1} remaining", dmg, this.mentalSanity);
        this.sanityBar.setSanity(mentalSanity);
    }

    private void PickUpWeapon(GameObject gotWeapon) {
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
        Destroy(gotWeapon); // des-spawnea el arma recién recogida
    }

    /// <summary>
    /// Cambia al sprite al de la espada Idle.
    /// </summary>
    private void SwordRestoreSprite() {
        this.spriteRenderer.sprite = this.spriteSwordIdle;
    }

    private static class InputController {
        public static void Process(Player p) {
            PhysicsController.Move(p, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Input.GetKeyDown(KeyCode.Space)) {
                StateController.Attack(p);
            }
        }
    }

    private static class PhysicsController {
        public static void Move(Player p, float xInput, float yInput) {
            p.FlipOnMovementX(xInput);
            p.transform.position += new Vector3(xInput, yInput, 0) * p.moveSpeed * Time.deltaTime;
        }
        public static void HandleCollision(Player p, Collision2D collision) {
            GameObject obj = collision.gameObject;
            //Debug.LogFormat("[Player] Collided with {0} (tagged \"{1}\")", obj.name, obj.tag);
            switch (obj.tag) {
                case "Enemy":
                    p.TakeDamage(collision.gameObject.GetComponent<Enemy>().touchAttack);
                    break;
                case "xboxRayShot":
                    p.TakeDamage(Xbox360.RAY_ATTACK);
                    break;
                case "Weapon":
                    p.PickUpWeapon(obj);
                    break;
                case "door":
                    FindObjectOfType<GameManager>().WinGame();
                    break;
            }
        }
        public static void HandleTrigger(Player p, Collider2D collision) {
            // TODO: recibir daño de triggers e.g. el rayo de la Xbox
            throw new NotImplementedException();
        }
    }

    private static class StateController {
        public static void Attack(Player p) {
            if (p.weapon == null) {
                Debug.LogFormat("[Player] Cannot attack without a weapon!");
                return;
            }
            Debug.LogFormat("[Player] Player attacked with weapon {0}", p.weapon);
            
            switch (p.weapon) {
                case "RayGun":
                    p.raygunSound.Play();
                    FindObjectOfType<Factory>().SpawnRayGunShot();
                    break;
                case "ShockSword":
                    p.swordSound.Play();
                    p.swordCombatHandler.SwordAttack();
                    p.spriteRenderer.sprite = p.spriteSwordAttack;
                    p.Invoke(nameof(p.SwordRestoreSprite), p.swordAttackSpriteDuration);
                    break;
            }
        }
    }
}
