using UnityEngine;

/// <summary>
/// Disparo de rayo de luz de la Xbox 360.
/// </summary>
public class XboxRayShot : MonoBehaviour {
    public static readonly float LIFESPAN_SECONDS = 1; // segundos que dura antes de desaparecer cada rayo
    public static readonly float MOVE_SLPEED = 13;

    // definiendo desfase en los ejes con respecto a la posición de la Xbox
    [SerializeField] private float spawnOffsetX = 2.8f;
    [SerializeField] private float spawnOffsetY = 0.8f;
    private Vector3 moveDir; // auxiliar
    private Transform target;

    private void Start() {
        // aplicando offset al spawnear
        Vector3 aux = this.transform.position;
        aux.x += spawnOffsetX;
        aux.y += spawnOffsetY;
        this.transform.position = aux;

        // estableciendo dirección y rotación
        this.target = FindObjectOfType<Player>().transform;
        this.moveDir = (Vector2)(target.position - this.transform.position).normalized;
        this.AdjustRotation(this.transform, this.target);
        Destroy(this.gameObject, LIFESPAN_SECONDS);
    }

    private void Update() {
        if (GameManager.isGamePaused) return;

        this.transform.position += this.moveDir * MOVE_SLPEED * Time.deltaTime;
    }

    /// <summary>
    /// Rota el rayo adecuadamente hacia `target`.
    /// </summary>
    private void AdjustRotation(Transform shooter, Transform target) {
        Vector2 from = (Vector2)shooter.position;
        Vector2 to = (Vector2)target.position;
        Vector2 dir = (to - from).normalized;

        this.transform.rotation = Quaternion.FromToRotation(from, dir).normalized * Quaternion.Euler(0, 0, 90).normalized;
    }
}
