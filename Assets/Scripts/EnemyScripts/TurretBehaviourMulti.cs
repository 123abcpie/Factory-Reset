using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
[RequireComponent(typeof(NetworkTransform))]
public class TurretBehaviourMulti : NetworkBehaviour
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
    public float meleeDistance = 3f;
    private Transform player1;
    private Transform player2;
    private Transform closestPlayer;
    public float rotationSpeed = 0.1f;
    public float attackRate = 2f;
    public float projectileSpeed = 10f;
    private Quaternion targetRotation;
    public float cooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        targetRotation = transform.localRotation;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnClientConnected(ulong clientId)
    {
        // Assign player references automatically as they connect
        var playerObj = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        if (playerObj == null) return;

        if (player1 == null)
        {
            player1 = playerObj.transform;
            Debug.Log("Turret assigned Player 1");
        }
        else if (player2 == null)
        {
            player2 = playerObj.transform;
            Debug.Log("Turret assigned Player 2");
            TrackPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsServer) return;
        if (!player1 || !player2) return;
        float curDistance = Vector3.Distance(transform.position, closestPlayer.position);
        switch (currentState)
        {
            case EnemyState.Idle:
                // The case switch for the Idle State
                if (curDistance < meleeDistance)
                {
                    currentState = EnemyState.Melee;
                }
                else if (curDistance < targetDistance)
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
                if (curDistance > targetDistance)
                {
                    currentState = EnemyState.Idle;
                }
                else if (curDistance < meleeDistance)
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
                if (curDistance > targetDistance)
                {
                    currentState = EnemyState.Idle;
                }
                if (curDistance > meleeDistance)
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
            float angleToPlayer = Vector3.Angle(transform.forward, closestPlayer.position - transform.position);
            if (angleToPlayer <= 15f)
            {

                //instantiate a projectile object and send it the player's way
                GameObject projectile = UnityEngine.Object.Instantiate(projectilePrefab, transform.position, transform.rotation);
                projectile.GetComponent<NetworkObject>().Spawn();
                GameObject burst = UnityEngine.Object.Instantiate(burstPrefab, transform.position, transform.rotation);
                burst.GetComponent<NetworkObject>().Spawn();
                //push forward the projectile
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    projectile.transform.rotation = transform.rotation;
                    rb.velocity = new Vector3(projectile.transform.forward.x * projectileSpeed, rb.velocity.y, projectile.transform.forward.z * projectileSpeed);
                }
                Rigidbody brb = burst.GetComponent<Rigidbody>();
                if (brb != null)
                {
                    burst.transform.rotation = transform.rotation;
                    brb.useGravity = true;
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
            TrackPlayer();
        }
    }
    public void Melee()
    {
        Debug.Log("Is Melee");
        if (cooldown > attackRate / 2 - 0.05f && cooldown < attackRate / 2 + 0.05f)
        {
            Burst();
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
            TrackPlayer();
        }
    }
    public void TrackPlayer()
    {
        //this part tracks the player movement while in this state
        cooldown -= Time.deltaTime;
        Vector3 player1Distance = player1.transform.position - transform.position;
        Vector3 player2Distance = player2.transform.position - transform.position;
        Vector3 directionToPlayer;
        if (player1Distance.sqrMagnitude < player2Distance.sqrMagnitude)
        {
            directionToPlayer = player1Distance;
            closestPlayer = player1;
        }
        else
        {
            directionToPlayer = player2Distance;
            closestPlayer = player2;
        }
        directionToPlayer.y = 0f;
        targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
    }
    public void Burst()
    {
        Debug.Log("In Burst");
        Vector3 currentRotation = transform.eulerAngles;
        for (float i = 0; i < 24; i++)
        {
            Vector3 newRotation = new Vector3(currentRotation.x, currentRotation.y + 15 * i, currentRotation.z);
            //instantiate a projectile object and send it the player's way
            GameObject projectile = UnityEngine.Object.Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<NetworkObject>().Spawn();
            GameObject burst = UnityEngine.Object.Instantiate(burstPrefab, transform.position, Quaternion.identity);
            burst.GetComponent<NetworkObject>().Spawn();
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
        cooldown = attackRate / 2 - 0.05f;
    }
}
