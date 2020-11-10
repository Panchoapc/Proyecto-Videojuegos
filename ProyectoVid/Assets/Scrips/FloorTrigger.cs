using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    AudioSource audiosourceCollision;
    public AudioSource playSound;
    public GameObject door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter");
        playSound.Play();
        door.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        door.SetActive(true);
    }
}
