using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Rigidbody))]
public class SinglePlayerMovement : NetworkBehaviour
{
    public float moveAcceleration = 50f;
    public float maxSpeed = 23f;
    private float tempMaxSpeed = 23f;
    public float turnSpeed = 20f;
    private float boostCooldown = 1f;
    public float boostSpeed = 25f;
    private bool boost = false;

    public float attackRate = 2f;
    public float projectileSpeed = 10f;
    public float shootCooldown = 2f;
    public GameObject projectilePrefab;
    private Rigidbody projectileRb;

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
            if (Input.GetKey(KeyCode.LeftShift) && boostCooldown <= 0)
            {
                boost = true;
                boostCooldown = 0.5f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!gameOn) return;

        Turn();
        Move();
        Boost();
        Shoot();
        
        Vector3 currentEuler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(initialRotation.eulerAngles.x, currentEuler.y, initialRotation.eulerAngles.z);
    }

    private void Move()
    {
        Vector3 force = transform.forward * movementInputValue * moveAcceleration;
        rb.AddForce(force, ForceMode.Force);

        // Limit max speed
        if (rb.velocity.magnitude > tempMaxSpeed)
            rb.velocity = rb.velocity.normalized * tempMaxSpeed;
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
    /*
    First attempt at shoot function. Accidently used tranform of the player object and not the player projectile, so when pressing the 
    Spacebar the player object moves. Does result in a cool quick boost tho. Maybe integrate it in?
    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) )
            {
                 //instantiate a projectile object and send it the player's way
                GameObject projectile = UnityEngine.Object.Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                
                //push forward the projectile
                Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
                if (projectileRb != null)
                {
                    projectile.transform.rotation = transform.rotation;
                    projectileRb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
                }
                shootCooldown = attackRate / 2 - 0.05f;
            }
    }
    */

     private void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) )
            {
                 //instantiate a projectile object and send it the player's way
                GameObject projectile = UnityEngine.Object.Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                
                //push forward the projectile
                Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
                if (projectileRb != null)
                {
                    projectile.transform.rotation = transform.rotation;
                    projectileRb.AddForce(projectile.transform.forward * projectileSpeed, ForceMode.Impulse);
                }
                shootCooldown = attackRate / 2 - 0.05f;
            }
    }

}