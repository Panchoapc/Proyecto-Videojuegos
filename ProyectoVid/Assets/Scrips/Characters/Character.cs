using UnityEngine;

/// <summary>
/// Clase base para todo personaje (jugador y NPCs).
/// </summary>
public abstract class Character : MonoBehaviour {
    public float moveSpeed { get; protected set; }
    public bool isFacingRight { get; protected set; } = true; // dice si está mirando a la derecha

    /// <summary>
    /// Se mueve en la dirección `dir` (normalizado) con velocidad `speed`.
    /// </summary>
    protected void Move(Vector3 dir, float speed) {
        this.transform.position += dir * speed * Time.deltaTime;
    }

    /// <summary>
    /// Voltea el sprite de acuerdo al movimiento horizontal.
    /// Para que funcione, al inicio del juego debe estar mirando hacia la derecha.
    /// </summary>
    public void FlipOnMovementX(float xMove) {
        if (xMove > 0 && !isFacingRight || xMove < 0 && isFacingRight) {
            isFacingRight = !isFacingRight;
            this.FlipSprite();
        }
    }

    private void FlipSprite() {
        Vector3 aux = this.transform.localScale;
        aux.x *= -1;
        this.transform.localScale = aux;
    }
}
