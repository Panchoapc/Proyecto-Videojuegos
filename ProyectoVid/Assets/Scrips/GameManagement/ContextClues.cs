using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class ContextClues : MonoBehaviour
{
    private bool inRange;
    [Range(0f, 2f)]
    public float duration = 1f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inRange)
        {
            //Debug.Log("F PRESSED");
            Freeze();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<ContextClue>().Enable();
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<ContextClue>().Disable();
            inRange = false;
        }
    }

    void Freeze()
    {
        Debug.Log("FREEZE");
    }
}