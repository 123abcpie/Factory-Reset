using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from horizontal (A/D or Left/Right) and vertical (W/S or Up/Down)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Create movement vector
        Vector3 move = new Vector3(moveX, 0f, moveZ);

        // Move the character
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }
}
