using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBear : Enemy {
    public static readonly float MOVE_SPEED = 5.5f;
    public static readonly int TOUCH_ATTACK = 27;
    public static readonly int MAX_HEALTH = 150;

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Sprite spriteNormal = null;
    [SerializeField] private Sprite spriteNightmare = null;

    protected override void Start() {
        base.Start();
        this.moveSpeed = MOVE_SPEED;
        this.health = MAX_HEALTH;
        this.touchAttack = TOUCH_ATTACK;
    }

    private void Update() {
        if (GameManager.isGamePaused) return;

        this.CheckNightmareCondition();
        this.FollowPlayer();
    }

    protected override void EnterNightmareMode() {
        this.spriteRenderer.sprite = this.spriteNightmare;
        this.moveSpeed = MOVE_SPEED;
    }

    protected override void ExitNightmareMode() {
        this.spriteRenderer.sprite = this.spriteNormal;
        this.moveSpeed = 0; // sólo se mueve en modo pesadilla
    }
}
