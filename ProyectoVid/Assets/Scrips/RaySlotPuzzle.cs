using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Ranura que se activa cuando se le dispara con la pistola de rayos.
/// </summary>
public class RaySlotPuzzle : MonoBehaviour {
    private bool solved = false;

    private void OnCollisionEnter2D(Collision2D collider) {
        Debug.LogFormat("[RaySlotPuzzle] Triggered by collision named {0}", collider.gameObject.name);
        if (collider.gameObject.name.Contains("LightRay")) {
            this.UnlockExit();
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Debug.LogFormat("[RaySlotPuzzle] Collided with {0}", collider.name);
        if (collider.name.Contains("LightRay")) {
            this.UnlockExit();
        }
    }

    /// <summary>
    /// Quita al obstáculo de la salida.
    /// </summary>
    private void UnlockExit() {
        if (solved) return;
        Destroy(FindObjectOfType<ExitObstacle>().gameObject);
        solved = true;
        Debug.LogFormat("[RaySlotPuzzle] Removed obstacle from exit.");
    }
}
