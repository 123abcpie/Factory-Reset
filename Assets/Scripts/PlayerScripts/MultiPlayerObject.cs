using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class MultiPlayerObject : NetworkBehaviour
{
    public NetworkVariable<int> health = new NetworkVariable<int>(
        3,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);
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
        if (health.Value < 1)
        {
            PlayerMovement playerMovement = gameObject.GetComponent<PlayerMovement>();
            playerMovement.gameOn = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag("Bullet") && !collider.CompareTag("CollidableEnemy")) return;

        float speed = 10f;
        rb.AddForce(collider.transform.forward * speed, ForceMode.Impulse);

        if (cooldown <= 0)
        {
            // **Update locally for immediate feedback**
            if (IsOwner)
            {
                health.Value -= 1; // client sees it immediately
            }

            // **Tell the server to update authoritative value**
            if (IsOwner)
            {
                TakeDamageServerRpc(1);
            }

            cooldown = iFrames;
        }

        if (collider.CompareTag("Bullet"))
            Destroy(collider.gameObject);
    }

    // ServerRpc to modify the authoritative health on the server
    [ServerRpc]
    private void TakeDamageServerRpc(int damage)
    {
        health.Value -= damage;
    }
}
