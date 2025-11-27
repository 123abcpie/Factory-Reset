using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Rigidbody))]
public class SinglePlayerMovement : NetworkBehaviour
{
    public float moveAcceleration = 50f;
    public float maxSpeed = 23f;
    public float turnSpeed = 20f;

    private Rigidbody rb;
    private float movementInputValue;
    private float turnInputValue;

    public bool gameOn = true;
    private Quaternion initialRotation;

    private void Awake()
    {
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        rb.drag = 1.5f;
        rb.angularDrag = 4f;
    }

    private void Update()
    {
        if (gameOn)
        {
            movementInputValue = Input.GetAxis("Vertical");
            turnInputValue = Input.GetAxis("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        if (!gameOn) return;

        Turn();
        Move();
        
        Vector3 currentEuler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(initialRotation.eulerAngles.x, currentEuler.y, initialRotation.eulerAngles.z);
    }

    private void Move()
    {
        Vector3 force = transform.forward * movementInputValue * moveAcceleration;
        rb.AddForce(force, ForceMode.Force);

        // Limit max speed
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    private void Turn()
    {
        float torque = turnInputValue * turnSpeed;
        rb.AddTorque(Vector3.up * torque, ForceMode.Acceleration);
    }
}