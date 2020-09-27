using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float zoneOfEffect = 5;
    private float trapSpeed = 2;
    Transform trapMovable;
    // Start is called before the first frame update
    void Start()
    {
        trapMovable = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionTrigger2D(Collider2D collider)
    {
        if (collider.tag=="Player")
        {
            Vector3 moveVector = new Vector3(trapSpeed * Time.deltaTime, 0, 0);
            for (int i = 0; i < zoneOfEffect*2; i++)
            {
                transform.Translate(moveVector*i);
            }
        }
    }
}
