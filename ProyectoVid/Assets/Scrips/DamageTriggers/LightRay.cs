using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lo que dispara la pistola de rayos (RayGun).
/// </summary>
public class LightRay : MonoBehaviour {

    public static readonly int DAMAGE = 30; // daño individual
    public static readonly float LIFESPAN_SECONDS = 1f; // segundos que dura antes de desaparecer cada rayo

    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float spawnPosOffset = 2.1f; // desfase en eje X, para ajustar y aparecer al lado del jugador y no desde dentro
    private Vector3 moveDirection; // auxiliar

    private void Start() {
        Player player = FindObjectOfType<Player>();
        Vector3 aux = player.transform.position;
        // se dispara el rayo en la dirección que mira el jugador, y desde su posición junto a un offset para que salga al lado suyo y no desde dentro
        if (player.isFacingRight) {
            aux.x += spawnPosOffset;
            this.moveDirection = new Vector3(1,0,0);
        }
        else {
            aux.x -= spawnPosOffset;
            this.moveDirection = new Vector3(-1, 0, 0);
        }

        this.transform.position = aux;
        Destroy(this.gameObject, LIFESPAN_SECONDS);
    }

    private void Update() {
        this.transform.position += this.moveDirection * this.moveSpeed * Time.deltaTime;
    }
}
