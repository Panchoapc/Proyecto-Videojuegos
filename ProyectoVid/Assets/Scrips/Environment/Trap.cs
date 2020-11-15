using UnityEngine;

public class Trap : MonoBehaviour {
    public float zoneOfEffect = 5;
    private float trapSpeed = 2;
    private Transform trapMovable = null;
    // Start is called before the first frame update
    void Start() {
        trapMovable = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            Debug.Log("[Trap] Player detected");
            Vector3 moveVector = new Vector3(trapSpeed * Time.deltaTime, 0, 0);
            for (int i = 0; i < zoneOfEffect * 2; i++) {
                trapMovable.Translate(moveVector * i);
            }
        }
    }
}
