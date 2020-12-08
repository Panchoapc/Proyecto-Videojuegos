using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    public static readonly int HP = 60;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Player p = FindObjectOfType<Player>();
            if (p.mentalSanity == Player.MAX_SANITY)
            {
                Debug.LogFormat("[MediKit] Player health is full!");
                return;
            }
            p.Heal(HP);
            Destroy(this.gameObject);
        }
    }
}
