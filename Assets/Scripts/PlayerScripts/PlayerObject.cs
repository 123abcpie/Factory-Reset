using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public int health = 3;
    private Rigidbody rb;
    public float iFrames = 0.5f;
    private float cooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (health < 1)
        {
            PlayerMovementV3 playerMovement = gameObject.GetComponent<PlayerMovementV3>();
            playerMovement.gameOn = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            float speed = 10f;
            rb.AddForce(collider.gameObject.transform.forward * speed, ForceMode.Impulse);
            Destroy(collider.gameObject);
            if(cooldown <= 0)
            {
                health -= 1;
                cooldown = iFrames;
            }
        }
    }
}
