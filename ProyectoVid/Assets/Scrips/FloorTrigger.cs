using UnityEngine;

public class FloorTrigger : MonoBehaviour {
    public AudioSource playSound = null;
    public GameObject door = null;

    private void OnTriggerEnter2D(Collider2D other) {
        playSound.Play();
        door.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        door.SetActive(true);
    }
}
