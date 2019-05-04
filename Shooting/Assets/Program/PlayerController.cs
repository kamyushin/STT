using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharaController
{

    public float speed = 10.0f;
    public Rigidbody rb;
    PlayerController()
    {
        HP = 10;
        Attack = 3;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();     
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        rb.AddForce(x, 0, z);
    }
    
}
