using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avocado : MonoBehaviour
{
    private static readonly int HEALING_POINTS = 20;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player") {
            if (FindObjectOfType<Player>().getPlayerSanity() == 100) {
                Debug.LogFormat("[AvocadoController] Player health is full!");
                return;
            }
            FindObjectOfType<Player>().TakeDamage(-HEALING_POINTS);
            Destroy(this.gameObject);
        }
    }
}
