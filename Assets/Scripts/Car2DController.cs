using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2DController : MonoBehaviour
{
    float speedForce = 15.0f;
    float torqueForce = -200.0f;
    float driftFactor = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (Input.GetButton("Accelerate"))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * speedForce);
        }

        if (Input.GetButton("Horizontal"))
        {
            CustomInput.SetAxis("Horizontal", Input.GetAxis("Horizontal"));
        } else
        {
            CustomInput.SetAxis("Horizontal", 0.0f);
        }

        rb.angularVelocity = (CustomInput.GetAxis("Horizontal") * torqueForce);

        rb.velocity = ForwardVelocity() + LeftVelocity()* driftFactor;

    }

    Vector2 ForwardVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }

    Vector2 LeftVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }
}


