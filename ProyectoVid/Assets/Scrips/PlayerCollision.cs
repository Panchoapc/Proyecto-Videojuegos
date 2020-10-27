using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "door")
        {
            FindObjectOfType<GameManager>().WinGame();
        }
    }

}
