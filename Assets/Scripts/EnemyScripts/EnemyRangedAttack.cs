using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : EnemyState
{
    public float rangedDistance = 10f;
    public float meleeDistance = 2f;
    public float fireRate = 1f;
    public float projectileSpeed = 10f;

    private float fireCooldown = 0f;
    public override void Enter(EnemyStateManager enemy)
    {
        
    }

    public override void Update(EnemyStateManager enemy)
    {
        if (enemy.player == null) { return; }

        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distance < meleeDistance || distance > rangedDistance) { enemy.ChangeState(enemy.PatrolState); }

        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            ShootAtPlayer(enemy);
            fireCooldown = 1f / fireRate;
        }
    }

    public override void Exit(EnemyStateManager enemy)
    {

    }

    void ShootAtPlayer(EnemyStateManager enemy)
    {
        Vector3 direction = (enemy.player.position - enemy.transform.position).normalized;

        // Instantiate the projectile
        GameObject projectile = Object.Instantiate(enemy.projectilePrefab, enemy.transform.position, Quaternion.identity);

        // Apply velocity or movement
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
    }

}
