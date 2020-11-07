using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La palta que recupera sanidad.
/// </summary>
public class Avocado : MonoBehaviour {
    private static readonly int HEALING_POINTS = 30;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            Player p = FindObjectOfType<Player>();
            if (p.mentalSanity == 100) {
                Debug.LogFormat("[Avocado] Player health is full!");
                return;
            }
            p.Heal(HEALING_POINTS);
            Destroy(this.gameObject);
        }
    }
}
