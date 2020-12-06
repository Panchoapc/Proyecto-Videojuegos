using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonBehaviour : Enemy
{
    [SerializeField] float MOVE_SPEED = 1f; // velocidad base de movimiento
    [SerializeField] float startPosition = 0f; // posicion inicial
    [SerializeField] float stopPosition = 3f; // radio de movimiento (offset)
    public static readonly int TOUCH_ATTACK = 20; // ataque por contacto
    public static readonly int MAX_HEALTH = 150;
    Rigidbody2D myRigidbody;
    BoxCollider2D myBoxCollider;

    private Player player = null;
    BalooonMovement movement;
    [SerializeField] private Animator animator = null;
    private bool isInNightmareMode = false;
    private Factory factory = null;

    protected override void Start()
    {
        this.moveSpeed = MOVE_SPEED;
        this.touchAttack = TOUCH_ATTACK;
        this.health = MAX_HEALTH;
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myRigidbody.velocity = new Vector2(0f, moveSpeed);
        movement = GetComponent<BalooonMovement>();
        this.player = FindObjectOfType<Player>();
        this.factory = FindObjectOfType<Factory>();
    }


    protected override void Update()
    {
        movement.Move(stopPosition, startPosition, moveSpeed, myRigidbody);

        bool nightmareCondition = this.player.mentalSanity < Player.NIGHTMARE_SANITY;
        if (nightmareCondition && !this.isInNightmareMode) this.EnterNightmareMode();
        else if (!nightmareCondition && this.isInNightmareMode) this.ExitNightmareMode();
    }

    protected override void EnterNightmareMode()
    {
        Debug.LogFormat("[Balloon] Entered nightmare mode!");
        this.isInNightmareMode = true;
        animator.SetBool("inNightmareMode", true);
        this.touchAttack += 25;
        this.moveSpeed += 4;
    }

    protected override void ExitNightmareMode()
    {
        Debug.LogFormat("[Balloon] Exited nightmare mode.");
        this.isInNightmareMode = false;
        animator.SetBool("inNightmareMode", false);
        this.touchAttack = TOUCH_ATTACK;
        this.moveSpeed = MOVE_SPEED;
    }
}
