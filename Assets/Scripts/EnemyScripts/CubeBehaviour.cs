using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeBehaviour : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Patrol
    }

    public EnemyState currentState = EnemyState.Idle;
    public float detectionRange = 15f;
    public float waitTime = 1f;
    public Transform player;

    public Transform pointA;
    public Transform pointB;

    private NavMeshAgent agent;
    private Transform currentTarget;
    private float cooldown = 0f;
    private float waitTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
        currentState = EnemyState.Patrol;
        currentTarget = pointA; 
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
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= waitTime)
                {
                    currentState = EnemyState.Patrol;
                    waitTimer = 0f;
                } else
                {
                    currentState = EnemyState.Idle;
                }
            }
        }

        switch (currentState)
        {
            case EnemyState.Patrol:
                MoveToNextPoint();
                break;
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
        currentTarget = (currentTarget == pointA) ? pointB : pointA;
    }

    void HandleChase(float distance)
    {
        if (agent == null) return;
        agent.SetDestination(player.position);
    }

    void MoveToNextPoint()
    {
        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position);
        }
    }
}
