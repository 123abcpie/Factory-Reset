using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public GameObject projectilePrefab;
    public float rangedDistance = 10f;
    public float fireRate = 1f;
    public float projectileSpeed = 10f;

    private float fireCooldown = 0f;
    // Update is called once per frame
    void Update()
    {
        if (player == null) { return; }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < rangedDistance)
        {
            fireCooldown -= Time.deltaTime;
            if (fireCooldown <= 0f)
            {
                ShootAtPlayer();
                fireCooldown = 1f / fireRate;
            }
        }
    }

    void ShootAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Apply velocity or movement
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
    }
}
