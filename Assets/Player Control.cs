using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController3 : MonoBehaviour
{
    Rigidbody rb;

    Vector3 direction;

    [SerializeField]
    float multiplier = 3f;

    [SerializeField]
    float jumpForce = 400f;

    bool onGround = false;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.zero; // {0, 0, 0}
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // m / s * s = m
        //transform.position += multiplier * velocity * Time.deltaTime;
        if (rb.velocity.magnitude < 10)
        {
            // {3, 2, 5]
            // sqrt(x^2 + y^2 + z^2) = 3
            Vector3 test = new Vector3(123, 0, 0);
            // test.normalized = {1, 0, 0]

            rb.AddForce(direction * multiplier);
        }
    }

    void OnMove(InputValue value)
    {
        // {x, y}
        Vector2 input = value.Get<Vector2>();
        float movementX = input.x;
        float movementZ = input.y;

        //rb.velocity = new Vector3(movementX, 0, movementZ);

        direction = new Vector3(movementX, 0, movementZ);


    }

    void OnJump()
    {
        Debug.Log("Jumping!");

        if (!onGround)
        {
            return;
        }
        Vector3 jumpVector = new Vector3(0, jumpForce, 0);
        rb.AddForce(jumpVector);
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint c = collision.contacts[i];
            float ratioInDirection = Vector3.Dot(Vector3.up, c.normal);
            if (ratioInDirection > 0.8)
            {
                onGround = true;
            }
        }

        //if(collision.gameObject.CompareTag("Floor"))
        //{
        //    onGround = true;
        //}
    }

    private void OnCollisionExit(Collision collision)
    {
        onGround = false;
        //for (int i = 0; i < collision.contactCount; i++)
        //{
        //    ContactPoint c = collision.contacts[i];
        //    float ratioInDirection = Vector3.Dot(Vector3.up, c.normal);
        //    if (ratioInDirection > 0.9)
        //    {
        //        onGround = false;
        //    }
        //}
        //if (collision.gameObject.CompareTag("Floor"))
        //{
        //    onGround = false;
        //}
    }
}