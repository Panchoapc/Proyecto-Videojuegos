using UnityEngine;

public class Portal : MonoBehaviour {
    private static readonly int COOLDOWN_SECONDS = 3; // para que no entre en bucle teletransportándose de un portal al otro siempre
    private static bool COOLDOWN_ACTIVE = false;

    void OnTriggerEnter2D(Collider2D collider) {
        if (COOLDOWN_ACTIVE) return;

        if (collider.tag == "Player") {
            Debug.LogFormat("[Portal] Player entered portal!");
            // teletransportando al otro portal (se asume que hay 2)
            foreach (Portal p in FindObjectsOfType<Portal>()) {
                if (p.GetInstanceID() != this.GetInstanceID()) {
                    FindObjectOfType<Player>().transform.position = p.transform.position;
                    COOLDOWN_ACTIVE = true;
                    this.GetComponent<Renderer>().material.color = Color.gray;
                    Invoke(nameof(this.Reactivate), COOLDOWN_SECONDS);
                    return;
                }
            }
        }
    }

    void Reactivate() {
        Debug.LogFormat("[Portal] Reactivated");
        COOLDOWN_ACTIVE = false;
        this.GetComponent<Renderer>().material.color = Color.white;
    }
}

