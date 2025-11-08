using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveAcceleration = 20f;

    public float maxSpeed = 7f;
    public float turnSpeed = 180f;
    private Rigidbody rb;
    public bool gameOn = true;

    private string movementAxisName;
    private string turnAxisName;

    private float movementInputValue;
    private float turnInputValue;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.drag = 5f;
            rb.angularDrag = 5f;
        }
    }

    // Update is called once per frame
    // Store player's input
    void Update()
    {
        if (gameOn)
        {
            // Get input from horizontal (A/D or Left/Right) and vertical (W/S or Up/Down)
            movementInputValue = Input.GetAxis("Vertical");
            turnInputValue = Input.GetAxis("Horizontal");
        }
    }

    // Move and turn player
    private void FixedUpdate()
    {
        if (gameOn)
        {
            Move();
            Turn();
        }
    }

    // Adjust position of player based on input
    private void Move()
    {
        Vector3 desiredForce = transform.forward * movementInputValue * moveAcceleration;
        rb.AddForce(desiredForce, ForceMode.Force);

        // Optional: Limit max speed
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;

        //Vector3 movement = transform.forward * movementInputValue * moveAcceleration * Time.deltaTime;
        //rb.MovePosition(rb.position + movement);
    }

    // Adjust rotation of player based on input
    private void Turn()
    {
        float turn = turnInputValue * turnSpeed;
        rb.AddTorque(Vector3.up * turn, ForceMode.Acceleration);

        //float turn = turnInputValue * turnSpeed * Time.deltaTime;
        //Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
        //rb.MoveRotation (rb.rotation * turnRotation);

    }
}