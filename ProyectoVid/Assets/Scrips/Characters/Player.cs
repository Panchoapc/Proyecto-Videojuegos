using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : Character
{
    [SerializeField] private Insanity sanityBar;
    [Range(0,100)] private int mentalSanity; // vida (sanidad mental)
    public int maxLives = 3;
    private int currentLives;
    private string weapon;
    [SerializeField] private GameObject rayGunShot; // tipo `LightRay`, rayo láser del arma de rayos
    private float xMove, yMove; // auxiliares
    private int startingSanity = 100;
    private Vector3 startingPos; // se guarda la posicion inicial para poder volver a esta en el caso de quedarse sin vida

    public int GetSanity() { return this.mentalSanity; }

    void Start() {
        this.moveSpeed = 7;
        this.mentalSanity = 100;
        sanityBar.setSanity(startingSanity);   // Se setea la sanity inicial en 100
        startingPos = transform.position;
        currentLives = maxLives;
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
            sanityBar.setSanity(startingSanity); // se vuelve la sanidad a 100
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
                Debug.LogFormat("[Player] Instantiating RayGun shot");
                Instantiate(p.rayGunShot);
            }
        }
    }
}
