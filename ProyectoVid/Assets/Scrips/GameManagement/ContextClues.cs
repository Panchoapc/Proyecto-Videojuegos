using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class ContextClues : MonoBehaviour
{
    public GameObject Palta;
    private bool inRange;
    private bool notOpened = true;
    [Range(0f, 2f)]
    public float duration = 1f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inRange && notOpened)
        {
            Debug.Log("F PRESSED");
            Instantiate(Palta, this.transform.position, Quaternion.identity);
            inRange = false;
            notOpened = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (notOpened)
        {
            inRange = true;
            if (collision.CompareTag("Player") && inRange)
            {
                collision.GetComponent<ContextClue>().Enable();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        inRange = false;
        if (collision.CompareTag("Player") && !inRange)
        {
            collision.GetComponent<ContextClue>().Disable();
        }
    }
}