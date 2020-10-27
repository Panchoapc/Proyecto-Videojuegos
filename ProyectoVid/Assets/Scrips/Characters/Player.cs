using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : Character {
    public static int MAX_LIVES = 3;
    public static int MAX_SANITY = 100;

    [SerializeField] private Insanity sanityBar;
    public int mentalSanity { get; private set; } // vida (sanidad mental) ϵ [0, MAX_SANITY]
    public int currentLives { get; private set; } // cantidad de intentos ϵ [0, MAX_LIVES]
    private string weapon; // nombre del arma equipada
    [SerializeField] private GameObject rayGunShot; // tipo `LightRay`, rayo láser del arma de rayos
    private Vector3 startingPos; // se guarda la posicion inicial para poder volver a esta en el caso de quedarse sin vida

    public int GetSanity() { return this.mentalSanity; }

    void Start() {
        this.moveSpeed = 7;
        this.mentalSanity = 100;
        this.sanityBar.setSanity(MAX_SANITY);   // Se setea la sanity inicial en 100
        this.startingPos = transform.position;
        this.currentLives = MAX_LIVES;
    }
    
    void Update() {
        InputController.Process(this);
        checkSanity();
    }

    void checkSanity() // funcion que verificara la sanidad del personaje y lo devolvera a la pos incial si se queda sin sanidad
    {
        if(mentalSanity <= 0)
        {
            transform.position = startingPos;
            sanityBar.setSanity(MAX_SANITY); // se vuelve la sanidad a 100
            mentalSanity = 100;
            currentLives -= 1;
        }
    }

    public int getCurrentLives()
    {
        return currentLives;
    }
    public int getPlayerSanity()
    {
        return mentalSanity;
    }

    /// <summary>
    /// Daño.
    /// </summary>
    public void TakeDamage(int dmg) {
        this.mentalSanity = System.Math.Max(this.mentalSanity - dmg, 0); // asugurando nunca una vida negativa, para no tener problemas.
        Debug.LogFormat("[Player] Took {0} damage. {1} remaining", dmg, this.mentalSanity);
        this.sanityBar.setSanity(mentalSanity);
    }

    /// <summary>
    /// Curación.
    /// </summary>
    public void Heal(int dmg) {
        this.mentalSanity = System.Math.Min(this.mentalSanity + dmg, 100); // asegurando que nunca queda con más del máximo de vida
        Debug.LogFormat("[Player] Took {0} damage. {1} remaining", dmg, this.mentalSanity);
        this.sanityBar.setSanity(mentalSanity);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        PhysicsController.HandleCollision(this, collision);
    }

    public void PickUpWeapon(GameObject gotWeapon) {
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
            Debug.LogFormat("[Player] Player collided with {0} (tagged \"{1}\")", obj.name, obj.tag);
            switch (obj.tag) {
                case "Enemy":
                    p.TakeDamage(FindObjectOfType<PizzaMonster>().GetTouchAttack());
                    break;
                case "Weapon":
                    p.PickUpWeapon(obj);
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
                Instantiate(p.rayGunShot);
            }
        }
    }
}
