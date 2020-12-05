using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonBehaviour : MonoBehaviour
{
     [SerializeField] float moveSpeed = 1f;
    [SerializeField] float stopPosition = 3f;
    [SerializeField] float startPosition = 0f;
    Rigidbody2D myRigidbody;
    BoxCollider2D myBoxCollider;

    BalooonMovement movement;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myRigidbody.velocity = new Vector2(0f, moveSpeed);
        movement = GetComponent<BalooonMovement>();
    }


    void Update()
    {
        movement.Move(stopPosition, startPosition, moveSpeed, myRigidbody);
    }
}
