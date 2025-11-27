using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : NetworkBehaviour
{
    public float moveAcceleration = 50f;
    public float maxSpeed = 23f;
    public float turnSpeed = 20f;
    private float boostCooldown = 1f;
    public float boostSpeed = 25f;
    private bool boost = false;

    private Rigidbody rb;
    private float movementInputValue;
    private float turnInputValue;

    public bool gameOn = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 1.5f;
        rb.angularDrag = 4f;
    }

    private void Update()
    {
        // Only the local player reads input
        if (!IsOwner) return;

        if (gameOn)
        {
            movementInputValue = Input.GetAxis("Vertical");
            turnInputValue = Input.GetAxis("Horizontal");
            if (Input.GetKey(KeyCode.LeftShift) && boostCooldown <= 0)
            {
                boost = true;
                boostCooldown = 0.5f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return; // Only move local player locally
        if (!gameOn) return;

        Move();
        Turn();
        Boost();
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

    private void Boost()
    {
        if (boost)
        {
            if(boostCooldown == 0.5)
            {
                tempMaxSpeed = boostSpeed;
                rb.AddForce(transform.forward * boostSpeed, ForceMode.Impulse);
            }
            else if(boostCooldown <= 0)
            {
                boostCooldown = 2;
                tempMaxSpeed = maxSpeed;
                boost = false;
            }
        }
        boostCooldown -= Time.deltaTime;
    }
}