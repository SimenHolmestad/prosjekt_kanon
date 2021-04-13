using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPMovement : MonoBehaviour
{
    public float speed = 10f;
    public CharacterController controller;

    //private float gravity = -9.81f;
    private float fake_gravity = -0.2f;
    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = (transform.right * x) + (transform.forward * z);
        controller.Move(move * speed * Time.deltaTime);
            
        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
        velocity.y += fake_gravity;
        controller.Move(velocity);
    }
}
