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
    public EnemyState currentState = EnemyState.Patrol;
    public float targetDistance = 10f;
    public float meleeDistance = 2f;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                // Implement Idle behavior here
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
                    Idle;
                }
                break;
            case EnemyState.Ranged:
                // Implement Ranged behavior here
                if (Vector3.Distance(transform.position, player.position) > targetDistance)
                {
                    currentState = EnemyState.Idle;
                }
                if (Vector3.Distance(transform.position, player.position) < meleeDistance)
                {
                    currentState = EnemyState.Melee;
                }
                break;
            case EnemyState.Melee:
                // Implement Melee behavior here
                if (Vector3.Distance(transform.position, player.position) > targetDistance)
                {
                    currentState = EnemyState.Idle;
                }
                if (Vector3.Distance(transform.position, player.position) > meleeDistance)
                {
                    currentState = EnemyState.Ranged;
                }
                break;
        }
    }
    
    public void Idle()
    {
        
    }
    public void Ranged()
    {
        
    }
    public void Melee()
    {
        
    }
}
