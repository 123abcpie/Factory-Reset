using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeBehaviour : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase
    }

    public EnemyState currentState = EnemyState.Idle;
    public float detectionRange = 15f;
    public Transform player;

    private NavMeshAgent agent;
    private float cooldown = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
                player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (cooldown > 0)
            cooldown -= Time.deltaTime;

        if (distance <= detectionRange)
            currentState = EnemyState.Chase;
        else
            currentState = EnemyState.Idle;

        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdle();
                break;
            case EnemyState.Chase:
                HandleChase(distance);
                break;
        }
    }

    void HandleIdle()
    {
        if (agent.hasPath)
            agent.ResetPath();
    }

    void HandleChase(float distance)
    {
        if (agent == null) return;
        agent.SetDestination(player.position);
    }
}
