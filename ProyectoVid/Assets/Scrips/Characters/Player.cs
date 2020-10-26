using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Insanity sanityBar;
    [Range(0,100)] private int mentalSanity; // vida (sanidad mental)
    public int maxLives = 3;
    private int currentLives;
    private GameObject weapon;
    private float xMove, yMove; // auxiliares
    private int startingSanity = 100;
    private Vector3 startingPos;        // se guarda la posicion inicial para poder volver a esta en el caso de quedarse sin vida

    void Start() {
        this.moveSpeed = 5;
        this.mentalSanity = 100;
        sanityBar.setSanity(startingSanity);   // Se setea la sanity inicial en 100
        startingPos = transform.position;
        currentLives = maxLives;
    }
    
    void FixedUpdate() {
        InputController.Process(this);
        checkSanity();
    }

    void checkSanity()      // funcion que verificara la sanidad del personaje y lo devolvera a la pos incial si se queda sin sanidad
    {
        if(mentalSanity <= 0)
        {
            transform.position = startingPos;
            sanityBar.setSanity(startingSanity);        // se vuelve la sanidad a 100
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
    public void TakeDamage(int dmg) {
        this.mentalSanity -= dmg;
        this.sanityBar.setSanity(mentalSanity);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        PhysicsController.HandleCollision(this, collision);
    }

    public void PickUpWeapon(GameObject newWeapon) {
        if (this.weapon != null) { // si ya tenía arma equipada, la respawnea donde estaba y la des-equipa
            Instantiate(this.weapon);
        }
        Utilities.Logf("[Player] Picked up {0}", newWeapon.name); // se asigna el arma como atributo de `Player`, y esta desaparece del mapa
        this.weapon = newWeapon;
        Destroy(this.weapon);
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
            Utilities.Logf("[Player] Player collided with {0} (tagged \"{1}\")", obj.name, obj.tag);
            switch (obj.tag) {
                case "Enemy":
                    int enemyAtackDamage = FindObjectOfType<PizzaMonster>().getAttack();
                    p.TakeDamage(enemyAtackDamage);
                    break;
                case "Weapon":
                    p.PickUpWeapon(obj);
                    break;
            }
        }
    }
    private static class StateController {
        public static void Attack(Player p) {
            if (p.weapon == null) return;
            Utilities.Logf("[Player] Player attacked with weapon {0}", p.weapon.name);
            // TODO: hitbox de acuerdo a arma equipada
        }
    }
}
