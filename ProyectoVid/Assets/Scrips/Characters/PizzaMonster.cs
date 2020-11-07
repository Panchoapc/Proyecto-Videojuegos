using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PizzaMonster : Enemy {
    private static readonly int MOVE_SPEED = 4;
    private static readonly int ATTACK = 20;
    private static readonly int MAX_HEALTH = 200;

    private bool inNightmareMode = false;

    private void Start() {
        this.moveSpeed = MOVE_SPEED;
        this.touchAttack = ATTACK;
        this.health = MAX_HEALTH;
    }

    override protected void Update() {
        this.FollowPlayer();
        if (this.inNightmareMode) return;
        bool nightmareCondition = FindObjectOfType<Player>().mentalSanity < Enemy.NIGHTMARE_SANITY;
        if (nightmareCondition && !this.inNightmareMode) this.EnterNightmareMode();
        else if (!nightmareCondition && this.inNightmareMode) this.ExitNightmareMode();
    }

    protected override void EnterNightmareMode() {
        Debug.LogFormat("[PizzaMonster] Entered nightmare mode!");
        this.inNightmareMode = true;
        this.moveSpeed += 2;
        this.touchAttack += 10;
        this.GetComponent<Renderer>().material.color = Color.red;
    }

    protected override void ExitNightmareMode() {
        Debug.LogFormat("[PizzaMonster] Exited nightmare mode.");
        this.inNightmareMode = false;
        this.moveSpeed = MOVE_SPEED;
        this.touchAttack = ATTACK;
        this.GetComponent<Renderer>().material.color = Color.white;
    }
}
