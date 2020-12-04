using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dorito : MonoBehaviour
{
    public readonly int HP = 10;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Player p = FindObjectOfType<Player>();
            if (p.mentalSanity == Player.MAX_SANITY)
            {
                Debug.LogFormat("[Avocado] Player health is full!");
                return;
            }
            p.Heal(HP);
            Destroy(this.gameObject);
        }
    }
}