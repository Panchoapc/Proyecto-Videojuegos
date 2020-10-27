using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMonster : Enemy {
    private static int MOVE_SPEED = 4;
    private static int ATTACK = 20;
    private static int MAX_HEALTH = 200;
    private bool nightmareMode;

    private void Start() {
        this.moveSpeed = MOVE_SPEED;
        this.touchAttack = ATTACK;
        this.health = MAX_HEALTH;
        this.nightmareMode = false;
    }

    override protected void Update() {
        this.FollowPlayer();
        if (this.nightmareMode) return;
        if (FindObjectOfType<Player>().GetSanity() < 70) {
            this.EnterNightmareMode();
        }
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
}
