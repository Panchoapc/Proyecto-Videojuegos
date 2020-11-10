using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PizzaMonster : Enemy {
    public static readonly float MOVE_SPEED = 3f;
    public static readonly int BITE_ATTACK = 30;
    public static readonly int MAX_HEALTH = 200;

    private bool isInNightmareMode = false;

    protected override void Start() {
        this.playerTransform = FindObjectOfType<Player>().transform;
        this.moveSpeed = MOVE_SPEED;
        this.touchAttack = BITE_ATTACK;
        this.health = MAX_HEALTH;
    }

    protected override void Update() {
        this.FollowPlayer();
        if (this.isInNightmareMode) return;
        bool nightmareCondition = FindObjectOfType<Player>().mentalSanity < Enemy.NIGHTMARE_SANITY;
        if (nightmareCondition && !this.isInNightmareMode) this.EnterNightmareMode();
        else if (!nightmareCondition && this.isInNightmareMode) this.ExitNightmareMode();
    }

    /// <summary>
    /// Pega una mascada al jugador.
    /// </summary>
    private void BitePlayer() {
        // TODO: hacer daño y ejecutar animación de mordida
        throw new NotImplementedException();
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
