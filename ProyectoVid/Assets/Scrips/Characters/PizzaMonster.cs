using System; 
using UnityEngine;

public class PizzaMonster : Enemy {
    public static readonly float MOVE_SPEED = 4;
    public static readonly int BITE_ATTACK = 30; // ataque por contacto
    public static readonly float BITE_FREEZE_TIME = 0.5f; // cantidad de segundos en que no se mueve luego de pegarle una mascada al jugador
    public static readonly int MAX_HEALTH = 200;

    private Animator animator = null;
    private bool freezeMovement = false; // sirve para detener movimiento de la pizza

    protected override void Start() {
        base.Start();
        this.moveSpeed = MOVE_SPEED;
        this.touchAttack = BITE_ATTACK;
        this.health = MAX_HEALTH;
        this.animator = this.GetComponent<Animator>();
    }

    private void Update() {
        if (GameManager.isGamePaused || this.freezeMovement) return; // no se mueve durante un corto tiempo luego de haber atacado

        this.FollowPlayer();
        this.CheckNightmareCondition();
    }

    public override void OnPostTouchAttack() {
        this.freezeMovement = true;
        Invoke(nameof(UnfreezeMovement), BITE_FREEZE_TIME);
        this.animator.SetTrigger("biteAttack"); // ejecutando animación de mordida cuando haga daño por colisión al jugador
    }

    private void UnfreezeMovement() {
        this.freezeMovement = false;
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
