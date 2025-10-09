using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBehaviour : MonoBehaviour
{
    // Basic Enum
    public enum EnemyState
    {
        Melee,
        Ranged,
        Idle
    }
    public EnemyState currentState = EnemyState.Idle;
    public GameObject projectilePrefab;
    public GameObject burstPrefab;
    public float targetDistance = 10f;
    public float meleeDistance = 2f;
    public Transform player;
    public float rotationSpeed = 0.1f;
    public float attackRate = 2f;
    public float projectileSpeed = 10f;
    private Quaternion targetRotation;
    public float cooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        targetRotation = transform.localRotation;
        //detect the player if left unassigned
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogWarning("Player not found in scene!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                // The case switch for the Idle State
                if (Vector3.Distance(transform.position, player.position) < meleeDistance)
                {
                    currentState = EnemyState.Melee;
                }
                else if (Vector3.Distance(transform.position, player.position) < targetDistance)
                {
                    currentState = EnemyState.Ranged;
                }
                else
                {
                    Idle();
                }
                break;
            case EnemyState.Ranged:
                // The case switch for the Ranged State
                if (Vector3.Distance(transform.position, player.position) > targetDistance)
                {
                    currentState = EnemyState.Idle;
                }
                else if (Vector3.Distance(transform.position, player.position) < meleeDistance)
                {
                    currentState = EnemyState.Melee;
                }
                else
                {
                    Ranged();
                }
                break;
            case EnemyState.Melee:
                // The case switch for the Melee State
                if (Vector3.Distance(transform.position, player.position) > targetDistance)
                {
                    currentState = EnemyState.Idle;
                }
                if (Vector3.Distance(transform.position, player.position) > meleeDistance)
                {
                    currentState = EnemyState.Ranged;
                }
                else
                {
                    Melee();
                }
                break;
        }
    }

    public void Idle()
    {
        //smoothly point in the direction of the selected random angle.
        if (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
        }
        else
        {
            //randomly determine a direction to scan in
            if (UnityEngine.Random.Range(0, 2) < 1)
            {
                float randomYAngle = UnityEngine.Random.Range(0f, 360f);
                targetRotation = Quaternion.Euler(0f, randomYAngle, 0f);
            }
        }
    }
    public void Ranged()
    {
        if (cooldown > attackRate / 2 - 0.05f && cooldown < attackRate / 2 + 0.05f)
        {
            // Only shoots if aiming at the player
            float angleToPlayer = Vector3.Angle(transform.forward, player.position - transform.position);
            if (angleToPlayer <= 10f)
            {

                //instantiate a projectile object and send it the player's way
                GameObject projectile = UnityEngine.Object.Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                GameObject burst = UnityEngine.Object.Instantiate(burstPrefab, transform.position, Quaternion.identity);
                //push forward the projectile
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    projectile.transform.rotation = transform.rotation;
                    rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
                }
                Rigidbody brb = burst.GetComponent<Rigidbody>();
                if (brb != null)
                {
                    burst.transform.rotation = transform.rotation;
                    brb.AddForce(transform.forward * projectileSpeed / 2, ForceMode.Impulse);
                }
                cooldown = attackRate / 2 - 0.05f;
            }
            else
            {
                cooldown = attackRate / 2 + 0.06f;
            }
        }
        else if (cooldown <= attackRate / 2 && cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else if (cooldown <= 0f)
        {
            cooldown = attackRate;
        }
        else
        {
            trackPlayer();
        }
    }
    public void Melee()
    {
        if (cooldown > attackRate / 2 - 0.05f && cooldown < attackRate / 2 + 0.05f)
        {
            Vector3 currentRotation = transform.eulerAngles;
            for (float i = 0; i < 24; i++)
            {
                Vector3 newRotation = new Vector3(currentRotation.x, currentRotation.y + 15 * i, currentRotation.z);

                //instantiate a projectile object and send it the player's way
                GameObject projectile = UnityEngine.Object.Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                GameObject burst = UnityEngine.Object.Instantiate(burstPrefab, transform.position, Quaternion.identity);
                ProjectileScript projectileScript = projectile.GetComponent<ProjectileScript>();
                if (projectileScript != null)
                {
                    projectileScript.lifetime = 0.2f;
                }
                //push forward the projectile
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    projectile.transform.eulerAngles = newRotation;
                    rb.AddForce(projectile.transform.forward * projectileSpeed, ForceMode.Impulse);
                }
                Rigidbody brb = burst.GetComponent<Rigidbody>();
                if (brb != null)
                {
                    burst.transform.eulerAngles = newRotation;
                    brb.AddForce(projectile.transform.forward * projectileSpeed / 2, ForceMode.Impulse);
                }
            }
            cooldown -= Time.deltaTime;

        }
        else if (cooldown <= attackRate / 2 && cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else if (cooldown <= 0f)
        {
            cooldown = attackRate;
        }
        else
        {
            trackPlayer();
        }
    }
    public void trackPlayer()
    {
        //this part tracks the player movement while in this state
            cooldown -= Time.deltaTime;
            Vector3 directionToPlayer = player.position - transform.position;
            targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
    }
}

