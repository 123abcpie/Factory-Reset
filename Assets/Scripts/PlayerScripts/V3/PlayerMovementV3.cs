using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementV3 : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float turnSpeed = 180f;
    private Rigidbody rb; 
    private float moveX;
    private float moveY;
    public bool gameOn = true;

    private string movementAxisName;
    private string turnAxisName;

    private float movementInputValue;
    private float turnInputValue;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        // Create movement vector
        Vector3 movement = new Vector3(moveX, 0f, moveY).normalized;

        // Move the character
        rb.AddForce(movement * moveSpeed);
        }
    }

    // Move and turn player
    private void FixedUpdate ()
    {
        if (gameOn)
        {
            Move ();
            Turn ();
        }
    }

    // Adjust position of player based on input
    private void Move() 
    {
        Vector3 movement = transform.forward * movementInputValue * moveSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + movement);
    }

    // Adjust rotation of player based on input
    private void Turn ()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

        rb.MoveRotation (rb.rotation * turnRotation);
    }
}
