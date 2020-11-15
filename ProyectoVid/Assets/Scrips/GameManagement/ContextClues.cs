using UnityEngine;

/// <summary>
/// Cofre.
/// </summary>
public class ContextClues : MonoBehaviour {
    [SerializeField] private float spawnOffsetX = 1; // desfase en la posición de aparición para el objeto que contenga el cofre
    [SerializeField] private float spawnOffsetY = 0;
    [SerializeField] private float previewCircleRadius = 1;
    private bool inRange;
    private bool notOpened = true;
    private Vector2 objectSpawnPosition;
    //[Range(0, 2)] public float duration = 1;

    private void Start() {
        this.objectSpawnPosition = this.transform.position;
        this.objectSpawnPosition.x += this.spawnOffsetX;
        this.objectSpawnPosition.y += this.spawnOffsetY;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(this.objectSpawnPosition, this.previewCircleRadius);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F) && inRange && notOpened) {
            Debug.Log("[ContextClues] F PRESSED");
            FindObjectOfType<Factory>().SpawnAvocado(this.objectSpawnPosition);
            inRange = false;
            notOpened = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (notOpened) {
            inRange = true;
            if (collision.CompareTag("Player") && inRange) {
                collision.GetComponent<ContextClue>().Enable();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<ContextClue>().Disable();
        }
    }
}