using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        int playerHealth = FindObjectOfType<Player>().getPlayerSanity();
        if (collider.tag == "Player" && playerHealth < 100)     
        {
            FindObjectOfType<Player>().TakeDamage(-20); // dano negativo es igual a healeo
            Destroy(gameObject);
        }
    }
}
