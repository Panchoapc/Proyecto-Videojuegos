using UnityEngine;

/// <summary>
/// Disparo de rayo de luz de la Xbox 360.
/// </summary>
public class XboxRayShot : MonoBehaviour {
    public static readonly float LIFESPAN_SECONDS = 1; // segundos que dura antes de desaparecer cada rayo
    public static readonly float MOVE_SLPEED = 12;

    // definiendo desfase en los ejes con respecto a la posición de la Xbox
    [SerializeField] private float spawnOffsetX = 2.8f; // 0.64f (pivot)
    [SerializeField] private float spawnOffsetY = 0.8f; // 0.78f (pivot)
    private Vector3 moveDir; // auxiliar

    private void Start() {
        Xbox360 xbox = FindObjectOfType<Xbox360>();
        Player player = FindObjectOfType<Player>();
        this.SetStartPosition(xbox);
        this.moveDir = (player.transform.position - this.transform.position).normalized;
        this.SetStartRotation(xbox, player);
        Destroy(this.gameObject, LIFESPAN_SECONDS);
    }

    private void Update() {
        if (GameManager.isGamePaused) return;

        this.transform.position += this.moveDir * MOVE_SLPEED * Time.deltaTime;
    }

    /// <summary>
    /// Establece la posición de acuerdo a la de `shooter`, añadiendo el offset.
    /// </summary>
    private void SetStartPosition(Xbox360 shooter) {
        Vector3 aux = shooter.transform.position;
        if (shooter.isFacingRight) {
            aux.x += spawnOffsetX;
        }
        else {
            aux.x -= spawnOffsetX;
        }
        aux.y += spawnOffsetY;
        this.transform.position = aux;
    }

    /// <summary>
    /// Rota el rayo adecuadamente hacia `target`.
    /// </summary>
    private void SetStartRotation(Xbox360 shooter, Player target) {
        Quaternion rotation = Quaternion.FromToRotation(shooter.transform.position, target.transform.position);
        this.transform.rotation = rotation;
        Debug.LogFormat("[XboxRayShot] Rotated {0} towards player", rotation);
    }
}
