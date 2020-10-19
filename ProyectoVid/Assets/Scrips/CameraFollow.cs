using UnityEngine;

/* La cámara sigue a `target` de forma suave. */
public class CameraFollow : MonoBehaviour {
    private Transform target;
    private float smoothSpeed = 0.2f; // 0.2f
    private Vector2 velocity;

    void Start() {
        this.target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate() { // si se usa FixedUpdate se pierde cierta suavidad de movimiento debido a que se usa la velocidad. Se laguea.
        this.transform.position = new Vector3(
            x: Mathf.SmoothDamp(this.transform.position.x, target.position.x, ref velocity.x, smoothSpeed),
            y: Mathf.SmoothDamp(this.transform.position.y, target.position.y, ref velocity.y, smoothSpeed),
            z: this.transform.position.z
        );
    }
}
