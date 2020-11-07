using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class Xbox360 : Enemy {
    private static readonly float MOVE_SPEED = 3.2f;
    private static readonly int TOUCH_ATTACK = 15;
    private static readonly int RAY_ATTACK = 45;
    private static readonly int MAX_HEALTH = 150;
    private static readonly float SIGHT_DISTANCE = 10f; // unidades de distancia a la que puede detectar al jugador para perseguirlo
    private static readonly float RAY_ATTACK_COOLDOWN = 2f; // segundos que hay entre disparos de rayos láser

    [SerializeField] private Animator animator;
    private bool isInNightmareMode = false;
    private bool isAttacking = false;
    private bool rayAvailable = true; // para el cooldown del rayo
    private Player player;

    protected override void Start() {
        this.moveSpeed = MOVE_SPEED;
        this.touchAttack = TOUCH_ATTACK;
        this.health = MAX_HEALTH;
        this.player = FindObjectOfType<Player>();
        this.playerTransform = player.transform;
    }

    protected override void Update() {
        bool nightmareCondition = this.player.mentalSanity < Enemy.NIGHTMARE_SANITY;

        if (this.isInNightmareMode) {
            // ...
            if (this.rayAvailable) {
                this.RayAttack();
                rayAvailable = false;
                Invoke("RayAttackCooldown", RAY_ATTACK_COOLDOWN);
            }
            return;
        }
        if (Vector2.Distance(this.transform.position, this.playerTransform.position) <= SIGHT_DISTANCE) {
            this.FollowPlayer();
        }
        else {
            // ...
        }

        if (nightmareCondition && !this.isInNightmareMode) this.EnterNightmareMode();
        else if (!nightmareCondition && this.isInNightmareMode) this.ExitNightmareMode();
    }

    /// <summary>
    /// Patrón de movimiento perpendicular a la dirección del jugador.
    /// </summary>
    private void DodgeMove() {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Ataca con tres rayos de luz que salen de las infames tres luces rojas de la muerte y destrucción.
    /// </summary>
    public void RayAttack() {
        Debug.LogFormat("[Xbox360] Attacked with light ray!");
        animator.SetBool("rayAttacking", true); // llamando a la animación de ataque
        // ...
        animator.SetBool("rayAttacking", false);
    }

    /// <summary>
    /// Habilita el ataque de rayo para ser usado nuevamente en el siguiente Update.
    /// </summary>
    private void RayAttackCooldown() {
        rayAvailable = true;
    }

    protected override void EnterNightmareMode() {
        Debug.LogFormat("[Xbox360] Entered nightmare mode!");
        this.isInNightmareMode = true;
        animator.SetBool("inNightmareMode", true);
        this.moveSpeed += 2;
        this.touchAttack += 10;
    }

    protected override void ExitNightmareMode() {
        Debug.LogFormat("[Xbox360] Exited nightmare mode.");
        this.isInNightmareMode = false;
        animator.SetBool("inNightmareMode", false);
        this.moveSpeed = MOVE_SPEED;
        this.touchAttack = TOUCH_ATTACK;
    }
}
