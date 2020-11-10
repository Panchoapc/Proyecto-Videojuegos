using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContextClue : MonoBehaviour
{
    public GameObject clue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        clue.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        clue.SetActive(false);
    }
}
