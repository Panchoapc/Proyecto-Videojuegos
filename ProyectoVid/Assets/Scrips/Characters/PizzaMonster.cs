using System;
using UnityEngine;

public class PizzaMonster : Enemy {
    public static readonly float MOVE_SPEED = 4;
    public static readonly int BITE_ATTACK = 30; // ataque por contacto
    public static readonly int MAX_HEALTH = 200;

    private Animator animator = null;
    private bool isInNightmareMode = false;

    protected override void Start() {
        this.playerTransform = FindObjectOfType<Player>().transform;
        this.animator = this.GetComponent<Animator>();
        this.moveSpeed = MOVE_SPEED;
        this.touchAttack = BITE_ATTACK;
        this.health = MAX_HEALTH;
    }

    protected override void Update() {
        if (GameManager.isPaused) return;

        this.FollowPlayer();
        if (this.isInNightmareMode) return;
        bool nightmareCondition = FindObjectOfType<Player>().mentalSanity < Player.NIGHTMARE_SANITY;
        if (nightmareCondition && !this.isInNightmareMode) this.EnterNightmareMode();
        else if (!nightmareCondition && this.isInNightmareMode) this.ExitNightmareMode();
    }

    public override void OnPostTouchAttack() {
        this.animator.SetTrigger("biteAttack"); // ejecutando animación de mordida cuando haga daño por colisión al jugador
    }

    protected override void EnterNightmareMode() {
        Debug.LogFormat("[PizzaMonster] Entered nightmare mode!");
        this.isInNightmareMode = true;
        this.moveSpeed += 2;
        this.touchAttack += 10;
        this.GetComponent<Renderer>().material.color = Color.red;
    }

    protected override void ExitNightmareMode() {
        Debug.LogFormat("[PizzaMonster] Exited nightmare mode.");
        this.isInNightmareMode = false;
        this.moveSpeed = MOVE_SPEED;
        this.touchAttack = BITE_ATTACK;
        this.GetComponent<Renderer>().material.color = Color.white;
    }
}
