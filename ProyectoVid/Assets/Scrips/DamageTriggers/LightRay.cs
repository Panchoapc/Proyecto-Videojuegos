using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lo que dispara la pistola de rayos (RayGun).
/// </summary>
public class LightRay : MonoBehaviour {

    public static readonly int DAMAGE = 30; // daño
    public static readonly int LIFESPAN_SECONDS = 1; // segundos que dura antes de desaparecer

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float spawnPosOffset; // desfase en eje X, para ajustar y aparecer al lado del jugador y no desde dentro
    private Vector3 moveDirection;

    private void Start() {
        Player player = FindObjectOfType<Player>();
        Vector3 aux = player.transform.position;
        // se dispara el rayo en la dirección que mira el jugador, y desde su posición
        if (player.isFacingRight) {
            aux.x += spawnPosOffset;
            this.moveDirection = new Vector3(1,0,0);
        }
        else {
            aux.x -= spawnPosOffset;
            this.moveDirection = new Vector3(-1, 0, 0);
        }

        this.transform.position = aux;

        //Debug.LogFormat(
        //    "[LightRay] Player shot a light ray from position {0} with direction {1}",
        //    this.transform.position,
        //    this.moveDirection
        //);
        Invoke("Vanish", LIFESPAN_SECONDS);
    }

    private void Update() {
        this.transform.position += this.moveDirection * this.moveSpeed * Time.deltaTime;
    }

    //private void OnTriggerEnter2D(Collider2D collider) {
    //    Debug.LogFormat("[LightRay] Collided wiht {0}", collider.name);
    //}

    private void Vanish() {
        Destroy(this.gameObject);
    }
}
