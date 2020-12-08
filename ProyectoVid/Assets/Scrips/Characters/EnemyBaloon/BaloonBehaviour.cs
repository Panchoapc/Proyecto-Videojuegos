using UnityEngine;

public class BaloonBehaviour : Enemy
{
    [SerializeField] private float MOVE_SPEED = 1f; // velocidad base de movimiento
    [SerializeField] private float startPosition; // posicion inicial
    [SerializeField] private float stopPosition = 3f; // radio de movimiento (offset)
    public static readonly int TOUCH_ATTACK = 20; // ataque por contacto
    public static readonly int MAX_HEALTH = 150;
    private Rigidbody2D myRigidbody;
    //BoxCollider2D myBoxCollider;

    private BalooonMovement movement;
    private Animator animator = null;
    //private Factory factory = null;

    protected override void Start()
    {
        base.Start();
        startPosition = transform.position.y;
        this.moveSpeed = MOVE_SPEED;
        this.touchAttack = TOUCH_ATTACK;
        this.health = MAX_HEALTH;
        myRigidbody = GetComponent<Rigidbody2D>();
        //myBoxCollider = GetComponent<BoxCollider2D>();
        myRigidbody.velocity = new Vector2(0f, moveSpeed);
        movement = GetComponent<BalooonMovement>();
        this.player = FindObjectOfType<Player>();
        //this.factory = FindObjectOfType<Factory>();
        this.animator = this.GetComponent<Animator>();
    }


    private void Update()
    {
        if (GameManager.isGamePaused) return;

        movement.Move(stopPosition, startPosition, moveSpeed, myRigidbody);

        this.CheckNightmareCondition();
    }

    protected override void EnterNightmareMode()
    {
        Debug.LogFormat("[Baloon] Entered nightmare mode!");
        this.isInNightmareMode = true;
        animator.SetBool("inNightmareMode", true);
        this.touchAttack += 25;
        this.moveSpeed += 4;
    }

    protected override void ExitNightmareMode()
    {
        Debug.LogFormat("[Baloon] Exited nightmare mode.");
        this.isInNightmareMode = false;
        animator.SetBool("inNightmareMode", false);
        this.touchAttack = TOUCH_ATTACK;
        this.moveSpeed = MOVE_SPEED;
    }
}
