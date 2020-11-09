using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Disparo de rayo de luz de la Xbox 360.
/// </summary>
public class XboxRayShot : MonoBehaviour {
    public static readonly float LIFESPAN_SECONDS = 1; // segundos que dura antes de desaparecer cada rayo
    [SerializeField] private float moveSpeed = 15;

    // definiendo desfase en los ejes con respecto a la posición de la Xbox
    [SerializeField] private float spawnOffsetX = 2.8f; // 0.64f (pivot)
    [SerializeField] private float spawnOffsetY = 0.8f; // 0.78f (pivot)
    private Vector3 moveDir; // auxiliar

    private void Start() {
        this.SetInitialPosition(FindObjectOfType<Xbox360>());
        this.moveDir = (FindObjectOfType<Player>().transform.position - this.transform.position).normalized;

        // TODO: rotar rayo de acuerdo a posición del jugador

        //this.transform.Rotate(this.moveDir);
        //this.transform.RotateAround((Vector2)this.moveDir, );
        Destroy(this.gameObject, LIFESPAN_SECONDS);
    }

    private void Update() {
        this.transform.position += this.moveDir * this.moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Establece la posición de acuerdo a la de `shooter`, añadiendo el offset.
    /// </summary>
    private void SetInitialPosition(Xbox360 shooter) {
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
}
