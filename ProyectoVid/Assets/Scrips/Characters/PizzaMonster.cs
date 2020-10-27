using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMonster : Enemy {
    private static int MOVE_SPEED = 4;
    private static int ATTACK = 20;
    private static int MAX_HEALTH = 200;
    private bool nightmareMode;
    private int currentHealth;
    private int movementCase = 1;
    private float lastMovCase = 0;
    private void Start() {
        this.moveSpeed = MOVE_SPEED;
        this.touchAttack = ATTACK;
        this.health = MAX_HEALTH;
        this.nightmareMode = false;
        currentHealth = 200;
    }

    override protected void Update() {
        this.FollowPlayer();
        if (this.nightmareMode) return;
        if (FindObjectOfType<Player>().GetSanity() < 70) {
            this.EnterNightmareMode();
        }
        lifeChecker();
        //randomMovement();
    }

    private void EnterNightmareMode() {
        Debug.LogFormat("[PizzaMonster] Entered nightmare mode!");
        this.nightmareMode = true;
        this.moveSpeed = 6;
        this.GetComponent<Renderer>().material.color = Color.red;
    }

    private void ExitNightmareMode() {
        Debug.LogFormat("[PizzaMonster] Exited nightmare mode.");
        this.nightmareMode = false;
        this.moveSpeed = MOVE_SPEED;
        this.GetComponent<Renderer>().material.color = Color.white;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        movementCase = Random.Range(1, 4);
        lastMovCase = Time.time;
        if (col.collider.tag == "LightRay")
        {
            Debug.Log("Pizza hit by a lightRay");
            int dmg = FindObjectOfType<LightRay>().getRayDamage();
            currentHealth -= dmg;
        }
    }

    void OnCollisionStay2D(Collision2D collision2D)
    {
        if (Time.time - lastMovCase > 2)
        {
            lastMovCase = Time.time;
            if (movementCase <= 2)
            {
                movementCase = Random.Range(3, 4);
            }
            else
            {
                movementCase = Random.Range(1, 2);
            }
        }
    }

    void lifeChecker()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void randomMovement()
    {
        if (Time.time - lastMovCase > 2)
        {
            lastMovCase = Time.time;
            movementCase = Random.Range(1, 4);
        }
        Vector2 move;
        switch (movementCase)
        {
            case 1:
                move = new Vector2(1 * MOVE_SPEED * Time.deltaTime, 0 * MOVE_SPEED * Time.deltaTime);
                transform.Translate(move);
                break;
            case 2:
                move = new Vector2(-1 * MOVE_SPEED * Time.deltaTime, 0 * MOVE_SPEED * Time.deltaTime);
                transform.Translate(move);
                break;
            case 3:
                move = new Vector2(0 * MOVE_SPEED * Time.deltaTime, 1 * MOVE_SPEED * Time.deltaTime);
                transform.Translate(move);
                break;
            case 4:
                move = new Vector2(0 * MOVE_SPEED * Time.deltaTime, -1 * MOVE_SPEED * Time.deltaTime);
                transform.Translate(move);
                break;
            default:
                break;
        }
    }
}
