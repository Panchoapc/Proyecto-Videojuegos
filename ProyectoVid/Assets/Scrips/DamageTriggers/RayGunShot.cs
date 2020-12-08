using UnityEngine;

/// <summary>
/// Lo que dispara la pistola de rayos (RayGun).
/// </summary>
public class RayGunShot : MonoBehaviour {
    public static readonly float LIFESPAN_SECONDS = 1; // segundos que dura antes de desaparecer cada rayo

    [SerializeField] private float moveSpeed = 20;
    [SerializeField] private float spawnOffsetX = 2.1f; // desfase en eje X, para ajustar y aparecer al lado del jugador y no desde dentro
    private Vector3 moveDirection; // auxiliar

    private void Start() {
        this.SetStartPosition(FindObjectOfType<Player>());
        Destroy(this.gameObject, LIFESPAN_SECONDS);
    }

    private void Update() {
        if (GameManager.isGamePaused) return;

        this.transform.position += this.moveDirection * this.moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Se posiciona desde `shooter` y añade el offset.
    /// </summary>
    private void SetStartPosition(Player shooter) {
        Vector3 aux = shooter.transform.position;
        if (shooter.isFacingRight) {
            aux.x += spawnOffsetX;
            this.moveDirection = new Vector3(1, 0, 0);
        }
        else {
            aux.x -= spawnOffsetX;
            this.moveDirection = new Vector3(-1, 0, 0);
        }
        this.transform.position = aux;
    }
}
