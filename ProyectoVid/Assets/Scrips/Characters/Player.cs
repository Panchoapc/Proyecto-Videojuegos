using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] protected float moveSpeed = 5;
    [SerializeField] protected float spriteSize = 2f; // ancho y largo en Unity del sprite
    [Range(1,100)] private int mentalSanity = 100; // vida (sanidad mental) ϵ [0, 100]

    void Start()
    {
        Debug.Log("Player started.");
    }
    
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        CharacterAux.FlipMovementX(this, xMove, this.spriteSize);

        this.transform.position += new Vector3(xMove, yMove, 0) * Time.deltaTime * this.moveSpeed;
    }
}
