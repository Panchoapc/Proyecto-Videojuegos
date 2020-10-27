using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PizzaMonster : Enemy {
    private static readonly int MOVE_SPEED = 4;
    private static readonly int ATTACK = 20;
    private static readonly int MAX_HEALTH = 200;
    private static readonly int NIGHTMARE_SANITY = 50; // nivel de sanidad a partir del cual se entra en modo pesadilla

    private bool nightmareMode;
    private int currentHealth;
    private int movementCase = 1;
    private float lastMovCase = 0;

    private void Start() {
        this.moveSpeed = MOVE_SPEED;
        this.touchAttack = ATTACK;
        this.health = MAX_HEALTH;
        this.nightmareMode = false;
    }

    override protected void Update() {
        this.FollowPlayer();
        if (this.nightmareMode) return;
        bool nightmareCondition = FindObjectOfType<Player>().GetSanity() < NIGHTMARE_SANITY;
        if (nightmareCondition && !this.nightmareMode) this.EnterNightmareMode();
        else if (!nightmareCondition && this.nightmareMode) this.ExitNightmareMode();
        //lifeChecker(); // esta condición ya se revisa cuando golpea el rayo i.e. luego de un evento que quita vida
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

    //void OnCollisionEnter2D(Collision2D col) {
    //    movementCase = Random.Range(1, 4);
    //    lastMovCase = Time.time;
    //    if (col.collider.tag == "LightRay") {
    //        Debug.Log("Pizza hit by a lightRay");
    //        int dmg = LightRay.DAMAGE;
    //        currentHealth -= dmg;
    //    }
    //}

    //void OnCollisionStay2D(Collision2D collision2D)
    //{
    //    if (Time.time - lastMovCase > 2)
    //    {
    //        lastMovCase = Time.time;
    //        if (movementCase <= 2)
    //        {
    //            movementCase = Random.Range(3, 4);
    //        }
    //        else
    //        {
    //            movementCase = Random.Range(1, 2);
    //        }
    //    }
    //}

    //void lifeChecker()
    //{
    //    if (currentHealth <= 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    void randomMovement()
    {
        if (Time.time - lastMovCase > 2)
        {
            lastMovCase = Time.time;
            movementCase = Random.Range(1, 4);
        }
        Vector3 moveDir = Vector3.zero;
        switch (movementCase)
        {
            case 1:
                moveDir = new Vector3(1, 0, 0);
                break;
            case 2:
                moveDir = new Vector3(-1, 0, 0);
                break;
            case 3:
                moveDir = new Vector3(0, 1, 0);
                break;
            case 4:
                moveDir = new Vector3(0, -1, 0);
                break;
        }
        this.transform.position += moveDir * this.moveSpeed * Time.deltaTime;
    }
}
