using UnityEngine;

public class Xbox360 : Enemy {
    public static readonly float MOVE_SPEED = 3.2f; // velocidad base de movimiento
    public static readonly int TOUCH_ATTACK = 15; // ataque por contacto
    public static readonly int RAY_ATTACK = 10; // ataque por rayo
    public static readonly int MAX_HEALTH = 150;
    public static readonly float RAY_ATTACK_COOLDOWN = 2; // segundos que hay entre disparos de rayos láser
    public static readonly float DODGE_SPEED = 7; // velocidad a la que realiza el patrón de movimiento de esquivar
    public static readonly float DODGE_TIME = 0.8f; // segundos por los que se mantiene esquivando
    public static readonly float DODGE_COOLDOWN = 3; // intervalo de tiempo (segundos) mínimo entre movimientos de esquivar

    [SerializeField] private Animator animator = null;
    private bool isInNightmareMode = false;
    private bool rayAvailable = true; // para el cooldown del rayo
    private Player player = null;
    private Factory factory = null;

    private bool isDodging = false;
    private bool dodgeAvailable = true;

    protected override void Start() {
        this.moveSpeed = MOVE_SPEED;
        this.touchAttack = TOUCH_ATTACK;
        this.health = MAX_HEALTH;
        this.player = FindObjectOfType<Player>();
        this.playerTransform = player.transform;
        this.factory = FindObjectOfType<Factory>();
    }

    protected override void Update() {
        if (this.isDodging) {
            this.Move(this.moveDir, DODGE_SPEED);
            return;
        }

        this.FollowPlayer();
        bool nightmareCondition = this.player.mentalSanity < Enemy.NIGHTMARE_SANITY;
        if (this.isInNightmareMode) { // en el modo pesadilla, puede esquivar y tirar rayos
            if (this.dodgeAvailable && UnityEngine.Random.value < 0.2) this.DodgeMove(player.transform);
            if (this.rayAvailable) this.RayAttack();
        }

        if (nightmareCondition && !this.isInNightmareMode) this.EnterNightmareMode();
        else if (!nightmareCondition && this.isInNightmareMode) this.ExitNightmareMode();
    }

    /// <summary>
    /// Movimiento perpendicular a la dirección hacia `target`.
    /// </summary>
    private void DodgeMove(Transform target) {
        Debug.LogFormat("[Xbox360] Dodging...");
        Vector3 targetDir = (target.position - this.transform.position).normalized;
        float rotationDir = UnityEngine.Random.value < 0.5f ? 1 : -1;
        Vector3 dodgeDir = Quaternion.AngleAxis(90 * rotationDir, targetDir).eulerAngles.normalized;
        this.moveDir = dodgeDir;
        this.isDodging = true;
        this.dodgeAvailable = false;
        Invoke(nameof(FinishDodge), DODGE_TIME);
        Invoke(nameof(DodgeCooldown), DODGE_COOLDOWN);
    }

    /// <summary>
    /// Finaliza el estado de esquivar, desbloqueando la dirección de movimiento.
    /// </summary>
    private void FinishDodge() {
        this.isDodging = false;
    }

    /// <summary>
    /// Habilita el movimiento de esquivar nuevamente para poder ser usado posiblemente en el siguiente `Update()`.
    /// </summary>
    private void DodgeCooldown() {
        Debug.LogFormat("[Xbox360] Dodge move cooled down.");
        this.dodgeAvailable = true;
    }

    /// <summary>
    /// Ataca con rayos de luz que salen de las legendarias tres luces rojas de la muerte.
    /// </summary>
    private void RayAttack() {
        Debug.LogFormat("[Xbox360] Attacked with light ray!");
        animator.SetTrigger("rayAttack"); // llamando a la animación de ataque
        Invoke(nameof(CreateOneRay), 0);
        Invoke(nameof(CreateOneRay), 0.3f);
        this.rayAvailable = false;
        Invoke(nameof(RayAttackCooldown), RAY_ATTACK_COOLDOWN);
    }

    private void CreateOneRay() {
        this.factory.SpawnXboxRayShot();
    }

    /// <summary>
    /// Habilita el ataque de rayo para ser usado nuevamente en el siguiente `Update()`.
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
