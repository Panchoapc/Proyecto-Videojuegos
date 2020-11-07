using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : Character {
    public static readonly int MAX_LIVES = 3;
    public static readonly int MAX_SANITY = 100;
    public static readonly float MOVE_SPEED = 7f;
    
    public int lives { get; private set; } // cantidad de intentos ϵ [0, MAX_LIVES]
    public int mentalSanity { get; private set; } // vida (sanidad mental) ϵ [0, MAX_SANITY]
    private string weapon; // nombre del arma equipada. Inicia null (desarmado).
    private Vector3 startingPos; // se guarda la posicion inicial para poder volver a esta en el caso de quedarse sin vida
    [SerializeField] private Insanity sanityBar;
    [SerializeField] private PlayerLives livesDisplay;

    private void Start() {
        this.moveSpeed = MOVE_SPEED;
        this.mentalSanity = MAX_SANITY;
        this.sanityBar.setSanity(MAX_SANITY);
        this.startingPos = this.transform.position;
        this.lives = MAX_LIVES;
    }
    
    private void Update() {
        InputController.Process(this);
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
        Debug.LogFormat("[Player] Took {0} damage. {1} remaining", dmg, this.mentalSanity);
        this.sanityBar.setSanity(mentalSanity);
        if (this.mentalSanity <= 0) {
            this.LooseLive();
        }
    }

    /// <summary>
    /// Curación.
    /// </summary>
    public void Heal(int dmg) {
        this.mentalSanity = System.Math.Min(this.mentalSanity + dmg, MAX_SANITY); // asegurando que nunca queda con más del máximo de vida
        Debug.LogFormat("[Player] Took {0} damage. {1} remaining", dmg, this.mentalSanity);
        this.sanityBar.setSanity(mentalSanity);
    }

    private void PickUpWeapon(GameObject gotWeapon) {
        if (this.weapon != null) { // si ya tenía arma equipada, la respawnea donde estaba y la des-equipa
            //Instantiate(this.weapon);
        }
        this.weapon = gotWeapon.name;
        Debug.LogFormat("[Player] Picked up {0}", this.weapon);
        Destroy(gotWeapon); // des-spawnea el arma recién recogida
    }

    private static class InputController {
        public static void Process(Player p) {
            PhysicsController.Move(p, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Input.GetKeyDown("space")) {
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
            //Debug.LogFormat("[Player] Player collided with {0} (tagged \"{1}\")", obj.name, obj.tag);
            switch (obj.tag) {
                case "Enemy":
                    p.TakeDamage(FindObjectOfType<PizzaMonster>().touchAttack);
                    break;
                case "Weapon":
                    p.PickUpWeapon(obj);
                    break;
                case "door":
                    FindObjectOfType<GameManager>().WinGame();
                    break;
            }
        }
    }

    private static class StateController {
        public static void Attack(Player p) {
            if (p.weapon == null) {
                Debug.LogFormat("[Player] Cannot attack without a weapon!");
                return;
            }
            Debug.LogFormat("[Player] Player attacked with weapon {0}", p.weapon);
            if (p.weapon == "RayGun") {
                FindObjectOfType<Factory>().SpawnRayGunShot();
            }
        }
    }
}
