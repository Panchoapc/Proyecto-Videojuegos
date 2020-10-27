using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Ranura que se activa cuando se le dispara con la pistola de rayos.
/// </summary>
public class RaySlotPuzzle : MonoBehaviour {
    private bool solved = false;

    private void OnTriggerEnter2D(Collider2D collider) {
        Debug.LogFormat("[RaySlotPuzzle] Triggered by collision named {0}", collider.name);
        if (solved) return;
        if (collider.name.Contains("LightRay")) {
            this.UnlockExit();
        }
    }

    /// <summary>
    /// Quita al obstáculo de la salida.
    /// </summary>
    private void UnlockExit() {
        Destroy(FindObjectOfType<ExitObstacle>());
        solved = true;
        Debug.LogFormat("[RaySlotPuzzle] Removed obstacle from exit.");
    }
}
