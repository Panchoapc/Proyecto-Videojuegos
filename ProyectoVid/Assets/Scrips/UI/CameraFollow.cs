using UnityEngine;

/// <summary>
/// La cámara sigue a `target` de forma suave.
/// </summary>
public class CameraFollow : MonoBehaviour {
    private Transform target; // lo que la cámara sigue
    [SerializeField] private float smoothSpeed = 0.15f; // ajusta al nivel de suevidad de seguimiento. Defecto: 0.2f
    private Vector2 velocity; // auxiliar

    private void Start() {
        this.target = FindObjectOfType<Player>().transform;
    }

    private void LateUpdate() { // si se usa FixedUpdate se pierde cierta suavidad de movimiento debido a que se usa la velocidad. Se laguea.
        this.transform.position = new Vector3(
            x: Mathf.SmoothDamp(this.transform.position.x, target.position.x, ref velocity.x, smoothSpeed),
            y: Mathf.SmoothDamp(this.transform.position.y, target.position.y, ref velocity.y, smoothSpeed),
            z: this.transform.position.z
        );
    }
}