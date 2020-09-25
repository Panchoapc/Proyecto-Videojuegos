using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    void Start()
    {

    }
    
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(xMove * moveSpeed * Time.deltaTime, yMove * moveSpeed * Time.deltaTime, 0);
        transform.Translate(move);
    }
}
