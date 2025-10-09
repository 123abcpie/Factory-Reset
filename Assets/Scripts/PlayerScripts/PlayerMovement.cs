using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb; 
    private float moveX;
    private float moveY;
    public bool gameOn = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOn)
        {
        // Get input from horizontal (A/D or Left/Right) and vertical (W/S or Up/Down)
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        // Create movement vector
        Vector3 movement = new Vector3(moveX, 0f, moveY).normalized;

        // Move the character
        rb.AddForce(movement * moveSpeed);
        }
    }
}
