using UnityEngine;

public class BalooonMovement : MonoBehaviour
{
    public void Move(float stopPosition, float startPosition, float moveSpeed, Rigidbody2D myRigidbody)
    {
        if (transform.position.y <= (startPosition - stopPosition))
        {
            myRigidbody.velocity = new Vector2(0f, moveSpeed);
        }
        else if (transform.position.y >= (startPosition + stopPosition))
        {
            myRigidbody.velocity = new Vector2(0f, -moveSpeed);
        }
    }
}
